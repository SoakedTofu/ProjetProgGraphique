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
    public sealed partial class Adherents : Page
    {
        Adherent unAdherent;
        public Adherents()
        {
            this.InitializeComponent();
            unAdherent = new Adherent();
            liste_adherents.ItemsSource = Singleton.getInstance().getListeAdherents();
        }

        private void btn_Exporter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            unAdherent = btn.DataContext as Adherent;
            Frame.Navigate(typeof(ModifierAdherants), unAdherent);

        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = liste_adherents.XamlRoot;
            dialog.Title = "Suppression";
            dialog.Content = "Voulez vous vraiment supprimer l'adhérent?";
            dialog.PrimaryButtonText = "Oui";
            // dialog.SecondaryButtonText = "Non";
            dialog.CloseButtonText = "Non";

            dialog.DefaultButton = ContentDialogButton.Close;

            var resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                Button btn = sender as Button;

                unAdherent = btn.DataContext as Adherent;

                Singleton.getInstance().supprimerAdherent(unAdherent);

            }
        }
    }
}
