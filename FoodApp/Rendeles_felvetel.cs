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
        public Rendeles_felvetel()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TODO: food és drink tábla elemeinek kilistázása
            uj_rendeles();
        }

        private void uj_rendeles()
        {
            //Elvitel- e a rendelés
            if (checkBox1.Checked == true){elvitel = 0;}
            else {elvitel = 1;}

            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try //customer tábla record létrehozás
            {
                conn.Open();
                string sql2 = $"INSERT INTO `customer`(`name`, `phoneNumber`) VALUES ('{textBox2.Text}','{textBox1.Text}')";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            try //cutomerID kiválasztása a destination tábla customerID mező kitöltéséhez
            {
                conn.Open();
                string sql4 = "SELECT ID FROM `customer` ORDER BY ID DESC LIMIT 1;";
                MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                MySqlDataReader rdr = cmd4.ExecuteReader();
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

            try // destination tábla record létrehozás
            {
                conn.Open();
                string sql1 = $"INSERT INTO `destination`(`orderType`, `orderStreet`, `orderStreetNum`, `customerID`) VALUES ('{elvitel}','{textBox3.Text}','{textBox4.Text}','{customerID}')";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            DueDate = dateTimePicker1.Value.Date + timePicker.Value.TimeOfDay;// DueDate összerakása a két DateTimePicker-ből
            try //destinationID keresés az orders tábla destinationID mezőjéhez
            {
                conn.Open();
                string sql5 = "SELECT ID FROM `destination` ORDER BY ID DESC LIMIT 1;";
                MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                MySqlDataReader rdr = cmd5.ExecuteReader();
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
            
            try// orders tábla record létrehozás
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

            if (comboBox1.SelectedIndex == 0)//fizetőeszköz eldöntése comboBox szerint (0=kp, 1=kártya)
            {
                fizeszk = 0;
            }
            else
            {
                fizeszk = 1;
            }
            //TODO: problémás orders-payment tábla kereszthivatkozás megcsinálása (a payment táblát az orders tábla után kell látrehozni, mert a payment táblának szüksége van az orders táblában kiválasztott ételek és italok ID-jára a szumma számoláshoz, valamint a kiválasztott ételek count-jára a csomagolás árának kiszámítására
            try//payment tábla record létrehozás
            {
                conn.Open();
                string sql3 = $"INSERT INTO `payment`(`paymentType`, `packagingCost`, `deliveryCost`, `sum`) VALUES ('{fizeszk}','[value-2]','[value-3]','[value-4]')";
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                cmd3.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

        }

        private void Rendeles_felvetel_Load(object sender, EventArgs e)
        { //placeholder attribútum hiányában ez a csunyaság van megoldásképp
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
        }//placeholder attribútum hiányában ez a csunyaság van megoldásképp
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
