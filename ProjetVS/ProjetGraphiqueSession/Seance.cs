using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class Seance:INotifyPropertyChanged
    {

        String date;
        string heureDebut;
        string heureFin;
        int nbPlaces;
        string nomAct;
        DateTime dateDB;
        DateTime heureDebutDB;
        DateTime heureFinDB;


        public Seance()
        {
            DateDB = DateTime.Parse("2020-12-1");
            heureDebutDB = DateTime.Parse("12:20");
            heureFinDB = DateTime.Parse("12:20");
            nbPlaces = 1;
        }
        public Seance(string date, string heureDebut, string heureFin, int nbPlaces)
        {
            DateDB = DateTime.Parse(date);
            HeureDebutDB = DateTime.Parse(heureDebut); 
            HeureFinDB = DateTime.Parse(heureFin); 
            this.nbPlaces = nbPlaces;
        }

        // Pour l'exportation de la liste complète des séances
        public Seance(string date, string heureDebut, string heureFin, int nbPlaces,string nomAct)
        {
            DateDB = DateTime.Parse(date);
            HeureDebutDB = DateTime.Parse(heureDebut);
            HeureFinDB = DateTime.Parse(heureFin); 
            this.nbPlaces = nbPlaces;
            this.nomAct = nomAct;
        }

        // Propriété lié à Date qui se met à jour quand l'autre change
        public DateTime DateDB
        {
            get => dateDB;
            set
            {
                if (dateDB != value)
                {
                    dateDB = value;
                    OnPropertyChanged(nameof(DateDB));
                    OnPropertyChanged(nameof(Date)); 
                }
            }
        }

        public string Date
        {
            get => DateDB.ToString("d MMM yyyy");
            set
            {
                if (Date != value)
                {
                    DateDB = DateTime.Parse(value); 
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string HeureDebut
        {
            get => HeureDebutDB.ToString("HH:mm"); // Pour les signes AM et PM
            set
            {
                if (HeureDebut != value)
                {
                    HeureDebutDB = DateTime.Parse(value); 
                    OnPropertyChanged(nameof(HeureDebut));
                }
            }
        }

        public string HeureFin
        {
            get => HeureFinDB.ToString("HH:mm"); 
            set
            {
                if (HeureFin != value)
                {
                    HeureFinDB = DateTime.Parse(value);
                    OnPropertyChanged(nameof(HeureFin));
                }
            }
        }

        public DateTime HeureDebutDB
        {
            get => heureDebutDB;
            set
            {
                if (heureDebutDB != value)
                {
                    heureDebutDB = value;
                    OnPropertyChanged(nameof(HeureDebutDB));
                    OnPropertyChanged(nameof(HeureDebut));
                }
            }
        }

        public DateTime HeureFinDB
        {
            get => heureFinDB;
            set
            {
                if (heureFinDB != value)
                {
                    heureFinDB = value;
                    OnPropertyChanged(nameof(HeureFinDB));
                    OnPropertyChanged(nameof(HeureFin));
                }
            }
        }


        public int NbPlaces { get { return nbPlaces; } set { nbPlaces = value; } }

        public string NomAct { get { return nomAct; } set { nomAct = value; } }


        public override string ToString()
        {
            return date+" "+heureDebut+" "+" "+heureFin+" "+nbPlaces;
        }

        // Pour gérer la visiblité du bouton de réservation
        public bool PlacesDisponibles => NbPlaces > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
