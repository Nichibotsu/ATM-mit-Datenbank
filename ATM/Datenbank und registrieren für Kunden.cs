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


namespace ATM
{
    public partial class Datenbank_und_registrieren_für_Kunden : Form
    {
        public Datenbank_und_registrieren_für_Kunden()
        {
            InitializeComponent();
        }

        /// 
        /// 
        /// Optisch
        /// 
        /// 
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "Vorname";
            textBox3.Text = "Nachname";
            textBox4.Text = "Höhe des Kapitals";
        }

        /// 
        /// Code
        /// 

        Random random = new Random(1000000000);
        Random r2 = new Random(1000000000);

        private void button4_Click(object sender, EventArgs e)//Save
        {
            try
            {
                //Ausführen der Lade-Metode
                SaveData.Save(Datenkunden, "Kontodaten.xml");
                SaveData.Save_passlist(passlist, "passlist.xml");
                SaveData.Save_kontonr(knrlist, "Kontonr.xml");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
        //Listen  und Obejekte erstellen
        public static List<int> passlist = new List<int>();
        public static List<int> knrlist = new List<int>();
        public static List<Datenbank> Datenkunden = new List<Datenbank>();

        private void button5_Click(object sender, EventArgs e)//Laden
        {
            //Eintragen der Daten in die Tabelle
            dataGridView1.Rows.Clear();
            if (File.Exists("Kontodaten.xml"))
            {
                Lade_Methode_Kontodaten();
                foreach (var kunde in Datenkunden)
                {
                    dataGridView1.Rows.Add( kunde.Vorname, kunde.Nachname, kunde.Kapi, kunde.Kontonr, kunde.passwort);
                }
            }
            Lade_Methode_Passlist_Knrlist();

        }
        public static void Lade_Methode_Kontodaten()//Lade-Methode für die Daten der Kunden
        {
            if (File.Exists("Kontodaten.xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Datenbank>));
                FileStream read = new FileStream("Kontodaten.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Datenkunden = (List<Datenbank>)xmlSerializer.Deserialize(read);
            }
            
        }
        public static void Lade_Methode_Passlist_Knrlist()//Lade Methode für die Passwort-Liste und Kontolsite
        {
            if (File.Exists("passlist.xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<int>));
                FileStream read = new FileStream("passlist.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                passlist = (List<int>)xmlSerializer.Deserialize(read);

            }
            if (File.Exists("Kontonr.xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<int>));
                FileStream read = new FileStream("Kontonr.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                knrlist = (List<int>)xmlSerializer.Deserialize(read);
            }

        }
        private void button2_Click(object sender, EventArgs e)//Hinzufügen
        {
           
            try
            {
                //Deklarien
                Datenbank daten = new Datenbank();
                daten.Vorname = textBox2.Text;
                daten.Nachname = textBox3.Text;
                daten.Kapi = Convert.ToDouble(textBox4.Text);
                daten.passwort = random.Next(100000);
                //Zufälliger
                daten.Kontonr = r2.Next(10000);
                daten.Kontonr = r2.Next(10000);

                //Verhinderung der Doppelbelegung durch eine Liste für die Random zahlen der Konto Nummer
                if (knrlist.Contains(daten.Kontonr))
                {
                    while (knrlist.Contains(daten.Kontonr))
                    {
                        daten.Kontonr = random.Next(10000);
                    }
                }
                knrlist.Add(daten.Kontonr);

                //Verhinderung der Doppelbelegung durch eine Liste für die Random zahlen der Passwörter

                if (passlist.Contains(daten.passwort))
                {
                    do
                    {
                        daten.passwort = r2.Next(100000);
                    } while (passlist.Contains(daten.passwort));
                }
                passlist.Add(daten.passwort);



                //Objekte in eine Liste hinzzufügen
                Datenkunden.Add(new Datenbank
                {
                    Vorname = textBox2.Text,
                    Nachname = textBox3.Text,
                    Kapi = Convert.ToDouble(textBox4.Text),
                    Kontonr = daten.Kontonr,
                    passwort = daten.passwort
                });

                //Eintragen der Objekte in eine Tabelle
                dataGridView1.Rows.Add( daten.Vorname, daten.Nachname, daten.Kapi, daten.Kontonr, daten.passwort);

                optisch();//Textbox Text hintergrund
            }
            catch (Exception ex)//Fehlermeldung ausgeben
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void optisch()//rein optisch
        {
            textBox2.Text = "Vorname";
            textBox3.Text = "Nachname";
            textBox4.Text = "Höhe des Kapitals";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
            //Zurückknopf
        }

        private void button6_Click(object sender, EventArgs e)//Neu Laden
        {
            textBox1.Text = "Nachname";
            dataGridView1.Rows.Clear();
            foreach (var kunde in Datenkunden)
            {
                dataGridView1.Rows.Add( kunde.Vorname, kunde.Nachname, kunde.Kapi, kunde.Kontonr, kunde.passwort);
            }
        }

        private void button1_Click(object sender, EventArgs e)//Suchen
        {
            string x = textBox1.Text;
            dataGridView1.Rows.Clear();
            foreach (var kunde in Datenkunden)
            {
                if (kunde.Nachname == x)
                {
                    dataGridView1.Rows.Add( kunde.Vorname, kunde.Nachname, kunde.Kapi, kunde.Kontonr, kunde.passwort);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)//Datenbank leeren
        {
            try
            {
                //Daten der Listen löschen
                Datenkunden.Clear();
                passlist.Clear();
                knrlist.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Datenbank_und_registrieren_für_Kunden_Load(object sender, EventArgs e)//Ausführen beim Start der Seite |Eintragen der Daten in die Tabelle
        {
            if (File.Exists("Kontodaten.xml"))
            {
                dataGridView1.Rows.Clear();
                foreach (var kunde in Datenkunden)
                {
                    dataGridView1.Rows.Add( kunde.Vorname, kunde.Nachname, kunde.Kapi, kunde.Kontonr, kunde.passwort);
                }
            }
        }
    }
    public class SaveData//Speicher classe
    {
        public static void Save(object obj, string filename)//Speichern der Kontodaten
        {
            if (File.Exists("Kontodaten.xml"))
            {
                File.Delete("Kontodaten.xml");
            }
            XmlSerializer sr = new XmlSerializer(typeof(List<Datenbank>));
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);
            writer.Close();
        }
        public static void Save_passlist(object obj,string filename)//Speichern der Passwörter
        {
            if (File.Exists("passlist.xml"))
            {
                File.Delete("passlist.xml");
            }
            XmlSerializer sr = new XmlSerializer(typeof(List<int>));
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);
            writer.Close();
        }
        public static void Save_kontonr(object obj, string filename)//Speichern der Kontonummer-Liste
        {
            if (File.Exists("Kontonr.xml"))
            {
                File.Delete("Kontonr.xml");
            }
            XmlSerializer sr = new XmlSerializer(typeof(List<int>));
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);
            writer.Close();
        }
        

    }

}
