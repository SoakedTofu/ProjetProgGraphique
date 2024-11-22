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
            mainFrame.Navigate(typeof(Affichage));
        }



        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            // Déterminer la page courrante et y naviguer seulement si elle est différente de la page sélectionnée

            var pageCourante = mainFrame.Content?.GetType();

            switch (item.Name)
            {
                case "Affichage":
                    if (pageCourante != typeof(Affichage))
                        mainFrame.Navigate(typeof(Affichage));
                    break;
                case "Stats":
                    //if (pageCourante != typeof(PageStats))
                    //    mainFrame.Navigate(typeof(PageStats));
                    break;
                default:
                    break;
            }
        }

        // Ouvre la boîte de dialogue de connexion

        private async void btn_Connexion_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogConnexion dialog = new ContentDialogConnexion();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.Title = "Conneztez-vous";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();

        }
    }
}
