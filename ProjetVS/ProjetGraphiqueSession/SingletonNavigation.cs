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
    }
}
