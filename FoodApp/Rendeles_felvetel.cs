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


        public class customer
        {
            private int id;
            private string nev;
            public customer(int id, string nev) => (this.id, this.nev) = (id, nev);

            public int Id { get => id; set => id = value; }
            public string Nev { get => nev; set => nev = value; }
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

        }

        private void customerek_betolt()
        {
            //Kapcsolódási adatok0
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT ID, name FROM `customer` ORDER BY name ASC;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customerek.Add(new customer(Convert.ToInt32(rdr[0]), rdr[1].ToString()));
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
            if (checkBox1.Checked == true){elvitel = 0;}
            else {elvitel = 1;}

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
            elmek_betolt();
        }

        private void elmek_betolt()
        {
            flowLayoutPanel1.Controls.Clear();
            //TODO: watch?v=u71RJZm7Gdc&t=0s&ab_channel=AaricAaiden
            
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
