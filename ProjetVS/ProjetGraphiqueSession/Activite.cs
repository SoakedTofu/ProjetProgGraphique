using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGraphiqueSession
{
    internal class Activite
    {
        string nom;
        double note;

        public Activite(string nom, double note)
        {
            this.nom = nom;
            this.note = note;
        }

        public string Nom { get { return nom; } set { nom = value; } }
        public double Note { get { return note; }set { note = value; } }

        public override string ToString()
        {
            return nom+" "+note;
        }

    }
}
