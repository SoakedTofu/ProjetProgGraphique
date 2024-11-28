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

                        ContentDialogConnexion dialog = new ContentDialogConnexion();
                        dialog.XamlRoot = this.Content.XamlRoot;
                        dialog.Title = "Conneztez-vous";
                        dialog.PrimaryButtonText = "Se connecter";
                        dialog.CloseButtonText = "Annuler";
                        dialog.DefaultButton = ContentDialogButton.Primary;

                        ContentDialogResult resultat = await dialog.ShowAsync();
                        break;
                    case "Deconnexion":
                        // Mettre à jour les informations de connexions

                        Singleton.getInstance().SetTBUtilisateur("");       // Enlever le nom de l'interface

                        SingletonNavigation.getInstance().VisibiliteConnexion(false);       // Montrer l'invitation à se connecter, cacher celle de déconnexion

                        // Réinitialiser les valeurs de connexion

                        Singleton.getInstance().SetConnecte(false);
                        Singleton.getInstance().SetAdmin(false);
                        Singleton.getInstance().SetUtilisateur("");

                        // Cacher les pages de l'administrateur

                        SingletonNavigation.getInstance().VisibiliteAdmin(false);

                        // Mettre à jour la navigation

                        SingletonNavigation.getInstance().ChangerNavigation();
                        break;
                    case "Stats":
                        if (pageCourante != typeof(PageStatistiques))
                            mainFrame.Navigate(typeof(PageStatistiques));
                        break;
                    default:
                        break;
                }
            }

        }

    }
}
