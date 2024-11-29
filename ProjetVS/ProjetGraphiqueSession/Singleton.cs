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
        ObservableCollection<Seance> listeSeances;
        ObservableCollection<Adherent> listeAdherents;
        MySqlConnection con = new MySqlConnection
            ("Server=cours.cegep3r.info;Database=a2024_420335ri_eq2;Uid='2309444';Pwd='2309444';");

        static Singleton instance = null;

        TextBlock TB_Utilisateur;       // Pour montrer l'utilsateur dans l'inferface apès la connexion

        String utilisateur;             // Nom de l'utilisateur

        Boolean admin;                  // Pour savoir si l'utilisateur est un admin

        Boolean connecte;               // Pour savoir si un utilisateur est connecté

        string nomActivite;

        ObservableCollection<String> exportActivites = new ObservableCollection<String>(); // Pour exporter les activités

        MainWindow window;

        public Singleton()
        {
            listeActivites = new ObservableCollection<Activite>();
            listeSeances = new ObservableCollection<Seance>();
            listeAdherents = new ObservableCollection<Adherent>();
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
                Double note = 0;

                //int id = (int)r["id"];
                if (r["moyenneNote"] == null)
                {
                    note = 0;
                }
                else
                {
                        note = Convert.ToDouble(r["moyenneNote"].ToString());
                }
                string nom = r["NomActivite"].ToString();



                listeActivites.Add(new Activite(nom, note));
            }


            con.Close();
            return listeActivites;

        }

        public ObservableCollection<Adherent> getListeAdherents()
        {
            listeActivites.Clear();
            MySqlCommand commande = new MySqlCommand("AffAdherent");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Double note = 0;

                //int id = (int)r["id"];
              
                string nom = r["nom"].ToString();
                string prenom = r["prenom"].ToString();
                string adresse = r["adresse"].ToString();
                string dateNaissance = r["dateNaissance"].ToString();
                string age = r["age"].ToString();



                listeAdherents.Add(new Adherent(nom, prenom,adresse,dateNaissance,age));
            }


            con.Close();
            return listeAdherents;

        }

        public ObservableCollection<Seance> getListeSeances(Activite uneActivite)
        {
            listeSeances.Clear();
            MySqlCommand commande = new MySqlCommand("AffSeance");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            commande.Parameters.AddWithValue("nomAct", uneActivite.Nom);
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {



                
                string date = r["date"].ToString();
                string heureDebut = r["heureDebut"].ToString();
                string heureFin = r["heureFin"].ToString();
                int  nbPlaces =Convert.ToInt32( r["nbPMax"].ToString());


                listeSeances.Add(new Seance(date, heureDebut,heureFin,nbPlaces));
            }


            con.Close();
            return listeSeances;

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

        //methode qui recupere les attributs d'une activite

        public ActiviteForm GetActiviteForm(string nomActivite)
        {
            ActiviteForm uneActivite=new ActiviteForm("aucun",1,1, "admin_unique",1);

            MySqlCommand commande = new MySqlCommand("Activite");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            commande.Parameters.AddWithValue("nomAct", nomActivite);
            con.Open();
            commande.Prepare();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {


                //int id = (int)r["id"];
                string nom = r["nom"].ToString();
                Double prixOrg = Convert.ToDouble(r["prixOrganisation"].ToString());
                Double prixVente = Convert.ToDouble(r["prixVente"].ToString());
                string nomAdmin = r["nomAdministrateur"].ToString();
                int nbPlacesMaxi = Convert.ToInt32(r["nbPlacesMax"].ToString());


                uneActivite=new ActiviteForm(nom, prixOrg, prixVente, nomAdmin, nbPlacesMaxi);
            }


            con.Close();
            return uneActivite;
        }


        //methode qui modifie une activité

        public void modifierActivite(ActiviteForm activiteForm, string nomActPrec)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("ModifActivite");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("nomAct", activiteForm.Nom);
                commande.Parameters.AddWithValue("prixOrg", activiteForm.PrixOrg);
                commande.Parameters.AddWithValue("prixVt", activiteForm.PrixVente);
                commande.Parameters.AddWithValue("nomAdmin", activiteForm.NomAdmin);
                commande.Parameters.AddWithValue("nbPlaces", activiteForm.NbPlacesMaxi);
                commande.Parameters.AddWithValue("nomActPrec", nomActPrec);
                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
            }


        }

        //Methode qui stocke le nom de l'activité de la seance

        public void nomActiviteSeance(string nomAct)
        {
            nomActivite = nomAct;
        }

        //methode qui permet de recupérer l'id d'une seance

        public int idSeance(Seance uneSeance)
        {
            int idSeance = 1;
            try
            {
                MySqlCommand commande = new MySqlCommand("idSeance");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("dateForm", uneSeance.Date);
                commande.Parameters.AddWithValue("hrDebut", uneSeance.HeureDebut);
                commande.Parameters.AddWithValue("hrFin", uneSeance.HeureFin);
                commande.Parameters.AddWithValue("nomAct", nomActivite);
           

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {


                 
                    idSeance = Convert.ToInt32(r["idSeance"].ToString());


               
                }
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
            }

            return idSeance;

          
        }

        //methode qui modifie une seance

        public void modifierSeance(seanceForm uneSeance, int id)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("ModifSeance");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("dateForm", uneSeance.Date);
                commande.Parameters.AddWithValue("hrDebut", uneSeance.HeureDebut);
                commande.Parameters.AddWithValue("hrFin", uneSeance.HeureFin);
                commande.Parameters.AddWithValue("nomAct", uneSeance.NomActivite);
                commande.Parameters.AddWithValue("id", id);
                
                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
            }


        }

        //Methode suppression des activités

        public void supprimerActivite( Activite uneActivite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("SuppActivite");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("nomAct", uneActivite.Nom);

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            listeActivites.Remove(uneActivite);
        }

        //Procédure qui permet de supprimer une seance
        public void supprimerSeance( Seance uneSeance,int idSeance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("SuppSeance");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("id", idSeance);

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            listeSeances.Remove(uneSeance);

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


        public String StatParticipantPopulaire()  // Participant suivant le plus d'activite 
        {

            string stat = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "ParticipantPopulaire";
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

        public String StatActivitePopulaire()  // Activite la mieux notée
        {

            string stat = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "ActiviteMieuxNote";
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

        public String StatActivitePlusSeances()  // Activite avec le plus de séances
        {

            string stat = "";

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SeancePopulaire";
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

        // Fonction qui retourne la collection des activités

        public ObservableCollection<String> GetActivites()
        {
            exportActivites.Clear();

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "ListeActivites";
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    exportActivites.Add(r[0].ToString());
                }

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return exportActivites;
        }

        // Pour gérer la mainwindow

        public void SetMainWindow(MainWindow mainwindow)
        {
            window = mainwindow;
        }

        public MainWindow GetMainWindow()
        {
            return window;
        }

        // Pour vérifier si quelqu'un est connecté au programme

        public bool VerifierConnecte()
        {
            bool connecte = false;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "VerifierConnecte";
                commande.CommandType = System.Data.CommandType.StoredProcedure;


                con.Open();
                commande.Prepare();
                var resultat = commande.ExecuteScalar();

                // Vérifier que la commande renvoie bien un résultat

                if (resultat != null)
                {
                    connecte = Convert.ToBoolean(resultat);
                }


                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return connecte;
        }



    }
}
