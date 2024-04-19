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

        private string id, nev, telszam, cim;
        public string foodID = "";
        public string drinkID = "";
        public string fizID, cimID, customerID;
        public static string fiztip;
        double kiszDij=0, sum=0;
        public byte checkClick = 0;
        private bool allapot;
        private DateTime duetime;
        public static string[] foodIDk = new string[1];
        public static string[] drinkIDk = new string[1];
        List<futar> futarok = new List<futar>();
        List<tetel> rendeles_tetelek = new List<tetel>();

        public class tetel
        {
            string tetelID;
            string tetelName;
            double tetelPrice;

            public tetel(string tetelID, string tetelName, double tetelPrice)
            {
                this.tetelID = tetelID;
                this.tetelName = tetelName;
                this.tetelPrice = tetelPrice;
            }
            public string TetelID { get => tetelID; set => tetelID = value; }
            public string TetelName { get => tetelName; set => tetelName = value; }
            public double TetelPrice { get => tetelPrice; set => tetelPrice = value; }
        }
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
        public DateTime Duetime { get => duetime; set => duetime = value; }

        private void Aktiv_rendeles_Load(object sender, EventArgs e)
        {
            combobox_kezeles();
            futarok_betolt();
            fiztip_lekerdezes();
            adat_beiras();
            vegosszeg_szamitas();
            vegosszeg_beiras();
        }

        private void combobox_kezeles()
        {
            futarComboBox.MouseWheel += new MouseEventHandler(futarComboBox_MouseWheel);
        }

        private void vegosszeg_beiras()
        {
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE `payment` SET sum='{sum}' WHERE ID='{fizID}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void vegosszeg_szamitas()
        {
            id_lekeres();
            rend_tetelekLekeres();
            adat_beiras();
        }
        private void id_lekeres()
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
                    foodIDk[0] = (rdr[0].ToString());
                    drinkIDk[0] = (rdr[1].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        public void rend_tetelekLekeres()
        {
            sum = 0;
            rendeles_tetelek.Clear();

            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            foreach (var item in foodIDk)
            {
                List<string> food = item.Split(' ').ToList();
                //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
                try
                {
                    if (food.Count > 0 && food[0] != "")
                    {
                        for (int i = 0; i < food.Count; i++)
                        {
                            conn.Open();
                            string sql = $"SELECT ID, foodName, foodPrice FROM `food` WHERE ID={food[i].TrimStart('f')} ORDER BY foodName ASC;";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            MySqlDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                rendeles_tetelek.Add(new tetel(food[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));
                            }
                            conn.Close();
                        }
                        food.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            foreach (var item in drinkIDk)
            {
                if (item != "" && item != null)
                {
                    List<string> drink = item.Split(' ').ToList();
                    if (drink.Count > 0 && drink[0] != "")
                    {
                        for (int i = 0; i < drink.Count; i++)
                        {
                            conn.Open();
                            string sql = $"SELECT ID, drinkName, drinkPrice FROM `drink` WHERE ID={drink[i].TrimStart('d')} ORDER BY drinkName ASC;";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            MySqlDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                rendeles_tetelek.Add(new tetel(drink[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));

                            }
                            conn.Close();
                        }
                    }
                }
            }

            Dictionary<string, (int, double)> dict = megszamlalas(rendeles_tetelek);
            foreach (KeyValuePair<string, (int, double)> x in dict)
            {
                sum += x.Value.Item2;
            }

            rendeles_tetelek.Clear();
            adat_beiras();
        }

        private void fiztip_lekerdezes()
        {
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = $"SELECT `paymentType`, `deliveryCost` FROM `payment` WHERE `ID`='{fizID}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    fiztip = rdr[0].ToString();
                    kiszDij = Convert.ToDouble(rdr[1]);
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
            foreach (futar item in futarok)
            {
                futarComboBox.Items.Add(item.Nev);
            }
            futarComboBox.SelectedIndex = -1;
        }
        //pack cost feltöltés sum-nál
        private void szerk_PictureBox_Click(object sender, EventArgs e)
        {
            checkClick = 1;
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
            Aktiv_Rendelesek.kiszDIj = kiszDij;
            Aktiv_Rendelesek.adat.Telszam = telszam;
            Aktiv_Rendelesek.adat.Nev = nev;
            Aktiv_Rendelesek.adat.Utcahsz = cim;
            Aktiv_Rendelesek.adat.Fizmod = fiztip;
            Aktiv_Rendelesek.adat.Ido = duetime;
            Aktiv_Rendelesek.adat.CustID = customerID;
            Aktiv_Rendelesek.adat.Check = checkClick;
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
            arLabel.Text = (sum + kiszDij).ToString();
            cimLabel.Text = cim;
            arLabel.Text = sum+kiszDij + "Ft";
            hatarido_kezeles();
            allapot_ellenorzes();

        }

        private void hatarido_kezeles()
        {
            DateTime ma = DateTime.Now;
            if (duetime.Date>ma.Date)
            {
                idoLabel.Text = Duetime.ToString("HH:mm tt");
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
                futarComboBox.Enabled = true;
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
                futarComboBox.Enabled = false;
            }
        }

        private Dictionary<string, (int, double)> megszamlalas(List<tetel> items)
        {
            var szamol = items
                .GroupBy(x => x.TetelName)
                .Select(x => new
                {
                    nev = x.Key,
                    count = x.Count(),
                    total = x.Sum(item => item.TetelPrice)
                }).ToDictionary(kp => kp.nev, kp => (kp.count, kp.total));
            return szamol;
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
        void futarComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        
    }
}
