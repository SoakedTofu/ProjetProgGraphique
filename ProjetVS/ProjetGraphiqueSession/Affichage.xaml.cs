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
    public sealed partial class Affichage : Page
    {
        Activite uneActivite;
        public Affichage()
        {
            this.InitializeComponent();
            liste_activites.ItemsSource = Singleton.getInstance().getListeActivites();
            uneActivite = new Activite("sport", 2);
        }

        // Pour gérer la NavigationView

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationViewItem navItem;

            foreach (var item in SingletonNavigation.getInstance().NavigationView.MenuItems)
            {
                navItem = item as NavigationViewItem;
                if (navItem.Name == "Affichage")
                {
                    SingletonNavigation.getInstance().NavigationView.SelectedItem = navItem;
                    break;
                }
            }
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = liste_activites.XamlRoot;
                dialog.Title = "Suppression";
                dialog.Content = "Voulez vous vraiment supprimer l'activité?";
                dialog.PrimaryButtonText = "Oui";
                // dialog.SecondaryButtonText = "Non";
                dialog.CloseButtonText = "Non";

                dialog.DefaultButton = ContentDialogButton.Close;

                var resultat = await dialog.ShowAsync();

                if (resultat == ContentDialogResult.Primary)
                {
                    Button btn = sender as Button;

                    uneActivite = btn.DataContext as Activite;

                    Singleton.getInstance().supprimerActivite(uneActivite);

                }
            
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            uneActivite = btn.DataContext as Activite;
            Frame.Navigate(typeof(ModifActivites), Singleton.getInstance().GetActiviteForm(uneActivite.Nom));
        }

        private void liste_activites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            uneActivite = liste_activites.SelectedItem as Activite;
            Frame.Navigate(typeof(Seances), uneActivite);
        }

        private async void btn_Exporter_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Singleton.getInstance().GetMainWindow());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "activites";
            picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();


            // La fonction ToString de la classe Client retourne: nom + ";" + prenom

            await Windows.Storage.FileIO.WriteLinesAsync(monFichier, Singleton.getInstance().GetActivites(), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }
    }
}
