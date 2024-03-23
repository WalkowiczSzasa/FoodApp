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
    public partial class Rendeles_felvetel : UserControl
    {
        private byte elvitel;
        private byte fizeszk;
        private DateTime DueDate = new DateTime();
        private int customerID;
        private int orderDestID;


        public List<string> Elemek { get => elemek; set => elemek = value; }
        private List<string> elemek = new List<string>();

        public class item
        {
            private string nev;
            private string leiras;
            private string ar;
            private bool elerheto;
            private string id;
            public item(string nev, string leiras, string ar, bool elerheto, string id) => (this.nev, this.leiras, this.ar, this.elerheto, this.id) = (nev, leiras, ar, elerheto, id);

            public string Nev { get => nev; set => nev = value; }
            public string Leiras { get => leiras; set => leiras = value; }
            public string Ar { get => ar; set => ar = value; }
            public bool Elerheto { get => elerheto; set => elerheto = value; }
            public string Id { get => id; set => id = value; }
        }
        List<item> Menu = new List<item>();


        public class customer
        {
            private int id;
            private string nev;
            private string telszam;
            public customer(int id, string nev, string telszam) => (this.id, this.nev, this.telszam) = (id, nev, telszam);

            public int Id { get => id; set => id = value; }
            public string Nev { get => nev; set => nev = value; }
            public string Telszam { get => telszam; set => telszam = value; }
        }
        List<customer> customerek = new List<customer>();


        public Rendeles_felvetel()
        {
            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //TODO: food és drink tábla elemeinek kilistázása
            uj_rendeles();
            customerek_betolt();
            /*try
            {
                MessageBox.Show(Elemek.ElementAt(0).ToString());
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
                throw;
            }*/
        }

        private void customerek_betolt()
        {
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT ID, name, phoneNumber FROM `customer` ORDER BY name ASC;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customerek.Add(new customer(Convert.ToInt32(rdr[0]), rdr[1].ToString(), rdr[2].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            //combobox2 feltöltése
            comboBox2.Items.Clear();
            foreach (customer item in customerek)
            {
                comboBox2.Items.Add(item.Nev);
            }
        }
        

        private void uj_rendeles()
        {
            //Elvitel- e a rendelés


            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //customer tábla record létrehozás
            try
            {
                conn.Open();
                string sql = $"INSERT INTO `customer`(`name`, `phoneNumber`) VALUES ('{textBox2.Text}','{textBox1.Text}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            //cutomerID kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT ID FROM `customer` ORDER BY ID DESC LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customerID = Convert.ToInt32(rdr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
            // destination tábla record létrehozás
            try
            {
                conn.Open();
                string sql = $"INSERT INTO `destination`(`orderType`, `orderStreet`, `orderStreetNum`, `customerID`) VALUES ('{elvitel}','{textBox3.Text}','{textBox4.Text}','{customerID}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            // DueDate összerakása a két DateTimePicker-ből
            DueDate = dateTimePicker1.Value.Date + timePicker.Value.TimeOfDay;

            //destinationID keresés az orders tábla destinationID mezőjéhez
            try
            {
                conn.Open();
                string sql = "SELECT ID FROM `destination` ORDER BY ID DESC LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    orderDestID = Convert.ToInt32(rdr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            // orders tábla record létrehozás
            try
            {
                conn.Open();
                string sql = $"INSERT INTO `orders`(`orderNote`, `orderTime`, `orderDueTime`, `orderDestID`, `orderDispatchID`, `orderStatus`, `foodID`, `drinkID`, `paymentID`) VALUES ('{richTextBox4.Text}','{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}','{DueDate.ToString("yyyy-MM-dd hh:mm:ss")}','{orderDestID}','[value-5]','0','[value-7]','[value-8]','0'   )";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            //fizetőeszköz eldöntése comboBox szerint (0=kp, 1=kártya)
            if (comboBox1.SelectedIndex == 0){fizeszk = 0;}
            else{fizeszk = 1;}

            //TODO: problémás orders-payment tábla kereszthivatkozás megcsinálása (a payment táblát az orders tábla után kell látrehozni, mert a payment táblának szüksége van az orders táblában kiválasztott ételek és italok ID-jára a szumma számoláshoz, valamint a kiválasztott ételek count-jára a csomagolás árának kiszámítására
            
            //payment tábla record létrehozás
            try
            {
                conn.Open();
                string sql = $"INSERT INTO `payment`(`paymentType`, `packagingCost`, `deliveryCost`, `sum`) VALUES ('{fizeszk}','[value-2]','[value-3]','[value-4]')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

        }

        private void Rendeles_felvetel_Load(object sender, EventArgs e)
        { 
            //placeholder attribútum hiányában ez a csunyaság van megoldásképp
            textBox1.Text = "Telefonszám";
            textBox2.Text = "Név:";
            textBox3.Text = "Utca:";
            textBox4.Text = "Házszám:";

            //DateTimePicker formázás
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy/MM/dd";

            //TimePicker formázás
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "HH:mm";
            timePicker.ShowUpDown = true;

            //customerek betöltése comboBox-ba
            customerek_betolt();

            elemek_betolt();
        }

        private void customer_select(object sender, EventArgs e)
        {
            int index;
            index=comboBox2.SelectedIndex;
            textBox1.Text = customerek[index].Nev;
            textBox2.Text = customerek[index].Telszam;
        }

        private void elvitel_ellenorzes(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) { 
                elvitel = 0; 
                textBox3.Text = "Étterem utcája";
                textBox4.Text = "Étterem hsz.";
            }
            else {
                textBox3.Text = "Utca:";
                textBox4.Text = "Házszám:";
                elvitel = 1; }

        }

        private void elemek_betolt()
        {
            flowLayoutPanel1.Controls.Clear();
            //TODO: watch?v=u71RJZm7Gdc&t=0s&ab_channel=AaricAaiden
            //Kapcsolódási adatok0
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
                    Menu.Add(new item(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), Convert.ToBoolean(Convert.ToInt32(rdr[3])), rdr[4].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            Menu_elemek[] menu_items = new Menu_elemek[Menu.Count];

            for (int i = 0; i < Menu.Count(); i++)
            {
                if (Menu[i].Elerheto)
                {
                    menu_items[i] = new Menu_elemek();
                    menu_items[i].Nev = Menu[i].Nev;
                    menu_items[i].Ar = Menu[i].Ar;
                    menu_items[i].Leiras = Menu[i].Leiras;
                    menu_items[i].Elerheto = Menu[i].Elerheto;
                    menu_items[i].Id = Menu[i].Id;

                    flowLayoutPanel1.Controls.Add(menu_items[i]);

                }
            }

        }

        //placeholder attribútum hiányában ez a csunyaság van megoldásképp
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Telefonszám")
            {
                textBox1.Text = "";
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Telefonszám";

            }
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Név:")
            {
                textBox2.Text = "";
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Név:";

            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Utca:")
            {
                textBox3.Text = "";
            }
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Utca:";

            }
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Házszám:")
            {
                textBox4.Text = "";
            }
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Házszám:";

            }
        }

    }
}
