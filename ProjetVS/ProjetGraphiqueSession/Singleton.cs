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

        MySqlConnection con = new MySqlConnection
            ("Server=cours.cegep3r.info;Database=420345ri_gr00002_2309444-brayan-dyvan-lando-longmene;Uid='2309444';Pwd='2309444';");

        static Singleton instance = null;

        public Singleton()
        {
 

        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }

    }
}
