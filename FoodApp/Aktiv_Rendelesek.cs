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
        List<customer> Customers = new List<customer>();
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

            public cim(string utca, string hazszam, string customerID, string orderID)
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
        public class customer
        {
            string orderID;
            string nev;
            string telszam;

            public customer(string orderID, string nev, string telszam)
            {
                this.orderID = orderID;
                this.nev = nev;
                this.telszam = telszam;
            }

            public string OrderID { get => orderID; set => orderID = value; }
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

        public void rendelesek_betolt()
        {
            flowLayoutPanel1.Controls.Clear();
            Orders.Clear();
            Cimek.Clear();
            Customers.Clear();
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
            
            foreach (cim item in Cimek)
            {
                try
                {
                    conn.Open();
                    string sql = $"SELECT `name`, `phoneNumber` FROM `customer` WHERE ID={item.CustomerID}";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Customers.Add(new customer(item.OrderID,rdr[0].ToString(), rdr[1].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
            MessageBox.Show(Customers.Count().ToString());
            Aktiv_rendeles[] aktiv_rend = new Aktiv_rendeles[Orders.Count];
            for (int i = 0; i < Orders.Count(); i++)
            {
                aktiv_rend[i] = new Aktiv_rendeles();
                aktiv_rend[i].Id = Orders[i].Id;
                for (int j = 0; j < Customers.Count; j++)
                {
                    if (Customers[j].OrderID==Orders[i].Id)
                    {
                        aktiv_rend[i].Nev = Customers[j].Nev;
                        aktiv_rend[i].Telszam = Customers[j].Telszam;
                        
                    }
                }
                //aktiv_rend[i].Nev = Customers[i].Nev;
                //aktiv_rend[i].Telszam = Customers[i].Telszam;
                aktiv_rend[i].Cim = Cimek[i].Utca + " " +Cimek[i].Hazszam;
                if (Orders[i].Allapot=="0")
                {
                    aktiv_rend[i].Allapot = false;
                }
                else
                {
                    aktiv_rend[i].Allapot = true;
                }
                aktiv_rend[i].Duetime = Orders[i].Ido;
                aktiv_rend[i].Ar = aktiv_rend[i].Ar;

                    flowLayoutPanel1.Controls.Add(aktiv_rend[i]);
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
