using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Datenbank
    {
        //private variablen
        private string  vorname, nachname;
        private double Kapital;
        private int x,knr;

        //Getter und Setter ,um auf die Variablen darauf zu zugreifen
        public string Vorname
        {
            get { return vorname; }
            set { vorname = value; }
        }
        public string Nachname
        {
            get { return nachname; }
            set { nachname = value; }
        }
        public double Kapi
        {
            get { return Kapital; }
            set { Kapital = value; }
        }
        public int passwort
        {
            get { return x; }
            set { x = value; }
        }
        public int Kontonr
        {
            get { return knr; }
            set { knr = value; }
        }


    }
}
