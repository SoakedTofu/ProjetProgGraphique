using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    public class Adherent
    {
        string id;
        string nom;
        string prenom;
        string adresse;
        string dateNaissance;
        string age;
        string nomAdministrateur;
        string numeroIdentification;

        public Adherent() {
            id = "aucun";
            nom = "Aucun";
            prenom = "Aucun";
            adresse = "Aucun";
            dateNaissance = "Aucune";
            age = "Aucun";
        }

        public Adherent(string id, string nom, string prenom, string adresse, string dateNaissance, string age)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.age = age;

        }

        public Adherent(string numeroIdentification, string nom, string prenom, string adresse, string dateNaissance, int age, string nomAdministrateur)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.age = age.ToString();
            this.nomAdministrateur = nomAdministrateur;
            this.numeroIdentification = numeroIdentification;
        }

        public string Id { get { return id; } set { id = value; } }
        public string Nom { get { return nom; } set { nom = value; } }
        public string Prenom { get { return prenom; } set { prenom = value; } }
        public string Adresse { get { return adresse; } set { adresse = value; } }
        public string DateNaissance { get { return dateNaissance; } set { dateNaissance = value; } }
        public string Age { get { return age; } set { age = value; } }
        public string NomAdministrateur { get { return nomAdministrateur; } set { nomAdministrateur = value; } }
        public string NumeroIdentification { get { return numeroIdentification; } set { numeroIdentification = value; } }

        public override string ToString()
        {
            return id+ nom + prenom + adresse + dateNaissance + age;
        }

        public string ToStringCSV()
        {
            return $"{NumeroIdentification},{Nom},{Prenom},{Adresse},{DateNaissance},{Age},{NomAdministrateur}";
        }


    }
}
