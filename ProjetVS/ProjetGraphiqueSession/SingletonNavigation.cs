using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class SingletonNavigation
    {
        NavigationView navigationView;

        static SingletonNavigation instance;

        public static SingletonNavigation getInstance()
        {
            if (instance == null)
                instance = new SingletonNavigation();

            return instance;
        }

        public NavigationView NavigationView
        {
            get { return navigationView; }
            set { navigationView = value; }
        }

        // Pour gérer le retour à la page principale après avoir sélectionné un ContentDialog

        public void ChangerNavigation()
        {

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


        // Pour gérer la visibilité des pages connexion / déconnexion
        public void VisibiliteConnexion(bool connecte)
        {
            
            if (navigationView != null)
            {
                // Variables des items
                var ItemConnexion = (NavigationViewItem)navigationView.FindName("Connexion");
                var ItemDeconnexion = (NavigationViewItem)navigationView.FindName("Deconnexion");

                if (ItemConnexion != null)
                {
                    ItemConnexion.Visibility = connecte ? Visibility.Collapsed : Visibility.Visible;
                }

                if (ItemDeconnexion != null)
                {
                    ItemDeconnexion.Visibility = connecte ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        // Montrer / cacher les pages de l'administrateur

        public void VisibiliteAdmin(bool admin)
        {
            if (navigationView != null)
            {
                // Variables des items
                var ItemStatistiques = (NavigationViewItem)navigationView.FindName("Stats");

                if (ItemStatistiques != null)
                {
                    ItemStatistiques.Visibility = admin ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
