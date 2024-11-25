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

        // Pour gérer la visibilité des pages connexion / déconnexion
        public void VisibiliteConnexion(bool connecte)
        {
            
            if (navigationView != null)
            {
                // Access the Connexion item by its name
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
    }
}
