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
        public Rendeles_felvetel()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            uj_rendeles();
        }

        private void uj_rendeles()
        {
            if (checkBox1.Checked == true)
            {
                elvitel = 0;
            }
            else
            {
                elvitel = 1;
            }


            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql2 = $"INSERT INTO `customer`(`name`, `phoneNumber`) VALUES ('{textBox1.Text}','{textBox2.Text}')";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();


            try
            {
                conn.Open();
                string sql1 = $"INSERT INTO `destination`(`orderType`, `orderStreet`, `orderStreetNum`, `customerID`) VALUES ('{elvitel}','{textBox3.Text}','{textBox4.Text}','[value-4]')";
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            DueDate = dateTimePicker1.Value.Date + timePicker.Value.TimeOfDay;


            try
            {
                conn.Open();
                string sql = $"INSERT INTO `orders`(`orderNote`, `orderTime`, `orderDueTime`, `orderDestID`, `orderDispatchID`, `orderStatus`, `foodID`, `drinkID`, `paymentID`) VALUES ('[value-1]','[value-2]','{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}','{DueDate.ToString("yyyy-MM-dd hh:mm:ss")}','[value-5]','[value-6]','[value-7]','[value-8]','[value-9]'   )";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            if (comboBox1.SelectedIndex == 0)
            {
                fizeszk = 0;
            }
            else
            {
                fizeszk = 1;
            }

            try
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
        {
            textBox1.Text = "Telefonszám";
            textBox2.Text = "Név:";
            textBox3.Text = "Utca:";
            textBox4.Text = "Házszám:";

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy/MM/dd";

            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "HH:mm";
            timePicker.ShowUpDown = true;
        }
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
