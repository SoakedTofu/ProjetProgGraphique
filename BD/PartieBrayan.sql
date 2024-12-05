
/*3.3. Créer un déclencheur qui permet d’insérer les participants dans une séance si le
nombre de places maximum n’est pas atteint. Sinon, il affiche un message d’erreur
avisant qu’il ne reste plus de places disponibles pour la séance choisie*/

DELIMITER //
CREATE TRIGGER nbAdherentMaxi BEFORE INSERT ON seances_adherents_noteappreciation FOR EACH ROW

BEGIN

  DECLARE nbP INT;
  DECLARE nbPMax INT;
  SET   nbP=(SELECT nbPlaces FROM seances WHERE idSeance=NEW.idSeance);
  SET  nbPMax=(SELECT nbPlacesMax FROM activites where nom=(select nomActivite from seances where seances.idSeance=NEW.idSeance));

  if (nbP >= nbPMax) then
       SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Il ne reste plus de places disponibles';

  end if ;

END//

DELIMITER ;



/*3.4. Vous pouvez ajouter tout autre déclencheur que vous jugez pertinent pour le
fonctionnement de la BDD. Justifiez votre choix.*/


DELIMITER //
CREATE TRIGGER ageMaxi BEFORE INSERT ON adherents FOR EACH ROW

BEGIN

        DECLARE age INT;
        SET age=YEAR( CURRENT_DATE)-YEAR(NEW.dateNaissance);

        if(age<18) then
             SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Vous devez avoir 18 ans et plus ';

        else
            set NEW.age= age;
        end if ;


END//

DELIMITER ;



/*Charger la base avec des données réalistes (chaque base devra comporter entre 50 et 100
occurrences) à partir des sites web fournis dans le cours (10 données par table à peu près)*/



-- Insertion dans la table Administrateur (seul administrateur)

INSERT INTO Administrateur (nomAdministrateur, motDePasse)
VALUES
('admin_unique', 'motdepasse123');

-- Insertion dans la table Categories (catégories diverses)
INSERT INTO Categories (nom)
VALUES
('Sport'),
('Musique'),
('Cuisine'),
('Arts Plastiques'),
('Danse');

-- Insertion dans la table Activites
INSERT INTO Activites (nom, prixOrganisation, prixVente, nomAdministrateur, nbPlacesMax)
VALUES
('Football', 200.0, 20.0, 'admin_unique', 30),
('Guitar', 150.0, 15.0, 'admin_unique', 25),
('Piano', 180.0, 18.0, 'admin_unique', 20),
('Cuisine Italienne', 100.0, 10.0, 'admin_unique', 15),
('Peinture', 120.0, 12.0, 'admin_unique', 20),
('Salsa', 130.0, 13.0, 'admin_unique', 25),
('Yoga', 50.0, 5.0, 'admin_unique', 30),
('Chanson Française', 200.0, 20.0, 'admin_unique', 20),
('Photographie', 170.0, 17.0, 'admin_unique', 15),
('Théâtre', 160.0, 16.0, 'admin_unique', 18);

-- Insertion dans la table Categories_Activites (lien entre catégories et activités)
INSERT INTO Categories_Activites (nomCategorie, nomActivite)
VALUES
('Sport', 'Football'),
('Musique', 'Guitar'),
('Musique', 'Piano'),
('Cuisine', 'Cuisine Italienne'),
('Arts Plastiques', 'Peinture'),
('Danse', 'Salsa'),
('Sport', 'Yoga'),
('Musique', 'Chanson Française'),
('Arts Plastiques', 'Photographie'),
('Arts Plastiques', 'Théâtre');

