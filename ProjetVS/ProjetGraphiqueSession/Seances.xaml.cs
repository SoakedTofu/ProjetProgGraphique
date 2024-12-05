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
                ContentDialog dialog1 = new ContentDialog();
                dialog1.XamlRoot = this.XamlRoot;
                dialog1.Title = "Réservation";
                dialog1.PrimaryButtonText = "Réserver";
                dialog1.CloseButtonText = "Annuler";
                dialog1.DefaultButton = ContentDialogButton.Primary;
                dialog1.Content = $"Voulez-vous vraiment réserver une place pour l'activité '{uneSeance.NomAct}' le {uneSeance.Date} à {uneSeance.HeureDebut}?";

                ContentDialogResult resultat = await dialog1.ShowAsync();

                if (resultat == ContentDialogResult.Primary)
                {
                    // Vérifier si l'utilisateur participe déjà à cette séance

                    if (Singleton.getInstance().VerifierSeanceAdherent(uneSeance.NomAct))
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.XamlRoot = this.XamlRoot;
                        dialog.Title = "Désolé";
                        dialog.PrimaryButtonText = "Ok";
                        dialog.DefaultButton = ContentDialogButton.Primary;
                        dialog.Content = "Vous êtes déjà inscrit à cette séance.";

                        ContentDialogResult resultat2 = await dialog.ShowAsync();
                    }
                    else    // Si l'adhérent n'est pas déjà inscrit
                    {
                        if (Singleton.getInstance().GetAdmin())
                        {
                            ContentDialog dialog2 = new ContentDialog();
                            dialog2.XamlRoot = this.XamlRoot;
                            dialog2.Title = "Désolé";
                            dialog2.PrimaryButtonText = "Ok";
                            dialog2.DefaultButton = ContentDialogButton.Primary;
                            dialog2.Content = "Utilsez un compte d'adherent.";
                            ContentDialogResult resultat2 = await dialog2.ShowAsync();


                        }
                        else
                        {
                            if(uneSeance.DateDB < new DateTimeOffset(DateTime.Today) && TimeSpan.Parse(uneSeance.HeureFin) < new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
                            {
                                ContentDialog dialog3 = new ContentDialog();
                                dialog3.XamlRoot = this.XamlRoot;
                                dialog3.Title = "Désolé";
                                dialog3.PrimaryButtonText = "Ok";
                                dialog3.DefaultButton = ContentDialogButton.Primary;
                                dialog3.Content = "La séance est terminé..!";
                                ContentDialogResult resultat2 = await dialog3.ShowAsync();

                            }
                            else
                            {
                                Singleton.getInstance().inscription(Singleton.getInstance().idSeanceAll(uneSeance),
                                                        Singleton.getInstance().GetUtilisateur());

                                ContentDialog dialog4 = new ContentDialog();
                                dialog4.XamlRoot = this.XamlRoot;
                                dialog4.Title = "Merci!";
                                dialog4.PrimaryButtonText = "Ok";
                                dialog4.DefaultButton = ContentDialogButton.Primary;
                                dialog4.Content = " Votre réservation a été éffectuer avec succè.\n Nous vous" +
                                    $" prions de bien vouloir vous présenter à parti votre activité de '{uneSeance.NomAct}' le {uneSeance.Date} à {uneSeance.HeureDebut}."+"\n\nÀ bientôt...";
                                ContentDialogResult resultat2 = await dialog4.ShowAsync();
                            }

                        
                        }
                    }
                }
                    

            }

            else
            {
                ContentDialog dialog5 = new ContentDialog();
                dialog5.XamlRoot = this.XamlRoot;
                dialog5.Title = "Désolé";
                dialog5.PrimaryButtonText = "Ok";
                dialog5.DefaultButton = ContentDialogButton.Primary;
                dialog5.Content = "Plus aucune place disponible.";

                ContentDialogResult resultat2 = await dialog5.ShowAsync();
            }



        }
    }
}
