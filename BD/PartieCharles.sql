-- Charles Ouellet

-- Trigger


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

-- Vues

-- Trouver le participant ayant le nombre de séances le plus élevé

SELECT *,
       COUNT(*) 'Nombre de séance'
FROM seances_adherents_noteappreciation
GROUP BY numeroIdentification
ORDER BY `Nombre de séances` DESC
LIMIT 1;

-- Trouver le prix moyen par activité pour chaque participant



-- Afficher les notes d’appréciation pour chaque activité

SELECT nom "Nom de l'activité",
       numeroIdentification "Numéro d'identification du participant",
       note "Note du participant"
FROM activites
INNER JOIN seances s on activites.nom = s.nomActivite
INNER JOIN seances_adherents_noteappreciation san on s.idSeance = san.idSeance
INNER JOIN noteappreciation n on san.idNote = n.idNote;


-- Affiche la moyenne des notes d’appréciations pour toutes les activités
