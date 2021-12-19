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
using System.Diagnostics;

namespace ATM
{
    public partial class Erwachsen : Form
    {
        public Erwachsen(Datenbank kunde)//übertragung des Objekts  um es in dieser Seite zu verwenden
        {
            InitializeComponent();

            Guthaben.Text = "Guthaben: " + Convert.ToString(kunde.Kapi);
            label2.Text = "Kontonummer: " + Convert.ToString(kunde.Kontonr);
            label1.Text = "Inhaber: " + kunde.Vorname + " " + kunde.Nachname;
            überschreibung = kunde;
        }
        Datenbank überschreibung;//objekt der Seite 

        private void button1_Click(object sender, EventArgs e)//Einzahlen
        {
            try
            {
                //Einzahlen
                double wert = Convert.ToDouble(textBox1.Text);
                überschreibung.Kapi = überschreibung.Kapi + wert;
                Guthaben.Text = "Guthaben: " + Convert.ToString(überschreibung.Kapi);

                //Speichern der Einzahlung in die Liste
                XmlSerializer sr = new XmlSerializer(typeof(List<Datenbank>));
                TextWriter writer = new StreamWriter("Kontodaten.xml");
                sr.Serialize(writer, Datenbank_und_registrieren_für_Kunden.Datenkunden);
                writer.Close();
            }
            catch(Exception ex)//Fehlermeldung ausgabe
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button2_Click(object sender, EventArgs e)//Abheben
        {
            try
            {
                double wert = Convert.ToDouble(textBox1.Text);
                if(0<= (überschreibung.Kapi - wert))//Darf nicht unter 0 sein
                {
                    überschreibung.Kapi = überschreibung.Kapi - wert;
                    Guthaben.Text = "Guthaben: " + Convert.ToString(überschreibung.Kapi);

                    //Speichern der Abhebung in die Liste
                    XmlSerializer sr = new XmlSerializer(typeof(List<Datenbank>));//Typ der in eine XML-Datei umgewandelt wird
                    TextWriter writer = new StreamWriter("Kontodaten.xml");//erstellen eines Schreibers
                    sr.Serialize(writer, Datenbank_und_registrieren_für_Kunden.Datenkunden);//Schreiben der Datei
                    writer.Close();//Schreiber schließen
                }
                else
                {
                    MessageBox.Show("Nicht genug Geld auf dem Konto");//Fehlermeldung ausgeben
                }
                
            }
            catch(Exception ex)//Fehlermeldung Ausgabe
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button4_Click(object sender, EventArgs e)//überweisen
        {
            try
            {
                //Variablen
                double wert = Convert.ToDouble(textBox2.Text);
                int knr = Convert.ToInt32(textBox3.Text);

                //Kontonummer gültig?
                if (Datenbank_und_registrieren_für_Kunden.knrlist.Contains(knr))
                {
                    if(0<=(überschreibung.Kapi = überschreibung.Kapi - wert))
                    {
                        //Abziehen des Geldes vom Konto
                        überschreibung.Kapi = überschreibung.Kapi - wert;
                        Guthaben.Text = "Guthaben: " + Convert.ToString(überschreibung.Kapi);

                        //Übertragung des Geld auf ein anders Konto
                        foreach (var kunde in Datenbank_und_registrieren_für_Kunden.Datenkunden)
                        {
                            if (kunde.Kontonr == knr)
                            {
                                kunde.Kapi = kunde.Kapi + wert;
                            }
                        }

                        //Speichern der Überweisung
                        XmlSerializer sr = new XmlSerializer(typeof(List<Datenbank>));
                        TextWriter writer = new StreamWriter("Kontodaten.xml");
                        sr.Serialize(writer, Datenbank_und_registrieren_für_Kunden.Datenkunden);
                        writer.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nicht genug Geld auf dem Konto");
                    }
                }
                else//Kontonummer nicht vorhanden
                {
                    MessageBox.Show("Kontonummer ist nicht vergeben");
                }
                textBox3.Text = "Kontonummer";
            }
            catch(Exception ex)//Fehlermeldung ausgabe
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)//rein Optisch
        {
            textBox3.Text = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //Rückkehr zum Home-Bildschrim
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
            
            // Beenden des Prozess |Fix vor Datei kann nicht ausgeführt werden da ein ander Prozess darauf greift
            Process[] pp = Process.GetProcessesByName("taskmgr");
            foreach (Process p in pp)
            {
                p.CloseMainWindow();
            }

        }
    }
}