-- Insertion dans la table Seances (ajout de séances pour chaque activité)
INSERT INTO Seances (idSeance, date, heureDebut, heureFin, nbPlaces, nomActivite, nomAdministrateur)
VALUES
(1, '2024-12-01', '09:00:00', '10:30:00', 0, 'Football', 'admin_unique'),
(2, '2024-12-02', '10:00:00', '11:30:00', 0, 'Guitar', 'admin_unique'),
(3, '2024-12-03', '14:00:00', '16:00:00', 0, 'Piano', 'admin_unique'),
(4, '2024-12-04', '16:00:00', '17:30:00', 0, 'Cuisine Italienne', 'admin_unique'),
(5, '2024-12-05', '08:00:00', '09:30:00', 0, 'Peinture', 'admin_unique'),
(6, '2024-12-06', '11:00:00', '12:30:00', 0, 'Salsa', 'admin_unique'),
(7, '2024-12-07', '09:00:00', '10:00:00', 0, 'Yoga', 'admin_unique'),
(8, '2024-12-08', '12:00:00', '14:00:00', 0, 'Chanson Française', 'admin_unique'),
(9, '2024-12-09', '15:00:00', '16:30:00', 0, 'Photographie', 'admin_unique'),
(10, '2024-12-10', '18:00:00', '20:00:00', 0, 'Théâtre', 'admin_unique');

-- Insertion dans la table Adherents (dix adhérents fictifs)
INSERT INTO Adherents (numeroIdentification, nom, prenom, adresse, dateNaissance, age, nomAdministrateur)
VALUES
('12345678901', 'Dupont', 'Jean', '123 rue de Paris', '1990-05-15', 34, 'admin_unique'),
('23456789012', 'Martin', 'Claire', '45 rue des Lilas', '1985-08-20', 39, 'admin_unique'),
('34567890123', 'Bernard', 'Paul', '67 avenue des Champs', '2000-01-10', 24, 'admin_unique'),
('45678901234', 'Durand', 'Marie', '89 boulevard de Lyon', '1995-03-25', 29, 'admin_unique'),
('56789012345', 'Lemoine', 'Louis', '12 place de la République', '1992-06-18', 32, 'admin_unique'),
('67890123456', 'Roche', 'Sophie', '34 rue de la Liberté', '1991-11-12', 33, 'admin_unique'),
('78901234567', 'Dufresne', 'Julien', '56 rue de la Gare', '1994-09-03', 30, 'admin_unique'),
('89012345678', 'Leclerc', 'Alice', '78 rue des Fleurs', '1988-02-25', 36, 'admin_unique'),
('90123456789', 'Charpentier', 'Franck', '90 rue de la Paix', '1997-04-30', 27, 'admin_unique'),
('01234567890', 'Pires', 'Isabelle', '11 rue du Moulin', '1999-07-16', 25, 'admin_unique');

