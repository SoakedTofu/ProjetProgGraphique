using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class Seance
    {

        String date;
        string heureDebut;
        string heureFin;
        int nbPlaces;
        string nomAct;


        public Seance()
        {
            date = "2020-12-1";
            heureDebut = "12:20";
            heureFin = "12:50";
            nbPlaces = 1;
        }
        public Seance(string date, string heureDebut, string heureFin, int nbPlaces)
        {
            this.date = date;
            this.heureDebut = heureDebut;
            this.heureFin = heureFin;
            this.nbPlaces = nbPlaces;
        }

        public Seance(string date, string heureDebut, string heureFin, int nbPlaces,string nomAct)
        {
            this.date = date;
            this.heureDebut = heureDebut;
            this.heureFin = heureFin;
            this.nbPlaces = nbPlaces;
            this.nomAct = nomAct;
        }

        public string Date { get { return date; }  set { date = value; } }

        public string HeureDebut { get { return heureDebut; } set {heureDebut=value; } }

        public int NbPlaces { get { return nbPlaces; } set { nbPlaces = value; } }

        public string HeureFin { get { return heureFin; } set { heureFin=value; } }



        public string NomAct { get { return nomAct; } set { nomAct = value; } }


        public override string ToString()
        {
            return date+" "+heureDebut+" "+" "+heureFin+" "+nbPlaces;
        }


    }
}
