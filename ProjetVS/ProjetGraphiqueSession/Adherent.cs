using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    public class Adherent
    {
        string nom;
        string prenom;
        string adresse;
        string dateNaissance;
        string age;
        string nomAdministrateur;

        public Adherent() {
            nom = "Aucun";
            prenom = "Aucun";
            adresse = "Aucun";
            dateNaissance = "Aucune";
            age = "Aucun";
        }

        public Adherent(string nom, string prenom, string adresse, string dateNaissance, string age)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.age = age;
        }

        public Adherent(string nom, string prenom, string adresse, string dateNaissance, string age, string nomAdministrateur)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.age = age;
            this.nomAdministrateur = nomAdministrateur;
        }
        public string Nom { get { return nom; } set { nom = value; } }
        public string Prenom { get {return prenom; } set { prenom = value; } }
        public string Adresse { get { return adresse; } set { adresse = value; } }  
        public string DateNaissance { get { return dateNaissance; } set { dateNaissance = value; } }
        public string Age { get { return age; } set { age = value; } }
        public string NomAdministrateur { get { return nomAdministrateur; } set { nomAdministrateur = value; } }

        public override string ToString()
        {
            return nom + prenom + adresse + dateNaissance + age;
        }

        public string ToStringCSV()
        {
            return $"{Nom},{Prenom},{Adresse},{DateNaissance},{Age},{NomAdministrateur}";
        }


    }
}
