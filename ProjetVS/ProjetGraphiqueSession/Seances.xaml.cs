using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    public sealed partial class Seances : Page
    {
        Seance uneSeance;
        Activite activite;
        public Seances()
        {
            this.InitializeComponent();
            uneSeance = new Seance();
            activite = new Activite("sport", 2);
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            uneSeance = btn.DataContext as Seance;
            uneSeance.NomAct = activite.Nom;
            Frame.Navigate(typeof(ModifierSeance), uneSeance) ;
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = liste_Seances.XamlRoot;
            dialog.Title = "Suppression";
            dialog.Content = "Voulez vous vraiment supprimer la séance?";
            dialog.PrimaryButtonText = "Oui";
            // dialog.SecondaryButtonText = "Non";
            dialog.CloseButtonText = "Non";

            dialog.DefaultButton = ContentDialogButton.Close;

            var resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                Button btn = sender as Button;

                uneSeance = btn.DataContext as Seance;

                Singleton.getInstance().supprimerSeance(uneSeance,Singleton.getInstance().idSeance(uneSeance));

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is not null)
            {
                activite = e.Parameter as Activite;
                liste_Seances.ItemsSource = Singleton.getInstance().getListeSeances(activite);
                titre.Text = "Séances de " + activite.Nom;

                Singleton.getInstance().nomActiviteSeance(activite.Nom);

            }
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Affichage));
        }

        private async void btn_Inscription_Click(object sender, RoutedEventArgs e)
        {
            // Regarder si il reste des places ou non

            Button btn = sender as Button;

            uneSeance = btn.DataContext as Seance;

            if (uneSeance.NbPlaces != 0)    // S'il y a encore des place disponibles. Sinon, le boutton est désactivé par la propriété calculé PlacesDisponibles
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.XamlRoot;
                dialog.Title = "Réservation";
                dialog.PrimaryButtonText = "Réserver";
                dialog.CloseButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = $"Voulez-vous vraiment réserver une place pour l'activité '{uneSeance.NomAct}' le {uneSeance.Date} à {uneSeance.HeureDebut}?";

                ContentDialogResult resultat = await dialog.ShowAsync();

                if (resultat == ContentDialogResult.Primary)
                {
                    // Vérifier si l'utilisateur participe déjà à cette séance

                    if (!Singleton.getInstance().VerifierSeanceAdherent(uneSeance.NomAct))
                    {
                        ContentDialog dialog2 = new ContentDialog();
                        dialog.XamlRoot = this.XamlRoot;
                        dialog.Title = "Désolé";
                        dialog.PrimaryButtonText = "Ok";
                        dialog.DefaultButton = ContentDialogButton.Primary;
                        dialog.Content = "Vous êtes déjà inscrit à cette séance.";

                        ContentDialogResult resultat2 = await dialog.ShowAsync();
                    }
                    else    // Si l'adhérent n'est pas déjà inscrit
                    {

                    }
                }
                    

            }



        }
    }
}
