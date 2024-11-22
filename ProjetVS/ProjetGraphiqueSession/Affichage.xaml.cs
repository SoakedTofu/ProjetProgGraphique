﻿using Microsoft.UI.Xaml;
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
    public sealed partial class Affichage : Page
    {
        public Affichage()
        {
            this.InitializeComponent();
            liste_activites.ItemsSource = Singleton.getInstance().listeActivite();
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

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
