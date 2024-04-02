using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FoodApp
{
    public partial class Aktiv_Rendelesek : UserControl
    {
        List<cim> Cimek = new List<cim>();
        List<order> Orders = new List<order>();
        public class order
        {
            private string id;
            private DateTime ido;
            private string allapot;
            private string cimID;
            private string fizID;

            public order(string id, DateTime ido, string allapot, string cimID, string fizID)
            {
                this.id = id;
                this.ido = ido;
                this.allapot = allapot;
                this.cimID = cimID;
                this.fizID = fizID;
            }

            public string Id { get => id; set => id = value; }
            public DateTime Ido { get => ido; set => ido = value; }
            public string Allapot { get => allapot; set => allapot = value; }
            public string CimID { get => cimID; set => cimID = value; }
            public string FizID { get => fizID; set => fizID = value; }
        }
        public class cim
        {
            string utca;
            string hazszam;
            string customerID;
            string orderID;

            public cim(string utca, string hazszam, string orderID, string customerID)
            {
                this.utca = utca;
                this.hazszam = hazszam;
                this.customerID = customerID;
                this.orderID = orderID;
            }

            public string Utca { get => utca; set => utca = value; }
            public string Hazszam { get => hazszam; set => hazszam = value; }
            public string CustomerID { get => customerID; set => customerID = value; }
            public string OrderID { get => orderID; set => orderID = value; }
        }
        public class customers
        {
            string nev;
            string telszam;

            public customers(string nev, string telszam)
            {
                this.nev = nev;
                this.telszam = telszam;
            }

            public string Nev { get => nev; set => nev = value; }
            public string Telszam { get => telszam; set => telszam = value; }
        }


        public Aktiv_Rendelesek()
        {
            InitializeComponent();
        }

        private void Aktiv_Rendelesek_Load(object sender, EventArgs e)
        {
            rendelesek_betolt();
        }

        private void rendelesek_betolt()
        {
            flowLayoutPanel1.Controls.Clear();
            Orders.Clear();
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT `orderID`, `orderDueTime`, `orderStatus`, `orderDestID`, `paymentID` FROM `orders` ORDER BY orderDueTime ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Orders.Add(new order(rdr[0].ToString(), Convert.ToDateTime(rdr[1]), rdr[2].ToString(),rdr[3].ToString(),rdr[4].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            foreach (order item in Orders)
            {
                try
                {
                    conn.Open();
                    string sql = $"SELECT `orderStreet`, `orderStreetNum`, `customerID` FROM `destination` WHERE ID={item.CimID}";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Cimek.Add(new cim(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), item.Id));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }


        }

        private void tetelAdd_Click(object sender, EventArgs e)
        {

        }

        private void tetelMinus_Click(object sender, EventArgs e)
        {

        }
    }
}
