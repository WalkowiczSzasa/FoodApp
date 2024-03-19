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
        {//If its stupid and it works its not stupid
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try{
                conn.Open();
                string sql = $"SELECT * FROM `admin` WHERE username='{textBox2.Text}' AND password='{textBox1.Text}'";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataTable dtbl = new DataTable();
                da.Fill(dtbl);
                conn.Close();
                label4.Show();
                if (dtbl.Rows.Count != 1)
                {
                    conn.Open();
                    string sql1 = $"SELECT * FROM `cook` WHERE username='{textBox2.Text}' AND password='{textBox1.Text}'";
                    MySqlDataAdapter da1 = new MySqlDataAdapter(sql1, conn);
                    DataTable dtbl1 = new DataTable();
                    da1.Fill(dtbl1);
                    conn.Close();
                    label4.Show();
                    if (dtbl1.Rows.Count != 1)
                    {
                        conn.Open();
                        string sql2 = $"SELECT * FROM `dispatch` WHERE username='{textBox2.Text}' AND password='{textBox1.Text}'";
                        MySqlDataAdapter da2 = new MySqlDataAdapter(sql2, conn);
                        DataTable dtbl2 = new DataTable();
                        da2.Fill(dtbl2);
                        conn.Close();
                        label4.Show();
                        if (dtbl2.Rows.Count == 1)
                        {
                            foreach (DataRow row in dtbl2.Rows)
                            {
                                dname = row["dname"].ToString();
                            }
                            this.Hide();
                            FoodApp form = new FoodApp();
                            form.Dname = dname;
                            form.ShowDialog();
                        }
                    }
                    else
                    {
                        foreach (DataRow row in dtbl1.Rows)
                        {
                            dname = row["dname"].ToString();
                        }
                        this.Hide();
                        FoodApp form = new FoodApp();
                        form.Dname = dname;
                        form.ShowDialog();
                    }
                }
                else
                {
                    foreach (DataRow row in dtbl.Rows)
                    {
                        dname = row["dname"].ToString();
                    }
                    this.Hide();
                    FoodApp form = new FoodApp();
                    form.Dname = dname;
                    form.ShowDialog();
                }
                
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());


            }
            conn.Close();
        }

    }
}
