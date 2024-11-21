-- Projet de session: Base de Données
-- Charles Ouellet & Bryan Dyvan lando Longmene

CREATE TABLE Administrateur (
  nomAdministrateur VARCHAR(50) PRIMARY KEY,
  motDePasse VARCHAR(50)
);

CREATE TABLE Categories (
  nom VARCHAR(50) PRIMARY KEY
);

CREATE TABLE Activites (
    nom VARCHAR(50) PRIMARY KEY,
    prixOrganisation DOUBLE,
    prixVente DOUBLE,
    nomAdministrateur VARCHAR(50),
    nbPlacesMax INT,
    CONSTRAINT fk_admin_activites FOREIGN KEY (nomAdministrateur) REFERENCES Administrateur(nomAdministrateur)
);

CREATE TABLE Categories_Activites (
    nomCategorie VARCHAR(50),
    nomActivite  VARCHAR(50)
);

CREATE TABLE Seances (
  idSeance INT PRIMARY KEY AUTO_INCREMENT,
  date DATE,
  heureDebut TIME,
  heureFin TIME,
  nbPlaces INT,
  nomActivite VARCHAR(50),
  nomAdministrateur VARCHAR(50),
  CONSTRAINT fk_Seances_Activites FOREIGN KEY (nomActivite) REFERENCES Activites(nom),
  CONSTRAINT fk_Seances_Administrateur FOREIGN KEY (nomAdministrateur) REFERENCES Administrateur(nomAdministrateur)
);


CREATE TABLE Adherents (
  numeroIdentification VARCHAR(11) PRIMARY KEY,
  nom VARCHAR(50),
  prenom VARCHAR(50),
  adresse VARCHAR(100),
  dateNaissance DATE,
  age INT,
  nomAdministrateur VARCHAR(50),
  CONSTRAINT fk_Adherents_Administrateur FOREIGN KEY (nomAdministrateur) REFERENCES Administrateur(nomAdministrateur)
);

CREATE TABLE noteAppreciation (
    idNote INT PRIMARY KEY AUTO_INCREMENT,
    note INT
);


CREATE TABLE Seances_Adherents_NoteAppreciation (
  idNote INT,
  idSeance INT,
  numeroIdentification VARCHAR(11),
  CONSTRAINT fk_SANA_NotesAppreciation FOREIGN KEY (idNote) REFERENCES noteAppreciation(idNote),
  CONSTRAINT fk_SANA_Seances FOREIGN KEY (idSeance) REFERENCES Seances(idSeance),
  CONSTRAINT fk_SANA_Adherents FOREIGN KEY (numeroIdentification) REFERENCES Adherents(numeroIdentification)
);

-- Triggers

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

DELIMITER //
CREATE TRIGGER after_insert_adherents_seances_noteappreciation
    AFTER INSERT ON seances_adherents_noteappreciation
    FOR EACH ROW
    BEGIN
        UPDATE seances
            SET nbPlaces = nbPlaces + 1;
    END;
DELIMITER //


