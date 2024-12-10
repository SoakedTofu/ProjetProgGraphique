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

        private async void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
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
                            
                        Singleton.getInstance().SetTBUtilisateur(Singleton.getInstance().GetNomAdherent(tb_identification.Text));   // Mettre à jour les informations de connexions

                        args.Cancel = false;

                        SingletonNavigation.getInstance().VisibiliteConnexion(true);        // Cacher l'invitation de connexion, montre celle de déconnexion                       

                        Singleton.getInstance().SetConnecte(true);                          // Mettre à jour les variables de connexion
                        Singleton.getInstance().SetUtilisateur(tb_identification.Text);

                        SingletonNavigation.getInstance().ChangerNavigation();              // Remettre la navigation à la page d'accueil

                        SingletonNavigation.getInstance().VisibiliteSeance(true);           // Cache la page des séances

                    }

                    // Vérifier si c'est un admin

                    else if (Singleton.getInstance().VerififierAdmin(tb_identification.Text))
                    {
                        // Montrer le champ du mot de passe

                        sp_MDP.Visibility = Visibility.Visible;

                        this.tb_MDP.Focus(FocusState.Programmatic);     // Focus sur la textbox du mot de passe

                        // Si le mot de passe est entré ou non

                        if(String.IsNullOrWhiteSpace(tb_MDP.Password))
                        {
                            // Message pour demander le mot de passe

                            tbl_erreurMDP.Text = "Veuillez entrer votre mot de passe";
                        }

                        else
                        {
                            // Vérifier si les données de connexion sont exacts

                            if (Singleton.getInstance().VerififierConnexionAdmin(tb_identification.Text, tb_MDP.Password))
                            {
                                    
                                Singleton.getInstance().SetTBUtilisateur(tb_identification.Text);   // Mettre à jour les informations de connexions

                                args.Cancel = false;

                                SingletonNavigation.getInstance().VisibiliteConnexion(true);    // Cacher l'invitation de connexion, montre celle de déconnexion

                                Singleton.getInstance().SetConnecte(true);                      // Mettre à jour les variables de connexion
                                Singleton.getInstance().SetAdmin(true);
                                Singleton.getInstance().SetUtilisateur(tb_identification.Text);
                                   
                                SingletonNavigation.getInstance().VisibiliteAdmin(true);        // Montrer les pages exclusives à l'admin

                                SingletonNavigation.getInstance().ChangerNavigation();          // Remettre la navigation à la page d'accueil

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
                SingletonNavigation.getInstance().ChangerNavigation();
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


    }
}
