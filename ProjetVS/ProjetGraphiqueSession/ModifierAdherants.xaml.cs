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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetGraphiqueSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifierAdherants : Page
    {
        Adherent unAdherent;
        public ModifierAdherants()
        {
            this.InitializeComponent();
            unAdherent = new Adherent();
            date.MaxDate = new DateTimeOffset(new DateTime((DateTime.Today).Year - 18, 12, 31));
            date.Date = new DateTimeOffset(DateTime.Today);
        }

        private async void modifier_Click(object sender, RoutedEventArgs e)
        {
            if (validation())
            {
                unAdherent.Nom = nom.Text;
                unAdherent.Prenom = prenom.Text;
                unAdherent.Adresse = Adresse.Text;
                unAdherent.DateNaissance = date.Date.Value.ToString("d");
                Singleton.getInstance().modifierAdherent(unAdherent);


                ContentDialog dialog = new ContentDialog();

                dialog.XamlRoot = modifier.XamlRoot;
                dialog.Title = "Succè";
                dialog.Content = "L'adhérent " + unAdherent.Nom + " a été modifier avec succè...!";

                dialog.PrimaryButtonText = "Ok";

                // dialog.SecondaryButtonText = "Non";


                dialog.DefaultButton = ContentDialogButton.Close;

                var resultat = await dialog.ShowAsync();

                if (resultat == ContentDialogResult.Primary)
                {
                    Frame.Navigate(typeof(Adherents));
                }
               
            }
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Adherents));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is not null)
            {
                unAdherent = e.Parameter as Adherent;
                nom.Text = unAdherent.Nom;
                prenom.Text = unAdherent.Prenom;
                Adresse.Text = unAdherent.Adresse;
                date.Date =new DateTimeOffset( Convert.ToDateTime( unAdherent.DateNaissance));


            }
            else
            {

            }
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
