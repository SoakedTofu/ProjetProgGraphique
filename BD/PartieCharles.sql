-- Charles Ouellet

/********************** TRIGGERS  **********************/

-- Créer un déclencheur qui permet de construire le numéro d’identification

DELIMITER //
CREATE TRIGGER before_insert_adherent_matricule
    BEFORE INSERT ON adherents
    FOR EACH ROW
    BEGIN
        SET NEW.numeroIdentification =
            CONCAT(
                SUBSTR(NEW.prenom, 1, 1),
                SUBSTR(NEW.nom, 1, 1),
                '-',
                YEAR(NEW.dateNaissance),
                '-',
                FLOOR(RAND() * (9 - 1 + 1)) + 1,
                FLOOR(RAND() * (9 - 1 + 1)) + 1,
                FLOOR(RAND() * (9 - 1 + 1)) + 1
            );
    END;
DELIMITER //

-- Créer un déclencheur qui permet de gérer le nombre de places disponibles

DELIMITER //
CREATE TRIGGER after_insert_adherents_seances_noteappreciation
    AFTER INSERT ON seances_adherents_noteappreciation
    FOR EACH ROW
    BEGIN
        UPDATE seances
            SET nbPlaces = nbPlaces + 1
            WHERE idSeance = NEW.idSeance;
    END;
DELIMITER //

/********************** VUES **********************/

-- Trouver le participant ayant le nombre de séances le plus élevé

CREATE VIEW ParticipantPlusActif AS
SELECT numeroIdentification "Numéro d'identification du participant",
       COUNT(*) 'Nombre de séance'
FROM seances_adherents_noteappreciation
GROUP BY numeroIdentification
ORDER BY `Nombre de séance` DESC
LIMIT 1;

-- Trouver le prix moyen par activité pour chaque participant

CREATE VIEW PrixMoyenParParticipant AS
SELECT CONCAT(prenom, ' ', adherents.nom) "Nom de l'adhérent",
       avg(prixVente) "Moyenne du prix des activités"
FROM adherents
INNER JOIN seances_adherents_noteappreciation san on adherents.numeroIdentification = san.numeroIdentification
INNER JOIN seances s on san.idSeance = s.idSeance
INNER JOIN activites a on s.nomActivite = a.nom
GROUP BY CONCAT(prenom, ' ', adherents.nom);


-- Afficher les notes d’appréciation pour chaque activité

CREATE VIEW NotesDesActivités AS
SELECT nom "Nom de l'activité",
       numeroIdentification "Numéro d'identification du participant",
       note "Note du participant"
FROM activites
INNER JOIN seances s on activites.nom = s.nomActivite
INNER JOIN seances_adherents_noteappreciation san on s.idSeance = san.idSeance
INNER JOIN noteappreciation n on san.idNote = n.idNote;


-- Affiche la moyenne des notes d’appréciations pour toutes les activités

CREATE VIEW MoyenneDesNotes AS
SELECT nomActivite "Nom de l'activité",
            avg(note)
FROM seances
INNER JOIN seances_adherents_noteappreciation san on seances.idSeance = san.idSeance
INNER JOIN noteappreciation n on san.idNote = n.idNote
GROUP BY nomActivite;

/********************** PROCEDURES **********************/

-- Nombre total d'adhérents

DELIMITER //
CREATE  PROCEDURE NbTotalAdherents ()
BEGIN
    SELECT COUNT(*)
    FROM adherents;
end //
DELIMITER ;

-- Nombre total d'activités

DELIMITER //
CREATE  PROCEDURE NbTotalActivites ()
BEGIN
    SELECT COUNT(*)
    FROM activites;
end //
DELIMITER ;

-- Nombre d'adhérents par activité

DELIMITER //
CREATE  PROCEDURE NbAdherentsParActivite ()
BEGIN
SELECT
    a.nom,
    (SELECT COUNT(*)
     FROM seances_adherents_noteappreciation san
     INNER JOIN seances s ON san.idSeance = s.idSeance
     WHERE s.nomActivite = a.nom AND s.idSeance = san.idSeance) AS nombreParticipants
    FROM activites a
    LEFT JOIN seances s ON a.nom = s.nomActivite
    GROUP BY a.nom;
end //
DELIMITER ;

-- Participant avec le plus de séances

