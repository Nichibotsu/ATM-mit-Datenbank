using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace ATM//Fixen Idee:Ein Formular mit meheren Panels
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//Eintragen Button
        {
            int passwort = Convert.ToInt32(textBox1.Text);//Passwort

            //Prüfen der Gültigkeit
            if (Datenbank_und_registrieren_für_Kunden.passlist.Contains(passwort))
            {
                foreach(var kunde in Datenbank_und_registrieren_für_Kunden.Datenkunden)
                {
                    if (kunde.passwort == passwort)
                    {
                        //Seite verstecken und Terminal öffnen
                        this.Hide();
                        Erwachsen erwachsen = new Erwachsen(kunde);
                        erwachsen.Show();
                    }
                }
            }
            else//Wenn nicht gültig
            {
                MessageBox.Show("Passwort ist falsch");
            }
        }

        private void label5_Click(object sender, EventArgs e)//Seite verstecken und Datenbank-Seite öffnen
        {
            this.Hide();
            Datenbank_und_registrieren_für_Kunden data = new Datenbank_und_registrieren_für_Kunden();
            data.Show();
        }

        private void Form1_Load(object sender, EventArgs e)//Führe beim Start die Lade-Methoden der Listen aus um das Passwort zu überprüfen und um die Kontodaten zu übergeben
        {
            Datenbank_und_registrieren_für_Kunden.Lade_Methode_Kontodaten();
            Datenbank_und_registrieren_für_Kunden.Lade_Methode_Passlist_Knrlist();
        }
    }
}
