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
            Frame.Navigate(typeof(ModifierSeance), uneSeance);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is not null)
            {
                activite = e.Parameter as Activite;
                liste_Seances.ItemsSource = Singleton.getInstance().getListeSeances(activite);
                titre.Text = "Séances de " + activite.Nom;
            }
            else
            {

            }
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Affichage));
        }
    }
}
