﻿using System;
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
    public partial class Futar_cimek : UserControl
    {

        private string id, nev, telszam, cim, ar;
        public static List<string> foodID = new List<string>();
        public static List<string> drinkID = new List<string>();
        public string fizID, cimID, customerID, foodIDs, drinkIDs;
        public static string fiztip;
        public byte checkClick = 0;
        private bool allapot;
        private DateTime duetime;
        List<futar> futarok = new List<futar>();
        List<tetel> rendeles_tetelek = new List<tetel>();

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

        public class futar
        {
            private string id;
            private string nev;

            public futar(string id, string nev)
            {
                this.id = id;
                this.nev = nev;
            }

            public string Id { get => id; set => id = value; }
            public string Nev { get => nev; set => nev = value; }
        }
        public string Id { get => id; set => id = value; }
        public string Nev { get => nev; set => nev = value; }
        public string Telszam { get => telszam; set => telszam = value; }
        public string Cim { get => cim; set => cim = value; }
        public bool Allapot { get => allapot; set => allapot = value; }
        public string Ar { get => ar; set => ar = value; }
        public DateTime Duetime { get => duetime; set => duetime = value; }

        public Futar_cimek()
        {
            InitializeComponent();
        }


        private void Futar_cimek_Load(object sender, EventArgs e)
        {
            fiztip_lekerdezes();
            adat_beiras();
            id_lekeres();
            rend_tetelekFeltoltes();
            
        }

        private void id_lekeres()
        {
            string orderID = id_Label.Text.Trim('#');

            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //dispatchID és név kiválasztása a users táblából combobox feltöltéshez
            try
            {
                conn.Open();
                string sql = $"SELECT `foodID`, `drinkID` FROM `orders` WHERE `orderID`='{orderID}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    foodID.Add(rdr[0].ToString());
                    drinkID.Add(rdr[1].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

        }

        private void fiztip_lekerdezes()
        {
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = $"SELECT `paymentType` FROM `payment` WHERE `ID`='{fizID}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    fiztip = rdr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        /* private void futarok_betolt()
         {
             tetelComboBox.Items.Clear();
             //Kapcsolódási adatok
             string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
             MySqlConnection conn = new MySqlConnection(connStr);

             try
             {
                 conn.Open();
                 string sql = "SELECT ID, dname FROM `users` WHERE role='dispatch' ORDER BY dname ASC;";
                 MySqlCommand cmd = new MySqlCommand(sql, conn);
                 MySqlDataReader rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     futarok.Add(new futar(rdr[0].ToString(), rdr[1].ToString()));
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
             conn.Close();
         }*/
        private void adat_beiras()
        {
            id_Label.Text = "#" + id;
            nevLabel.Text = nev;
            telszamLabel.Text = telszam;
            cimLabel.Text = cim;
            arLabel.Text = ar + "Ft";
            hatarido_kezeles();
        }

        private void hatarido_kezeles()
        {
            DateTime ma = DateTime.Now;
            if (duetime.Date > ma.Date)
            {
                idoLabel.Text = Duetime.ToString("HH:mm tt");
                dueDayLabel.Text = Duetime.ToString("MM/dd");
            }
            else
            {
                idoLabel.Text = Duetime.ToString("HH:mm");
                dueDayLabel.Hide();
            }
        }

        public void rend_tetelekFeltoltes()
        {
            tetelcomboBox.Items.Clear();
            rendeles_tetelek.Clear();

            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            foreach (var item in foodID)
            {
                List<string> food = item.Split(' ').ToList();
                //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
                try
                {
                    if (food.Count > 0 && food[0] != "")
                    {
                        for (int i = 0; i < food.Count; i++)
                        {
                            conn.Open();
                            string sql = $"SELECT ID, foodName, foodPrice FROM `food` WHERE ID={food[i].TrimStart('f')} ORDER BY foodName ASC;";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            MySqlDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                rendeles_tetelek.Add(new tetel(food[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));
                            }
                            conn.Close();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            foreach (var item in drinkID)
            {
                if (item!="" && item!=null)
                {
                    List<string> drink = item.Split(' ').ToList();
                    if (drink.Count > 0 && drink[0] != "")
                    {
                        for (int i = 0; i < drink.Count; i++)
                        {
                            conn.Open();
                            string sql = $"SELECT ID, drinkName, drinkPrice FROM `drink` WHERE ID={drink[i].TrimStart('d')} ORDER BY drinkName ASC;";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            MySqlDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                rendeles_tetelek.Add(new tetel(drink[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));
                            }
                            conn.Close();
                        }
                    }
                }

            }
            foreach (tetel item in rendeles_tetelek)
            {
                 tetelcomboBox.Items.Add(item.TetelID + " " + item.TetelName);
            }
            tetelcomboBox.Sorted = true;
            /*
            string[] items = new string[tetelcomboBox.Items.Count];
            tetelcomboBox.Items.CopyTo(items, 0);
            tetelcomboBox.Items.Clear();
            LinqMethod(items);
            */
        }
        /*private void LinqMethod(string[] items)
        {
            var ranking = (from item in items
                           group item by item into r
                           orderby r.Count() descending
                           select new { Name = r.Key, Rank = r.Count() });
            foreach (var item in ranking)
                tetelcomboBox.Items.Add(item);
        }*/
    }
}