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
        public string foodID = "";
        public string drinkID = "";
        public string fizID, cimID, customerID;
        public static string fiztip;
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
            fiztip_lekerdezes();
            adat_beiras();
        }

        private void fiztip_lekerdezes()
        {
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = $"SELECT `paymentType` FROM `payment` WHERE `ID`='{fizID}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    fiztip = rdr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
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

        private void szerk_PictureBox_Click(object sender, EventArgs e)
        {
            string orderID = id_Label.Text.Trim('#');

            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //dispatchID és név kiválasztása a users táblából combobox feltöltéshez
            try
            {
                conn.Open();
                string sql = $"SELECT `foodID`, `drinkID` FROM `orders` WHERE `orderID`='{orderID}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    foodID = rdr[0].ToString();
                    drinkID = rdr[1].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            adat_atadas();


        }

        private void adat_atadas()
        {
            Aktiv_Rendelesek.foodID = foodID.Split(' ').ToList();
            Aktiv_Rendelesek.drinkID = drinkID.Split(' ').ToList();
            Aktiv_Rendelesek.oID = Id;
            Aktiv_Rendelesek.fizID = fizID;
            Aktiv_Rendelesek.destID = cimID;
            Aktiv_Rendelesek.adat.Telszam = telszam;
            Aktiv_Rendelesek.adat.Nev = nev;
            Aktiv_Rendelesek.adat.Utcahsz = cim;
            Aktiv_Rendelesek.adat.Fizmod = fiztip;
            Aktiv_Rendelesek.adat.Ido = duetime;
            Aktiv_Rendelesek.adat.Vegossz = ar;
            Aktiv_Rendelesek.adat.CustID = customerID;
        }

        private void futarComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE `orders` SET orderDispatchID='{futarok[futarComboBox.SelectedIndex].Id}' WHERE orderID='{Id}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void adat_beiras()
        {
            id_Label.Text = "#"+id;
            nevLabel.Text = nev;
            telszamLabel.Text = telszam;
            cimLabel.Text = cim;
            arLabel.Text = ar + "Ft";
            hatarido_kezeles();
            allapot_ellenorzes();

            foreach (futar item in futarok)
            {
                futarComboBox.Items.Add(item.Nev);
            }
            futarComboBox.SelectedIndex = -1;
        }

        private void hatarido_kezeles()
        {
            DateTime ma = DateTime.Now;
            if (duetime.Date>ma.Date)
            {
                idoLabel.Text = Duetime.ToString("HH:mm");
                dueDayLabel.Text= Duetime.ToString("MM/dd");
            }
            else
            {
                idoLabel.Text = Duetime.ToString("HH:mm");
                dueDayLabel.Hide();
            }
        }

        private void allapot_ellenorzes()
        {
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            if (allapot == true)
            {
                checkBox.Checked = true;
                allapotLabel.Text = "Kész";
                allapotLabel.BackColor = Color.Green;
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    conn.Open();
                    string sql = $"UPDATE `orders` SET orderStatus=1 WHERE orderID='{Id}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
            else
            {
                checkBox.Checked = false;
                allapotLabel.Text = "Készül";
                allapotLabel.BackColor = Color.Goldenrod;
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    conn.Open();
                    string sql = $"UPDATE `orders` SET orderStatus=0 WHERE orderID='{Id}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
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
