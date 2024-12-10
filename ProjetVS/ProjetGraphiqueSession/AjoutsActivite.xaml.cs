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
    public sealed partial class AjoutsActivite : Page
    {
        ActiviteForm activite; double prixOrg1; double prixVente1; int nbPlaceMaxi1; string nomAct;
        public AjoutsActivite()
        {
            this.InitializeComponent();
            activite = new ActiviteForm("aucune", 2, 2, "admin_unique", 2);

        }

      
        private async void ajouter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (validation())
            {
                activite.Nom = nom.Text;
                activite.PrixOrg = Convert.ToDouble(prixOrg.Text);
                activite.PrixVente = Convert.ToDouble(prixVente.Text);
                activite.NbPlacesMaxi = Convert.ToInt32(nbPlacesMax.Text);
                activite.NomAdmin = nomAdmin.SelectedItem as string;
                Singleton.getInstance().ajouterActivite(activite);
                ContentDialog dialog = new ContentDialog();

                dialog.XamlRoot = ajouter.XamlRoot;
                dialog.Title = "Succes";
                dialog.Content = "L'activité " + activite.Nom+ " a été ajouté avec succè...!";

                dialog.PrimaryButtonText = "Ok";

                // dialog.SecondaryButtonText = "Non";
                

                dialog.DefaultButton = ContentDialogButton.Close;

                var resultat = await dialog.ShowAsync();

                if (resultat == ContentDialogResult.Primary)
                {
                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                }
                
            }
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private bool validation()
        {
            bool valide = true;
            resetErreurs();
            if (nom.Text.Trim() == String.Empty)
            {
                erreurNom.Text = "Entrer l'activité";

                valide = false;
            }
            if (double.TryParse(prixOrg.Text.Trim(), out prixOrg1) == false)
            {
                erreurPrixOrg.Text = "*Veuillez entrer une valeur numérique";
                valide = false;
            }
            else if (prixOrg1 < 0)
            {
                erreurPrixOrg.Text = "*Le prix ne peut etre négatif";
                valide = false;
            }
            if (double.TryParse(prixVente.Text.Trim(), out prixVente1) == false)
            {
                erreurPrixVente.Text = "*Veuillez entrer une valeur numérique";
                valide = false;
            }
            else if (prixVente1 < 0)
            {
                erreurPrixVente.Text = "*Le prix ne peut etre négatif";
                valide = false;
            }
           if(erreurPrixVente.Text.Trim()=="" && erreurPrixOrg.Text.Trim()=="")
            {
                if (prixOrg1 > prixVente1) {
                    erreurPrixVente.Text = "*Le prix de vente ne peut etre inférieur au prix d'organisation";
                    valide = false;
                }
            }

            if (int.TryParse(nbPlacesMax.Text.Trim(), out nbPlaceMaxi1) == false)
            {
                erreurNbPlacesMax.Text = "*Veuillez entrer une valeur numérique";
                valide = false;
            }
            else if (nbPlaceMaxi1 < 0)
            {
                erreurNbPlacesMax.Text = "*Le nombre de places ne peut etre négatif";
                valide = false;
            }
            return valide;

        }

        private void resetErreurs()
        {
            erreurNom.Text = string.Empty;
            erreurPrixOrg.Text = string.Empty;
            erreurPrixVente.Text = string.Empty;
            erreurNbPlacesMax.Text = string.Empty;

        }
    }
}

