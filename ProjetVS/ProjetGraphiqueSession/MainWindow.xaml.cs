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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            SingletonNavigation.getInstance().NavigationView = navView;     // Assigner l'attribut de la NavigationView
            mainFrame.Navigate(typeof(Affichage));      // Naviguer à la page d'affichage
            Singleton.getInstance().SetTextblock(tbl_usager);   // Pour afficher l'utilisateur connecté
            Singleton.getInstance().SetMainWindow(this);    // Assigner la mainwindow
        }

        private async void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            // Déterminer la page courrante et y naviguer seulement si elle est différente de la page sélectionnée

            var pageCourante = mainFrame.Content?.GetType();

            if (item != null)   // Pour gérer les cas où le SelectedItem est mis à null
            {
                switch (item.Name)
                {
                    case "Affichage":
                        if (pageCourante != typeof(Affichage))
                            mainFrame.Navigate(typeof(Affichage));
                        break;
                    case "Connexion": // Pour la boîte de dialogue

                        if(!Singleton.getInstance().GetConnecte())
                        {
                            DialogConnexion();                            
                        }
                        else
                        {                            
                            // Demander si l'utilisateur veut déconnecter celui connecté

                            ContentDialog dialog = new ContentDialog();
                            dialog.XamlRoot = Singleton.getInstance().GetMainWindow().Content.XamlRoot;
                            dialog.Title = "Déconnecter l'utilisateur actuel";
                            dialog.PrimaryButtonText = "Oui";
                            dialog.CloseButtonText = "Non";
                            dialog.Content = "Un utilisateur est présentement connecté sur cette machine. Voulez-vous le déconnecter?";
                            dialog.DefaultButton = ContentDialogButton.Primary;

                            ContentDialogResult resultat = await dialog.ShowAsync();

                            if (resultat == ContentDialogResult.Primary)
                            {
                                FonctionDeconnexion();      // Déconnecter l'usager et réinitialiser les variables
                                SingletonNavigation.getInstance().ChangerNavigation();      // Changer la navigation vers la page principale
                                DialogConnexion();
                            }
                            else
                            {
                                SingletonNavigation.getInstance().ChangerNavigation();
                            }                           
                        }
                        break;
                    case "Deconnexion":

                        FonctionDeconnexion();
                        mainFrame.Navigate(typeof(Affichage), null);

                        break;
                    case "Stats":
                        if (pageCourante != typeof(PageStatistiques))
                            mainFrame.Navigate(typeof(PageStatistiques));
                        break;
                    case "comptes":
                        if (pageCourante != typeof(Adherents))
                            mainFrame.Navigate(typeof(Adherents));
                        break;
                    case "activite":
                        if (pageCourante != typeof(Activites))
                            mainFrame.Navigate(typeof(Activites));
                        break;
                    case "seance":
                        if (pageCourante != typeof(AllSeances))
                            mainFrame.Navigate(typeof(AllSeances));
                        break;
                    case "NoteSeances":
                        if (pageCourante != typeof(PageNotesSeances))
                            mainFrame.Navigate(typeof(PageNotesSeances));
                        break;
                    default:
                        break;
                }
            }

        }

        private void FonctionDeconnexion()
        {
            Singleton.getInstance().SetTBUtilisateur("");       // Enlever le nom de l'interface

            SingletonNavigation.getInstance().VisibiliteConnexion(false);       // Montrer l'invitation à se connecter, cacher celle de déconnexion

            Singleton.getInstance().SetConnecte(false);                 // Réinitialiser les valeurs de connexion
            Singleton.getInstance().SetAdmin(false);
            Singleton.getInstance().SetUtilisateur("");

            SingletonNavigation.getInstance().VisibiliteAdmin(false);   // Cacher les pages de l'administrateur                      

            SingletonNavigation.getInstance().ChangerNavigation();      // Mettre à jour la navigation

            SingletonNavigation.getInstance().VisibiliteSeance(false);   // Cache la page des séances
        }

        private async void DialogConnexion()
        {
            ContentDialogConnexion dialog = new ContentDialogConnexion();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.Title = "Conneztez-vous";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();

            mainFrame.Navigate(typeof(Affichage), null);
        }
    }
}
