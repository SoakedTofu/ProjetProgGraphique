using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class Singleton
    {

        ObservableCollection<Activite> listeActivites;
        MySqlConnection con = new MySqlConnection
            ("Server=cours.cegep3r.info;Database=a2024_420335ri_eq2;Uid='2309444';Pwd='2309444';");

        static Singleton instance = null;

        TextBlock TB_Utilisateur;       // Pour montrer l'utilsateur dans l'inferface apès la connexion

        String utilisateur;             // Nom de l'utilisateur

        Boolean admin;                  // Pour savoir si l'utilisateur est un admin

        Boolean connecte;               // Pour savoir si un utilisateur est connecté

        public Singleton()
        {
            listeActivites = new ObservableCollection<Activite>();
            getListeActivites();
        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }

        public ObservableCollection<Activite> getListeActivites()
        {
            listeActivites.Clear();
            MySqlCommand commande = new MySqlCommand("AffActivite");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {


                //int id = (int)r["id"];

                Double note = Convert.ToDouble(r["moyenneNote"].ToString());
                string nom = r["NomActivite"].ToString();



                listeActivites.Add(new Activite(nom, note));
            }


            con.Close();
            return listeActivites;

        }

        public ObservableCollection<Activite> listeActivite()
        {
            return listeActivites;
        }


        // Fonction pour vérifier qu'un adhérent existe

        public Boolean VerifierAdherent(String utilisateur)
        {
            // Boolean qui indique si l'utilisateur est présent

            Boolean present = false;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT f_verifier_Adherent(@numeroUtil)";
                commande.Parameters.AddWithValue("@numeroUtil", utilisateur);

                con.Open();
                commande.Prepare();

                var resultat = commande.ExecuteScalar();

                // Vérifier que la commande renvoie bien un résultat

                if (resultat != null)
                {
                    present = Convert.ToBoolean(resultat);
                }


                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();   
            }

            return present;
        }

        // Fonction pour aller chercher le nom de l'adhérent

        public String GetNomAdherent(String utilisateur)
        {
            String NomComplet = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT f_get_Nom(@numeroUtil)";
                commande.Parameters.AddWithValue("@numeroUtil", utilisateur);

                con.Open();
                commande.Prepare();

                NomComplet = commande.ExecuteScalar().ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return NomComplet;
        }

        // Fonction pour vérifier le nom de l'admin

        public Boolean VerififierAdmin(String utilisateur)
        {
            // Boolean qui indique si l'utilisateur est présent

            Boolean present = false;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT f_verifier_admin(@numeroUtil)";
                commande.Parameters.AddWithValue("@numeroUtil", utilisateur);

                con.Open();
                commande.Prepare();

                var resultat = commande.ExecuteScalar();

                // Vérifier que la commande renvoie bien un résultat

                if (resultat != null)
                {
                    present = Convert.ToBoolean(resultat);
                }


                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return present;
        }

        // Fonction pour vérifier la connexion de l'admin

        public Boolean VerififierConnexionAdmin(String utilisateur, String MDP)
        {
            // Boolean qui indique si l'utilisateur est présent

            Boolean present = false;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT f_verifier_admin_MDP(@numeroUtil, @MDP)";
                commande.Parameters.AddWithValue("@numeroUtil", utilisateur);
                commande.Parameters.AddWithValue("@MDP", MDP);

                con.Open();
                commande.Prepare();

                var resultat = commande.ExecuteScalar();

                // Vérifier que la commande renvoie bien un résultat

                if (resultat != null)
                {
                    present = Convert.ToBoolean(resultat);
                }


                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return present;
        }

        // Fonction qui réfère le textblock de connexion

        public void SetTextblock(TextBlock tb)
        {
            TB_Utilisateur = tb;
        }

        // Fonction qui change le nom de connection dans l'interface

        public void SetTBUtilisateur(String nom)
        {
            TB_Utilisateur.Text = nom;
        }

        // Variable utilsateur

        public void SetUtilisateur(String utilisateur2)
        {
            utilisateur = utilisateur2;
        }

        public String GetUtilisateur()
        {
            return utilisateur;
        }

        // Variable connecté

        public void SetConnecte(bool valeur)
        {
            connecte = valeur;
        }

        public bool GetConnecte()
        {
            return connecte;
        }

        // Variable admin

        public void SetAdmin(bool valeur)
        {
            admin = valeur;
        }

        public bool GetAdmin()
        {
            return admin;
        }

        // Procédures des statistiques

        public String StatTotalAdherents()  // Nombre total d'adhérents
        {

            string stat = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "NbTotalAdherents";
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                commande.Prepare();
                stat = commande.ExecuteScalar().ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return stat;
        }

        public String StatTotalActivites()  // Nombre total d'activités
        {

            string stat = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "NbTotalActivites";
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                commande.Prepare();
                stat = commande.ExecuteScalar().ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return stat;
        }

        public String StatAdherentsParActivite()  // Nombre d'adhérents par activité
        {

            string stat = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "NbAdherentsParActivite";
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();

                while(r.Read())
                {
                    stat += r[0] + ": " + r[1] + "\n";
                }


                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return stat;
        }
        
        public String StatMoyenneNote()  // Moyenne des notes d'appréciation par activité
        {

            string stat = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "AffActivite";
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    stat += r[0] + ": " + r[1] + "\n";
                }


                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return stat;
        }


    }
}
