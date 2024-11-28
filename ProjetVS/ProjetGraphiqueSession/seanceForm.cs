using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class seanceForm
    {
        String date;
        string heureDebut;
        string heureFin;
        int nbPlaces;
        string nomActivite;
        string nomAdministrateur;

        public seanceForm()
        {
            date = "2020-12-1";
            heureDebut = "12:20";
            heureFin = "12:50";
            nbPlaces = 1;
            nomActivite = "aucun";
            nomAdministrateur = "aucun";
        }

        public seanceForm(string date, string heureDebut, string heureFin, int nbPlaces, string nomActivite, string nomAdministrateur)
        {
            this.date = date;
            this.heureDebut = heureDebut;
            this.heureFin = heureFin;
            this.nbPlaces = nbPlaces;
            this.nomActivite = nomActivite;
            this.nomAdministrateur = nomAdministrateur;
        }

        public string Date { get { return date; } set { date = value; } }

        public string HeureDebut { get { return heureDebut; } set { heureDebut = value; } }

        public int NbPlaces { get { return nbPlaces; } set { nbPlaces = value; } }

        public string HeureFin { get { return heureFin; } set { heureFin = value; } }

        public string NomAdmin { get { return nomAdministrateur; } set { nomAdministrateur = value; } }

        public string NomActivite { get { return nomActivite; } set { nomActivite = value; } }


        public override string ToString()
        {
            return date + " " + heureDebut + " " + " " + heureFin + " " + nbPlaces +" "+nomAdministrateur+" "+nomActivite;
        }

    }
}
