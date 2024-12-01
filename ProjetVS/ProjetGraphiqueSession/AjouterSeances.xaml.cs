using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AjouterSeances : Page
    {
        Seance uneSeance;
        seanceForm uneSeanceForm;
        ObservableCollection<Seance> listseance;
        public AjouterSeances()
        {
            this.InitializeComponent();
            uneSeance = new Seance();
            uneSeanceForm = new seanceForm();
            nomActivite.ItemsSource = Singleton.getInstance().getListeAllActivites();
            listseance = new ObservableCollection<Seance>();
            date.MinDate = new DateTimeOffset(DateTime.Today);
            date.Date = new DateTimeOffset(DateTime.Today);
            hrDebut.SelectedTime =new  TimeSpan (DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
            hrFin.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            hrFin.MinuteIncrement = 10;
            hrDebut.MinuteIncrement = 10;
        }

   

        private void ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (validation())
            {
                uneSeanceForm.Date = date.Date.Value.ToString("d");
                uneSeanceForm.HeureDebut =Convert.ToString( hrDebut.Time);
                uneSeanceForm.HeureFin =Convert.ToString( hrFin.Time);
                Activite seanceNomActivite = nomActivite.SelectedItem as Activite;
                uneSeanceForm.NomActivite = seanceNomActivite.Nom;
                Singleton.getInstance().ajouterSeance(uneSeanceForm);
                Frame.Navigate(typeof(AllSeances));
            }
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Affichage));
        }

       

        private bool validation()
        {
            bool valide = true;
            resetErreurs();


            if (hrFin.SelectedTime <= hrDebut.SelectedTime)
            {
                erreurHrFin.Text = "L'heure de fin de la s�ance doit �tre superieur � l'heure de d�but ";

                valide = false;
            }
            if (hrDebut.SelectedTime < new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) || hrFin.SelectedTime < new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
            {
                erreurHrFin.Text = "la s�ance doit �tre � venir ";

                valide = false;
            }
            if (nomActivite.SelectedIndex == -1)
            {
                erreurNomAct.Text = "Entrer le nom de l'activit�";

                valide = false;
            }

            return valide;

        }

        private void resetErreurs()
        {
         
           
            erreurHrFin.Text = string.Empty;
            erreurNomAct.Text = string.Empty;

        }
    }
}

