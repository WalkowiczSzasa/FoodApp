using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FoodApp
{
    public partial class Aktiv_rendeles : UserControl
    {
        public Aktiv_rendeles()
        {
            InitializeComponent();
        }

        private string id, nev, telszam, cim, ar;
        private bool allapot;
        private DateTime duetime;
        List<futar> futarok = new List<futar>();

        public class futar
        {
            private string id;
            private string nev;

            public futar(string id, string nev)
            {
                this.id = id;
                this.nev = nev;
            }

            public string Id { get => id; set => id = value; }
            public string Nev { get => nev; set => nev = value; }
        }
        public string Id { get => id; set => id = value; }
        public string Nev { get => nev; set => nev = value; }
        public string Telszam { get => telszam; set => telszam = value; }
        public string Cim { get => cim; set => cim = value; }
        public bool Allapot { get => allapot; set => allapot = value; }
        public string Ar { get => ar; set => ar = value; }
        public DateTime Duetime { get => duetime; set => duetime = value; }

        private void Aktiv_rendeles_Load(object sender, EventArgs e)
        {
            futarok_betolt();
            adat_beiras();
        }

        private void futarok_betolt()
        {
            futarComboBox.Items.Clear();
            futarok.Clear();
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //dispatchID és név kiválasztása a users táblából combobox feltöltéshez
            try
            {
                conn.Open();
                string sql = "SELECT ID, dname FROM `users` WHERE role='dispatch' ORDER BY dname ASC;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    futarok.Add(new futar(rdr[0].ToString(), rdr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void adat_beiras()
        {
            nevLabel.Text = nev;
            telszamLabel.Text = telszam;
            cimLabel.Text = cim;
            arLabel.Text = ar + "Ft";
            allapot_ellenorzes();

            foreach (futar item in futarok)
            {
                futarComboBox.Items.Add(item.Nev);
            }
            futarComboBox.SelectedIndex = -1;
        }

        private void allapot_ellenorzes()
        {
            if (allapot == true)
            {
                checkBox.Checked = true;
                allapotLabel.Text = "Kész";
                allapotLabel.BackColor = Color.Green;
            }
            else
            {
                checkBox.Checked = false;
                allapotLabel.Text = "Készül";
                allapotLabel.BackColor = Color.Goldenrod;
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked==true)
            {
                allapot = true;
            }
            else
            {
                allapot = false;
            }
            allapot_ellenorzes();
        }
    }
}
