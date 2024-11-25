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
    public sealed partial class ContentDialogConnexion : ContentDialog
    {
        public ContentDialogConnexion()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            // Arrêter la fermeture par défaut

            args.Cancel = true;


            ReinitialiserContentDialog();

            if (args.Result == ContentDialogResult.Primary)
            {

                // Vérifier si le champs est remplis

                if (!String.IsNullOrWhiteSpace(tb_identification.Text))
                {

                    // Vérifier si le numéro d'identification appartient à un adhérent

                    if (Singleton.getInstance().VerifierAdherent(tb_identification.Text))
                    {
                        // Mettre à jour les informations de connexions

                        Singleton.getInstance().SetTBUtilisateur(Singleton.getInstance().GetNomAdherent(tb_identification.Text));

                        args.Cancel = false;

                        SingletonNavigation.getInstance().VisibiliteConnexion(true);

                        ChangerNavigation();

                    }

                    // Vérifier si c'est un admin

                    else if (Singleton.getInstance().VerififierAdmin(tb_identification.Text))
                    {
                        // Montrer le champ du mot de passe

                        sp_MDP.Visibility = Visibility.Visible;

                        // Si le mot de passe est entré ou non

                        if(String.IsNullOrWhiteSpace(tb_MDP.Text))
                        {
                            // Message pour demander le mot de passe

                            tbl_erreurMDP.Text = "Veuillez entrer votre mot de passe";
                        }

                        else
                        {
                            // Vérifier si les données de connexion sont exacts

                            if (Singleton.getInstance().VerififierConnexionAdmin(tb_identification.Text, tb_MDP.Text))
                            {
                                // Mettre à jour les informations de connexions

                                Singleton.getInstance().SetTBUtilisateur(tb_identification.Text);

                                args.Cancel = false;

                                SingletonNavigation.getInstance().VisibiliteConnexion(true);

                                ChangerNavigation();
                            }

                            else
                            {
                                tbl_erreurMDP.Visibility = Visibility.Visible;
                                tbl_erreurMDP.Text = "Le mot de passe est incorrect";
                            }
                        }
                    }

                    else
                    {
                        tbl_erreurIdentification.Visibility = Visibility.Visible;
                        tbl_erreurIdentification.Text = "Le numéro d'utilisateur est incorrect";
                    }
                }

                else
                {
                    tbl_erreurIdentification.Visibility = Visibility.Visible;
                    tbl_erreurIdentification.Text = "Ce champ est obligatoire";
                }

            }
            else
            {
                args.Cancel = false;
                ChangerNavigation();

            }

        }

        // Réinitialiser les champs du ContentDialog

        public void ReinitialiserContentDialog()
        {
            tbl_erreurIdentification.Text = "";
            tbl_erreurMDP.Text = "";
            tbl_erreurIdentification.Visibility = Visibility.Collapsed;
            sp_MDP.Visibility = Visibility.Collapsed;
        }

        // Pour changer la NavigationView lors de la fermeture du ContentDialog
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
    }
}
