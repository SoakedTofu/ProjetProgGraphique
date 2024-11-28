using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetGraphiqueSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifierSeance : Page
    {
        Seance uneSeance;
        seanceForm uneSeanceForm;
        ObservableCollection<Seance> listseance;
        public ModifierSeance()
        {
            this.InitializeComponent();
            uneSeance = new Seance();
            uneSeanceForm = new seanceForm();
            nomActivite.ItemsSource= Singleton.getInstance().getListeActivites();
            listseance= new ObservableCollection<Seance>();
           
        }

        private void modifier_Click(object sender, RoutedEventArgs e)
        {
            if (validation())
            {
                uneSeanceForm.Date = date.Text;
                uneSeanceForm.HeureDebut = hrDebut.Text;
                uneSeanceForm.HeureFin = hrFin.Text;
                Seance seanceNbP= nbPlace.SelectedItem as Seance;
                uneSeanceForm.NbPlaces = seanceNbP.NbPlaces ;
                Activite seanceNomActivite=nomActivite.SelectedItem as Activite;
                uneSeanceForm.NomActivite=seanceNomActivite.Nom;
                Singleton.getInstance().modifierSeance(uneSeanceForm,Singleton.getInstance().idSeance(uneSeance));
                Frame.Navigate(typeof(Affichage));
            }
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is not null)
            {
                uneSeance = e.Parameter as Seance;
                date.Text = uneSeance.Date;
                hrDebut.Text = Convert.ToString(uneSeance.HeureDebut);
                hrFin.Text = Convert.ToString(uneSeance.HeureFin);
                listseance.Add(uneSeance);
                nbPlace.ItemsSource =listseance ;

            }
            else
            {

            }
        }

        private bool validation()
        {
            bool valide = true;
            resetErreurs();
            if (date.Text.Trim() == String.Empty)
            {
                erreurDate.Text = "Entrer la date de la séance";

                valide = false;
            }
            if (hrDebut.Text.Trim() == String.Empty)
            {
                erreurHrDebut.Text = "Entrer l'heure de début de la séance";

                valide = false;
            }
            if (hrFin.Text.Trim() == String.Empty)
            {
                erreurHrFin.Text = "Entrer l'heure de fin de la séance";

                valide = false;
            }
            if (nbPlace.SelectedIndex==-1)
            {
                erreurNbPlace.Text = "Entrer le nombre de place disponible de la séance";

                valide = false;
            }
            if (nomActivite.SelectedIndex==-1)
            {
                erreurNomAct.Text = "Entrer le nom de l'activité";

                valide = false;
            }

            return valide;

        }

        private void resetErreurs()
        {
            erreurDate.Text = string.Empty;
            erreurHrDebut.Text = string.Empty;
            erreurHrFin.Text = string.Empty;
            erreurNbPlace.Text = string.Empty;
            erreurNomAct.Text = string.Empty;

        }
    }
}
