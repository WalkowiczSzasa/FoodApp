using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace FoodApp
{

    public partial class Bejelentkezes : Form
    {

        public static string dname;
        public Bejelentkezes()
        {
            InitializeComponent();
            label4.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bejelentkezes();
        }

        private void bejelentkezes()
        {//Bejlentkezés
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = $"SELECT role, dname FROM `users` WHERE username='{textBox2.Text}' AND password='{textBox1.Text}'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataTable dtbl = new DataTable();
                da.Fill(dtbl);
                conn.Close();
                if (dtbl.Rows.Count == 1)
                {
                    FoodApp form = new FoodApp();
                    foreach (DataRow row in dtbl.Rows)
                    {
                        form.Dname = row["dname"].ToString();
                        form.role = row["role"].ToString();
                    }
                    form.ShowDialog();
                }
                else {label4.Show();}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

    }
}