DELIMITER //
CREATE  PROCEDURE NbAdherentsParActivite ()
BEGIN
SELECT
    a.nom,
    (SELECT COUNT(*) "compteSeance"
    FROM seances
    HAVING compteSeance > )
    FROM adherents;
end //
DELIMITER ;

-- Participant qui suit le plus de séances

DELIMITER //
CREATE PROCEDURE ParticipantPopulaire()
BEGIN
    SELECT
        CONCAT(a.prenom, " ", a.nom) AS participant_name,
        COUNT(sa.numeroIdentification) AS entries_count
    FROM seances_adherents_noteappreciation sa
    INNER JOIN adherents a ON sa.numeroIdentification = a.numeroIdentification
    GROUP BY sa.numeroIdentification
    ORDER BY entries_count DESC
    LIMIT 1;
END //
DELIMITER ;

-- Activité la mieux noté

DELIMITER //
CREATE PROCEDURE ActiviteMieuxNote()
BEGIN
    SELECT
        nomActivite,
        AVG(note)
    FROM seances
    INNER JOIN seances_adherents_noteappreciation san on seances.idSeance = san.idSeance
    INNER JOIN noteappreciation n on san.idNote = n.idNote
    GROUP BY seances.nomActivite
    HAVING
        AVG(n.note) = (
            SELECT MAX(avg_note)
            FROM (
                SELECT AVG(n.note) AS avg_note
                FROM seances
                INNER JOIN seances_adherents_noteappreciation san
                ON seances.idSeance = san.idSeance
                INNER JOIN noteappreciation n
                ON san.idNote = n.idNote
                GROUP BY seances.nomActivite
            ) AS maxNotes
        )
    LIMIT 1;
END //
DELIMITER ;

-- Activité avec le plus de séances

DELIMITER //
CREATE PROCEDURE SeancePopulaire()
BEGIN
    SELECT
        nomActivite,
        COUNT(*)
    FROM seances
    GROUP BY nomActivite
    HAVING
    COUNT(*) = (
        SELECT MAX(seance_count)
        FROM (
            SELECT COUNT(*) AS seance_count
            FROM seances
            GROUP BY nomActivite
        ) AS subquery
    )
    LIMIT 1;
END //
DELIMITER ;

-- Procédure pour avoir la liste des activités

DELIMITER //
CREATE PROCEDURE ListeActivites()
BEGIN
    SELECT nom
    FROM activites;
END //
DELIMITER ;

-- Fonction qui vérifie si quelqu'un est connecté

DELIMITER //
CREATE PROCEDURE VerifierConnecte()
BEGIN
    SELECT connecte
    FROM administrateur;
END //
DELIMITER ;




/********************** FONCTIONS **********************/

-- Fonction qui vérifie qu'un adhérent existe

DELIMITER //
CREATE function f_verifier_adherent (numIdentification VARCHAR(11)) RETURNS BOOLEAN
BEGIN
    DECLARE present BOOLEAN;

    SELECT COUNT(*) > 0 INTO present
    FROM adherents
    WHERE numeroIdentification = numIdentification;

    RETURN present;
end//
Delimiter ;

-- Fonction qui donne le nom complet à partir du numéro d'identification

DELIMITER //
CREATE function f_get_Nom (numIdentification VARCHAR(11)) RETURNS VARCHAR(50)
BEGIN
    DECLARE nomComplet VARCHAR(50);

    SELECT CONCAT(
           prenom,
           ' ',
           nom
           ) INTO nomComplet
    FROM adherents
    WHERE numeroIdentification = numIdentification;

    RETURN nomComplet;
end//
Delimiter ;

-- Fonction qui vérifie le nom de l'administrateur

DELIMITER //
CREATE function f_verifier_admin (nomAdmin VARCHAR(50)) RETURNS BOOLEAN
BEGIN
    DECLARE present BOOLEAN;

    SELECT COUNT(*) > 0 INTO present
    FROM administrateur
    WHERE nomAdministrateur = nomAdmin;

    RETURN present;
end//
Delimiter ;

-- Fonction qui vérifie la connexion de l'administrateur

DELIMITER //
CREATE function f_verifier_admin_MDP (nomAdmin VARCHAR(50), MDP VARCHAR(50)) RETURNS BOOLEAN
BEGIN
    DECLARE present BOOLEAN;

    SELECT COUNT(*) > 0 INTO present
    FROM administrateur
    WHERE nomAdministrateur = nomAdmin AND
          motDePasse = MDP;

    RETURN present;
end//
Delimiter ;
