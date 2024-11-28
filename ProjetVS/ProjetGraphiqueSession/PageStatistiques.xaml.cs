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
    public sealed partial class PageStatistiques : Page
    {
        public PageStatistiques()
        {
            this.InitializeComponent();
        }

        // Générer les statistiques à chaque arrivée sur la page

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Nombre total d'adhérents

            tbl_nbTotalAdherents.Text = Singleton.getInstance().StatTotalAdherents();

            // Nombre total d'activités

            tbl_nbTotalActivites.Text = Singleton.getInstance().StatTotalActivites();

            // Nombre d'adhérents par activité

            tbl_nbAdherentsParActivite.Text = Singleton.getInstance().StatAdherentsParActivite();

            // Moyenne des notes d'appréciation par activité

            tbl_moyenneNote.Text = Singleton.getInstance().StatMoyenneNote();

            // Participant ayant le plus de séances

            tbl_participantPopulaire.Text = Singleton.getInstance().StatParticipantPopulaire();

            // Activité la mieux noté

            tbl_activitePopulaire.Text = Singleton.getInstance().StatActivitePopulaire();

            // L'activité avec le plus de séances

            tbl_ActivitePlusSeances.Text = Singleton.getInstance().StatActivitePlusSeances();

        }

    }
}
