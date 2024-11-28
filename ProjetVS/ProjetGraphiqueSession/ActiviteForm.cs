using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class ActiviteForm
    {
        string nom;
        double prixOrg;
        double prixVente;
        string nomAdministrateur;
        int nbPlaceMaximum;

        public ActiviteForm(string nom, double prixOrg, double prixVente, string nomAdministrateur, int nbPlaceMaximum)
        {
            this.nom = nom;
            this.prixOrg = prixOrg;
            this.prixVente = prixVente;
            this.nomAdministrateur = nomAdministrateur;
            this.nbPlaceMaximum = nbPlaceMaximum;
        }

        public string Nom { get { return nom; } set { nom = value; } }


        public double PrixOrg { get { return prixOrg; } set { prixOrg = value; } }

        public double PrixVente { get { return prixVente; } set { prixVente = value; } }

        public string NomAdmin {  get { return nomAdministrateur; } set { nomAdministrateur = value; } }


        public int NbPlacesMaxi { get { return nbPlaceMaximum; } set { nbPlaceMaximum = value; } }
        public override string ToString()
        {
            return nom + " "+prixOrg+" " + prixVente+" "+nomAdministrateur+" "+nbPlaceMaximum;
        }
    }
}