-- Insertion dans la table noteAppreciation (dix notes d'appréciation fictives)
INSERT INTO noteAppreciation (idNote, note)
VALUES
(1, 5),
(2, 4),
(3, 3),
(4, 4),
(5, 5),
(6, 2),
(7, 5),
(8, 3),
(9, 4),
(10, 4);

-- Insertion dans la table Seances_Adherents_NoteAppreciation (lien entre les séances, adhérents et notes)
INSERT INTO Seances_Adherents_NoteAppreciation (idNote, idSeance, numeroIdentification)
VALUES
(1, 1, 'AL-1988-239'),
(2, 2, 'CM-1985-898'),
(3, 3, 'FC-1997-641'),
(4, 4, 'IP-1999-454'),
(5, 5, 'JD-1990-692'),
(6, 6, 'JD-1994-155'),
(7, 7, 'LL-1992-658'),
(8, 8, 'MD-1995-691'),
(9, 9, 'PB-2000-637'),
(10, 10, 'SR-1991-518');

-----VUES

CREATE VIEW nbParticipant AS
    SELECT SUM(nbPlaces) AS nbParticipant,nomActivite FROM seances GROUP BY nomActivite;

SELECT * FROM nbParticipant;

CREATE VIEW nbParticipantMoyParMois AS
    SELECT FLOOR( AVG(nbPlaces)) AS nbParticipant,date FROM seances GROUP BY MONTH(date);

SELECT * FROM nbParticipantMoyParMois;


/*Procedures*/


--Procedure qui permet d'afficher les activites avec leurs moyennes de notes 

DELIMITER //
CREATE  PROCEDURE AffActivite ()
BEGIN
   SELECT nomActivite,
            ifnull(ROUND( AVG(note),2) ,0) as moyenneNote
FROM seances
left join seances_adherents_noteappreciation san on seances.idSeance = san.idSeance
left join noteappreciation n on san.idNote = n.idNote
GROUP BY nomActivite;
end //
DELIMITER ;

CALL AffActivite();

--Procedure qui permet d'afficher une acivite a partir de son nom

DELIMITER //
CREATE  PROCEDURE Activite (IN  nomAct varchar(50))
BEGIN
   SELECT * FROM activites where nom=nomAct;

end //
DELIMITER ;

CALL Activite("Football");


--Procedure qui permet de modifier une acivite à partir de son nom

DELIMITER //
CREATE  PROCEDURE ModifActivite (IN  nomAct varchar(50),IN prixOrg double,
IN prixVt double,IN nomAdmin VARCHAR(50),IN nbPlaces INT ,IN nomActPrec varchar(50))
BEGIN

    alter table seances
    drop FOREIGN KEY fk_Seances_Activites;

  update activites
      SET nom=nomAct,prixOrganisation=prixOrg,prixVente=prixVt,nomAdministrateur=nomAdmin,nbPlacesMax=nbPlaces
  where nom=nomActPrec;

    update seances
    SET nomActivite=nomAct
    where nomActivite=nomActPrec;

      SET FOREIGN_KEY_CHECKS=0;


    ALTER TABLE seances
    ADD  CONSTRAINT fk_Seances_Activites FOREIGN KEY (nomActivite) REFERENCES Activites(nom);

end //
DELIMITER ;



CALL ModifActivite("Football",20,10,"admin_unique",2,"Soccer");

--Procedure qui permet de supprimer une acivite à partir de son nom


DELIMITER //
CREATE  PROCEDURE SuppActivite (IN  nomAct varchar(50))
BEGIN



    DELETE FROM seances_adherents_noteappreciation WHERE idSeance=(select idSeance from seances where nomActivite=nomAct);


     DELETE FROM seances WHERE nomActivite=nomAct;

    DELETE FROM activites WHERE nom=nomAct;


end //
DELIMITER ;


call  SuppActivite('Football');

--Procedure qui permet d'afficher une seance avec son nombre de places restantes à partir de son nom d'activité

DELIMITER //
CREATE  PROCEDURE AffSeance (IN nomAct VARCHAR(50))
BEGIN
   SELECT *,(SELECT nbPlacesMax FROM activites where nom=seances.nomActivite)-nbPlaces AS nbPMax from seances
   where nomActivite=nomAct;
end //
DELIMITER ;


CALL AffSeance('Yoga');

--Procédure qui permet de modifier une séance a partir de son id


DELIMITER //
CREATE  PROCEDURE ModifSeance (IN  dateForm DATE,IN hrDebut TIME,
IN hrFin TIME,IN nomAct varchar(50),IN id INT)
BEGIN

    update seances
    SET nomActivite=nomAct,date=dateForm,heureDebut=hrDebut,heureFin=hrFin
    where idSeance=id;


end //
DELIMITER ;

call ModifSeance('2020-10-11','12:00','13:00','Peinture',2);

--Procédure qui permet de supprimer une séance a partir de son id

DELIMITER //
CREATE  PROCEDURE SuppSeance (IN  id INT)
BEGIN

    DECLARE CONTINUE HANDLER FOR SQLSTATE '23000'


BEGIN
    RESIGNAL set message_text = 'Il existe dans la table seances_adherents_noteappreciation!';

END;

  DELETE FROM seances_adherents_noteappreciation WHERE idSeance in (id);

    DELETE FROM seances WHERE idSeance in (id);



end //
DELIMITER ;


call  SuppSeance(6);

--Procédure qui permet d'afficher les adhérents

DELIMITER //
CREATE  PROCEDURE AffAdherent ()
BEGIN

select *from adherents;


end //
DELIMITER ;

call  AffAdherent();

--procédure qui permet de modifier un adhérent

DELIMITER //
CREATE  PROCEDURE ModifAdherent (IN  nomAd varchar(50),IN  prenomAd varchar(50),IN  adrs varchar(50),IN  date DATE,in id VARCHAR(11))
BEGIN



  update adherents
      SET nom=nomAd,prenom=prenomAd,adresse=adrs,dateNaissance=date,age=YEAR( CURRENT_DATE)-YEAR(date)
  where numeroIdentification=id;


end //
DELIMITER ;

call ModifAdherent('Roche','Sophie','34 rue de la Ormeaux','1991-11-12','SR-1991-127');

--Procédure qui permet de supprimer un adhérent

DELIMITER //
CREATE  PROCEDURE SuppAdherent (IN  id varchar(11))
BEGIN

    DELETE FROM seances_adherents_noteappreciation WHERE numeroIdentification=id;

 DELETE  FROM adherents WHERE numeroIdentification=id;




end //
DELIMITER ;

call SuppAdherent('SR-1991-127');

--Procedure qui affiche toutes les activites

DELIMITER //
CREATE  PROCEDURE AffAllActivite ()
BEGIN

select *from activites;


end //
DELIMITER ;

call  AffAllActivite();

--Procedure qui affiche toutes les seances

DELIMITER //
CREATE  PROCEDURE AffAllSeance ()
BEGIN

select *from seances;


end //
DELIMITER ;

call  AffAllSeance();

--Procédure pour ajouter des adhérents

DELIMITER //
CREATE  PROCEDURE AjoutAdherent (IN  nomAd varchar(50),IN  prenomAd varchar(50),IN  adrs varchar(50),IN  date DATE,in id VARCHAR(11))
BEGIN




     INSERT INTO adherents ( nom, prenom, adresse, dateNaissance, age, nomAdministrateur)
     VALUES (nomAd,prenomAd,adrs,date,YEAR( CURRENT_DATE)-YEAR(date), 'admin_unique');



end //
DELIMITER ;

call AjoutAdherent('Roche','Sophie','34 rue de la Ormeaux','1991-11-12','SR-1991-127');

--procédure pour ajouter des activités

DELIMITER //
CREATE  PROCEDURE AjoutActivite (IN  nomAct varchar(50),IN prixOrg double,
                                 IN prixVt double,IN nomAdmin VARCHAR(50),IN nbPlaces INT )
BEGIN

  DECLARE EXIT HANDLER FOR 1062


BEGIN
    RESIGNAL set message_text = 'Erreur l activité  existe déja!';

END;

   DECLARE EXIT HANDLER FOR 1048


BEGIN
    RESIGNAL set message_text = 'Le nom de l activite ne peut etre null';

END;

    INSERT INTO activites VALUES (nomAct,prixOrg,prixVt,nomAdmin,nbPlaces);



end //
DELIMITER ;



CALL AjoutActivite("Foot",20,10,"admin_unique",2);

--Procédure qui permet d'ajouter des seances 

DELIMITER //
CREATE  PROCEDURE AjoutSeance (IN  dateForm DATE,IN hrDebut TIME,
                               IN hrFin TIME,IN nomAct varchar(50))
BEGIN


   INSERT INTO seances (date, heureDebut, heureFin,nbPlaces,  nomActivite, nomAdministrateur)
   VALUES (dateForm,hrDebut,hrFin,0,nomAct,'admin_unique');



end //
DELIMITER ;


call AjoutSeance('2020-10-11','18:00','20:00','Théâtre');

