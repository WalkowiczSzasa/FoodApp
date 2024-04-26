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
    public partial class Futar_nezet : UserControl
    {
        public string LoggedID { get; set; }
        public string LoggedRole { get; set; }

        string[] keszorder=new string[9];

        List<cim> Cimek = new List<cim>();
        List<customer> Customers = new List<customer>();
        List<order> Orders = new List<order>();

        public static class adat 
        {
            static bool ellenorzes;
            static string nev;
            static string telefonszam;
            static string cim;
            static byte tetelhossz;
            static string tetelek;
            static DateTime duedate;
            static double sum;
            static double kiszDij;
            static double csomagar;
            static double tetelszam;
            static string note;
            static string orderID;
            static string fiztip;

            public static string Nev { get => nev; set => nev = value; }
            public static string Telefonszam { get => telefonszam; set => telefonszam = value; }
            public static string Cim { get => cim; set => cim = value; }
            public static string Tetelek { get => tetelek; set => tetelek = value; }
            public static DateTime Duedate { get => duedate; set => duedate = value; }
            public static double Sum { get => sum; set => sum = value; }
            public static bool Ellenorzes { get => ellenorzes; set => ellenorzes = value; }
            public static byte Tetelhossz { get => tetelhossz; set => tetelhossz = value; }
            public static double KiszDij { get => kiszDij; set => kiszDij = value; }
            public static double Csomagar { get => csomagar; set => csomagar = value; }
            public static double Tetelszam { get => tetelszam; set => tetelszam = value; }
            public static string Note { get => note; set => note = value; }
            public static string OrderID { get => orderID; set => orderID = value; }
            public static string Fiztip { get => fiztip; set => fiztip = value; }
        }
        public class order
        {
            private string id;
            private DateTime ido;
            private string allapot;
            private string cimID;
            private string fizID;
            private string dispatchID;
            private string note;

            public order(string id, DateTime ido, string allapot, string cimID, string fizID, string dispatchID, string note)
            {
                this.id = id;
                this.ido = ido;
                this.allapot = allapot;
                this.cimID = cimID;
                this.fizID = fizID;
                this.DispatchID = dispatchID;
                this.note = note;
            }

            public string Id { get => id; set => id = value; }
            public DateTime Ido { get => ido; set => ido = value; }
            public string Allapot { get => allapot; set => allapot = value; }
            public string CimID { get => cimID; set => cimID = value; }
            public string FizID { get => fizID; set => fizID = value; }
            public string DispatchID { get => dispatchID; set => dispatchID = value; }
            public string Note { get => note; set => note = value; }
        }
        public class cim
        {
            string utca;
            string hazszam;
            string customerID;
            string orderID;
            string orderType;

            public cim(string utca, string hazszam, string customerID, string orderID, string orderType)
            {
                this.utca = utca;
                this.hazszam = hazszam;
                this.customerID = customerID;
                this.orderID = orderID;
                this.orderType = orderType;
            }

            public string Utca { get => utca; set => utca = value; }
            public string Hazszam { get => hazszam; set => hazszam = value; }
            public string CustomerID { get => customerID; set => customerID = value; }
            public string OrderID { get => orderID; set => orderID = value; }
            public string OrderType { get => orderType; set => orderType = value; }
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

        public Futar_nezet()
        {
            InitializeComponent();
        }

        private void Futar_nezet_Load(object sender, EventArgs e)
        {
            rendelesek_betolt();
        }

        private void szamlaBtn_Click(object sender, EventArgs e)
        {
            if (adat.Ellenorzes == true && adat.Tetelek != null)
            {
                tetelListBox.Items.Clear();
                adatbeiras();
                adat.Ellenorzes = false;
            }
            else
            {
                MessageBox.Show("Előbb válassz ki egy rendelést a nagyítóra kattintással!");
            }
        }

        private void refreshPictbox_Click(object sender, EventArgs e)
        {
            rendelesek_betolt();
    }

        private void keszBtn_Click(object sender, EventArgs e)
        {
            order_lekeres();
            keszorder_atadas();
            adat_urites();
        }

        public void rendelesek_betolt()
        {
            futarFlowLayoutPanel.Controls.Clear();
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
                string sql = "SELECT `orderID`, `orderDueTime`, `orderStatus`, `orderDestID`, `paymentID`, `orderDispatchID`, `orderNote` FROM `orders` ORDER BY orderDueTime ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Orders.Add(new order(rdr[0].ToString(), Convert.ToDateTime(rdr[1]), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString(), rdr[5].ToString(), rdr[6].ToString()));
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
                    string sql = $"SELECT `orderStreet`, `orderStreetNum`, `customerID`, `orderType`  FROM `destination` WHERE ID={item.CimID}";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Cimek.Add(new cim(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), item.Id, rdr[3].ToString()));

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
                        Customers.Add(new customer(item.OrderID, rdr[0].ToString(), rdr[1].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }

            Futar_cimek[] futar_cim = new Futar_cimek[Orders.Count];
            for (int i = 0; i < Orders.Count(); i++)
            {
                futar_cim[i] = new Futar_cimek();
                if (Orders[i].Allapot=="1" && Orders[i].DispatchID==LoggedID)
                {
                    futar_cim[i].Id = Orders[i].Id;
                    for (int j = 0; j < Customers.Count; j++)
                    {
                        if (Customers[j].OrderID == Orders[i].Id)
                        {
                            futar_cim[i].Nev = Customers[j].Nev;
                            futar_cim[i].Telszam = Customers[j].Telszam;

                        }
                    }
                    futar_cim[i].Cim = Cimek[i].Utca + " " + Cimek[i].Hazszam;
                    futar_cim[i].RendelesType = Cimek[i].OrderType;
                    if (Orders[i].Allapot == "0")
                    {
                        futar_cim[i].Allapot = false;
                    }
                    else
                    {
                        futar_cim[i].Allapot = true;
                    }
                    futar_cim[i].Duetime = Orders[i].Ido;
                    futar_cim[i].fizID = Orders[i].FizID;
                    futar_cim[i].cimID = Orders[i].CimID;
                    futar_cim[i].customerID = Cimek[i].CustomerID;
                    futar_cim[i].Note = Orders[i].Note;
                    futarFlowLayoutPanel.Controls.Add(futar_cim[i]);
                }
                else if (Orders[i].Allapot=="1" && LoggedID=="1")
                {
                    futar_cim[i].Id = Orders[i].Id;
                    for (int j = 0; j < Customers.Count; j++)
                    {
                        if (Customers[j].OrderID == Orders[i].Id)
                        {
                            futar_cim[i].Nev = Customers[j].Nev;
                            futar_cim[i].Telszam = Customers[j].Telszam;

                        }
                    }
                    futar_cim[i].Cim = Cimek[i].Utca + " " + Cimek[i].Hazszam;
                    futar_cim[i].RendelesType = Cimek[i].OrderType;
                    if (Orders[i].Allapot == "0")
                    {
                        futar_cim[i].Allapot = false;
                    }
                    else
                    {
                        futar_cim[i].Allapot = true;
                    }
                    futar_cim[i].Duetime = Orders[i].Ido;
                    futar_cim[i].fizID = Orders[i].FizID;
                    futar_cim[i].cimID = Orders[i].CimID;
                    futar_cim[i].customerID = Cimek[i].CustomerID;
                    futar_cim[i].Note = Orders[i].Note;
                    futarFlowLayoutPanel.Controls.Add(futar_cim[i]);
                }
                else if(Orders[i].DispatchID=="4" && LoggedRole=="cook")
                {
                    futar_cim[i].Id = Orders[i].Id;
                    for (int j = 0; j < Customers.Count; j++)
                    {
                        if (Customers[j].OrderID == Orders[i].Id)
                        {
                            futar_cim[i].Nev = Customers[j].Nev;
                            futar_cim[i].Telszam = Customers[j].Telszam;

                        }
                    }
                    futar_cim[i].Cim = Cimek[i].Utca + " " + Cimek[i].Hazszam;
                    futar_cim[i].RendelesType = Cimek[i].OrderType;
                    if (Orders[i].Allapot == "0")
                    {
                        futar_cim[i].Allapot = false;
                    }
                    else
                    {
                        futar_cim[i].Allapot = true;
                    }
                    futar_cim[i].Duetime = Orders[i].Ido;
                    futar_cim[i].fizID = Orders[i].FizID;
                    futar_cim[i].cimID = Orders[i].CimID;
                    futar_cim[i].customerID = Cimek[i].CustomerID;
                    futar_cim[i].Note = Orders[i].Note;
                    futarFlowLayoutPanel.Controls.Add(futar_cim[i]);
                }

            }

        }
        private void adatbeiras()
        {
            string[] tetelek = adat.Tetelek.Split(';').ToArray();
            for (int i = 0; i < tetelek.Length-1; i++)
            {
                tetelListBox.Items.Add(tetelek[i]);
            }
            Array.Clear(tetelek, 0, tetelek.Length);
            nevLabel.Text = adat.Nev;
            telszamLabel.Text = adat.Telefonszam;
            cimLabel.Text = adat.Cim;
            szummaLabel.Text = (adat.Sum+adat.Csomagar*adat.Tetelszam).ToString();
            szallitasidijLabel.Text = adat.KiszDij.ToString();
            csomagolasTextBox.Text = $"{adat.Tetelszam}x Csomagolás \t\t{adat.Csomagar*adat.Tetelszam}Ft";
            megjegyzesRichTextBox.Text = adat.Note;
            datumkezeles();
            fiztip_kezeles();
        }

        private void fiztip_kezeles()
        {
            if (adat.Fiztip=="True")
            {
                fiztipLabel.Text = "Kártyás fizetés";
            }
            else if (adat.Fiztip=="False")
            {
                fiztipLabel.Text = "Készpénzes fizetés";
            }
            else
            {
                fiztipLabel.Text = "Hibás formátumú fizetési mód!";
            }
        }

        private void datumkezeles()
        {
            DateTime ma = DateTime.Now;
            if (adat.Duedate.Date > ma.Date)
            {
                duetimeLabel.Text = adat.Duedate.ToString("HH:mm");
                datumLabel.Text = adat.Duedate.ToString("MM/dd");
                datumLabel.Show();
            }
            else
            {
                duetimeLabel.Text = adat.Duedate.ToString("HH:mm");
                datumLabel.Hide();
            }
        }
        private void keszorder_atadas()
        {
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string sql = $"INSERT INTO `finishedorders`(`forderID`, `orderNote`, `orderTime`, `orderDueTime`, `orderDestID`, `orderDispatchID`, `foodID`, `drinkID`, `paymentID`) VALUES ('{keszorder[0]}','{keszorder[1]}','{keszorder[2]}','{keszorder[3]}','{keszorder[4]}','{keszorder[5]}','{keszorder[6]}','{keszorder[7]}','{keszorder[8]}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                try
                {
                    conn.Open();
                    sql = $"DELETE FROM `orders` WHERE `orderID`={adat.OrderID}";
                    MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    adat_urites();
                    rendelesek_betolt();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Valamilyen hiba történt a művelet során!");
            }

        }

        private void adat_urites()
        {
            nevLabel.Text = "xxxxxxx xxxxxx";
            telszamLabel.Text = "xx/xxx xxxx";
            cimLabel.Text = "xxxxxxxxxxx xxxxxx xxx";
            szummaLabel.Text = "xxxxx";
            szallitasidijLabel.Text = "xxx";
            csomagolasTextBox.Text = default;
            megjegyzesRichTextBox.Text = "*Megjegyzés helye*";
            tetelListBox.Items.Clear();
            duetimeLabel.Text = "xx:xx";
            datumLabel.Text = "xx/xx";
            fiztipLabel.Text = "Fizetési mód";
        }

        private void order_lekeres()
        {
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string sql = $"SELECT `orderID`, `orderNote`, `orderTime`, `orderDueTime`, `orderDestID`, `orderDispatchID`,`foodID`, `drinkID`, `paymentID` FROM `orders` WHERE orderID={adat.OrderID}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    keszorder[0] = rdr[0].ToString();
                    keszorder[1] = rdr[1].ToString();
                    keszorder[2] = Convert.ToDateTime(rdr[2]).ToString("yyyy-MM-dd HH:mm:ss");
                    keszorder[3] = Convert.ToDateTime(rdr[3]).ToString("yyyy-MM-dd HH:mm:ss");
                    keszorder[4] = rdr[4].ToString();
                    keszorder[5] = rdr[5].ToString();
                    keszorder[6] = rdr[6].ToString();
                    keszorder[7] = rdr[7].ToString();
                    keszorder[8] = rdr[8].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

        }
    }
}
