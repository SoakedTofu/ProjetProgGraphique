using MySql.Data.MySqlClient;
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
    }
}
