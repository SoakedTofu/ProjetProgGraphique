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

        private void edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btn_Exporter_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Singleton.getInstance().GetMainWindow());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "activites";
            picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            var listeAdherents = Singleton.getInstance().getListeAdherentsCSV();

            // La fonction ToString de la classe Client retourne: nom + ";" + prenom

            if (monFichier != null && listeAdherents != null && listeAdherents.Count > 0)
            {
                // CSV Header
                var header = "numeroIdentification,nom,prenom,adresse,dateNaissance,age,NomAdministrateur";         // Pour les entêtes       

                var adherentsString = listeAdherents.Select(x => x.ToStringCSV()).ToList();                        // Exporter la liste en strings

                adherentsString.Insert(0, header);

                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, adherentsString, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }
        }
    }
}
