using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class Activite
    {
        string nom;
        double note;
        double prixOrganisation;
        double prixVente;
        string nomAdministrateur;
        int nbPlacesMax;


        public Activite(string nom, double note)
        {
            this.nom = nom;
            this.note = note;
        }

        public Activite(string nom, double prixOrganisation, double prixVente, string nomAdministrateur, int nbPlacesMax)
        {
            Nom = nom;
            PrixOrganisation = prixOrganisation;
            PrixVente = prixVente;
            NomAdministrateur = nomAdministrateur;
            NbPlacesMax = nbPlacesMax;
        }

        public string Nom { get { return nom; } set { nom = value; } }
        public double Note { get { return note; } set { note = value; } }

        public Double PrixOrganisation { get { return prixOrganisation; } set { prixOrganisation = value; } }

        public Double PrixVente { get { return prixVente; } set { prixVente = value; } }

        public String NomAdministrateur { get { return nomAdministrateur; } set { nomAdministrateur = value; } }

        public int NbPlacesMax { get { return nbPlacesMax; } set { nbPlacesMax = value; } }

        public override string ToString()
        {
            return nom+" "+note;
        }

        public string Visible

        {
           
            get {

                if (Singleton.getInstance().GetAdmin())
                {
                    return "visible";

                }
                else
                {
                    return "Collapsed";
                }
            }
            
        }

        public string ToStringCSV() 
        {
            return $"{Nom},{PrixOrganisation},{PrixVente},{NomAdministrateur},{NbPlacesMax}";
        }


    }
}
