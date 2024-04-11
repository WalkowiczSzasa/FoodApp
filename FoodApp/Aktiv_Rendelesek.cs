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
        public static string oID;
        public static List<string> foodID = new List<string>();
        public static List<string> drinkID = new List<string>();
        List<food> foodMenu = new List<food>();
        List<drink> drinkMenu = new List<drink>();
        List<customer> Customers = new List<customer>();
        List<cim> Cimek = new List<cim>();
        List<order> Orders = new List<order>();
        List<tetel> rendeles_tetelek = new List<tetel>();

        public class food
        {
            private string nev;
            private string leiras;
            private string ar;
            private bool elerheto;
            private string id;
            public food(string nev, string leiras, string ar, bool elerheto, string id) => (this.nev, this.leiras, this.ar, this.elerheto, this.id) = (nev, leiras, ar, elerheto, id);

            public string Nev { get => nev; set => nev = value; }
            public string Leiras { get => leiras; set => leiras = value; }
            public string Ar { get => ar; set => ar = value; }
            public bool Elerheto { get => elerheto; set => elerheto = value; }
            public string Id { get => id; set => id = value; }
        }
        public class drink
        {
            private string dNev;
            private string dAr;
            private bool dElerheto;
            private string dId;
            public drink(string dNev, string dAr, bool dElerheto, string dId) => (this.DNev, this.DAr, this.DElerheto, this.DId) = (dNev, dAr, dElerheto, dId);

            public string DNev { get => dNev; set => dNev = value; }
            public string DAr { get => dAr; set => dAr = value; }
            public bool DElerheto { get => dElerheto; set => dElerheto = value; }
            public string DId { get => dId; set => dId = value; }
        }
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
            etelek_betolt();
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
                aktiv_rend[i].fizID = Orders[i].FizID;
                
                flowLayoutPanel1.Controls.Add(aktiv_rend[i]);
            }

        }
        private void etelek_betolt()
        {
            flowLayoutPanel2.Controls.Clear();
            foodMenu.Clear();
            //TODO: watch?v=u71RJZm7Gdc&t=0s&ab_channel=AaricAaiden
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT `foodName`, `foodDesc`,`foodPrice`,`foodStatus`,`ID`  FROM `food`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    foodMenu.Add(new food(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), Convert.ToBoolean(Convert.ToInt32(rdr[3])), rdr[4].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            Menu_elemek_Aktiv_rend[] ar_menu_items = new Menu_elemek_Aktiv_rend[foodMenu.Count];

            for (int i = 0; i < foodMenu.Count(); i++)
            {
                if (foodMenu[i].Elerheto)
                {
                    ar_menu_items[i] = new Menu_elemek_Aktiv_rend();
                    ar_menu_items[i].Nev = foodMenu[i].Nev;
                    ar_menu_items[i].Ar = foodMenu[i].Ar;
                    ar_menu_items[i].Leiras = foodMenu[i].Leiras;
                    ar_menu_items[i].Elerheto = foodMenu[i].Elerheto;
                    ar_menu_items[i].Id = foodMenu[i].Id;

                    flowLayoutPanel2.Controls.Add(ar_menu_items[i]);

                }
            }

        }
        private void italok_betolt()
        {
            drinkMenu.Clear();
            flowLayoutPanel2.Controls.Clear();
            //TODO: watch?v=u71RJZm7Gdc&t=0s&ab_channel=AaricAaiden
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT `drinkName`, `drinkPrice`,`drinkStatus`,`ID`  FROM `drink`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    drinkMenu.Add(new drink(rdr[0].ToString(), rdr[1].ToString(), Convert.ToBoolean(Convert.ToInt32(rdr[2])), rdr[3].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            Menu_elemek_Aktiv_rend[] ar_Dmenu_items = new Menu_elemek_Aktiv_rend[drinkMenu.Count];

            for (int i = 0; i < drinkMenu.Count(); i++)
            {
                if (drinkMenu[i].DElerheto)
                {
                    ar_Dmenu_items[i] = new Menu_elemek_Aktiv_rend();
                    ar_Dmenu_items[i].Nev = drinkMenu[i].DNev;
                    ar_Dmenu_items[i].Ar = drinkMenu[i].DAr;
                    ar_Dmenu_items[i].Elerheto = drinkMenu[i].DElerheto;
                    ar_Dmenu_items[i].Id = drinkMenu[i].DId;

                    flowLayoutPanel2.Controls.Add(ar_Dmenu_items[i]);

                }
            }
        }
        private void rend_tetelek_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kiv_tetel = rend_tetelek.SelectedItem.ToString();
            kivalasztott_tetelTextBox.Text = kiv_tetel.TrimEnd('\t').Remove(kiv_tetel.LastIndexOf('\t') + 1);
        }

        public void rend_tetelekFeltoltes()
        {
            rend_tetelek.Items.Clear();
            rendeles_tetelek.Clear();
            
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                
                if (foodID.Count > 0 && foodID[0] != "")
                {
                    for (int i = 0; i < foodID.Count; i++)
                    {
                        conn.Open();
                        string sql = $"SELECT ID, foodName, foodPrice FROM `food` WHERE ID={foodID[i].TrimStart('f')} ORDER BY foodName ASC;";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            rendeles_tetelek.Add(new tetel(foodID[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));
                        }
                        conn.Close();
                    }
                }
                if (drinkID.Count > 0 && drinkID[0]!="")
                {
                    for (int i = 0; i < drinkID.Count; i++)
                    {
                        conn.Open();
                        string sql = $"SELECT ID, drinkName, drinkPrice FROM `drink` WHERE ID={drinkID[i].TrimStart('d')} ORDER BY drinkName ASC;";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            rendeles_tetelek.Add(new tetel(drinkID[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            foreach (tetel item in rendeles_tetelek)
            {
                rend_tetelek.Items.Add(item.TetelID + " " + item.TetelName + "\t" + item.TetelPrice);
            }
            rend_tetelek.Sorted = true;
           
        }

        private void etelBtn_Click(object sender, EventArgs e)
        {
            etelek_betolt();
        }

        private void italBtn_Click(object sender, EventArgs e)
        {
            italok_betolt();
        }

        private void tetelAdd_Click(object sender, EventArgs e)
        {
            string addID = kivalasztott_tetelTextBox.Text.Split(' ').FirstOrDefault();
            if (addID.Contains('d'))
            {
                drinkID.Add(addID);
            }
            else
            {
                foodID.Add(addID);
            }
            rend_tetelekFeltoltes();
        }

        private void tetelMinus_Click(object sender, EventArgs e)
        {
            string addID = kivalasztott_tetelTextBox.Text.Split(' ').FirstOrDefault();
            if (addID.Contains('d'))
            {
                drinkID.Remove(addID);
            }
            else
            {
                foodID.Remove(addID);
            }
            rend_tetelekFeltoltes();
        }

        private void refreshPictbox_Click(object sender, EventArgs e)
        {
            rend_tetelekFeltoltes();
        }

        private void mentesBtnClick(object sender, EventArgs e)
        {
            var oFoodID=String.Join(" ",foodID);
            var oDrinkID=String.Join(" ", drinkID);
            
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE `orders` SET `foodID`='{oFoodID}',`drinkID`='{oDrinkID}' WHERE orderID='{oID}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void adatok_modBtn_Click(object sender, EventArgs e)
        {
            adatmodPanel.BringToFront();
            //true= kártya false=kp
            
        }

        private void rendeles_szerkBtn_Click(object sender, EventArgs e)
        {
            tetelmodPanel.BringToFront();
        }
    }
}
