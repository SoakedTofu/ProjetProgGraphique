using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.DateTimeFormatting;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetGraphiqueSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutsAdherents : Page
    {
        Adherent unAdherent;
        public AjoutsAdherents()
        {
            this.InitializeComponent();
            unAdherent = new Adherent();
          date.MaxDate = new DateTimeOffset(new DateTime(  (DateTime.Today).Year-18,12,31));
            date.Date = new DateTimeOffset(DateTime.Today);
        }

        private void ajouter_Click_1(object sender, RoutedEventArgs e)
        {
            if (validation())
            {
                unAdherent.Nom = nom.Text;
                unAdherent.Prenom = prenom.Text;
                unAdherent.Adresse = Adresse.Text;
                unAdherent.DateNaissance = date.Date.Value.ToString("d");
                Singleton.getInstance().ajouterAdherent(unAdherent);
                Frame.Navigate(typeof(Adherents));
            }
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Adherents));
        }

        

        private bool validation()
        {
            bool valide = true;
            resetErreurs();
            if (nom.Text.Trim() == String.Empty)
            {
                erreurNom.Text = "Entrer le nom de l'adhérent";

                valide = false;
            }
            if (prenom.Text.Trim() == String.Empty)
            {
                erreurPrenom.Text = "Entrer le prénom de l'adhérent";

                valide = false;
            }
            if (Adresse.Text.Trim() == String.Empty)
            {
                erreurAdresse.Text = "Entrer l'adresse de l'adhérent";

                valide = false;
            }
           

            return valide;

        }

        private void resetErreurs()
        {
            erreurNom.Text = string.Empty;
            erreurPrenom.Text = string.Empty;
            erreurPrenom.Text = string.Empty;
            erreurAdresse.Text = string.Empty;
           

        }

      
    }
}
