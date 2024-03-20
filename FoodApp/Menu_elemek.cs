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
    public partial class Menu_elemek : UserControl
    {

        public class item
        {
            private string nev;
            private string leiras;
            private string ar;
            private bool elerheto;
            public item(string nev, string leiras, string ar, bool elerheto) => (this.nev, this.leiras, this.ar, this.elerheto) = (nev, leiras, ar, elerheto);

            public string Nev { get => nev; set => nev = value; }
            public string Leiras { get => leiras; set => leiras = value; }
            public string Ar { get => ar; set => ar = value; }
            public bool Elerheto { get => elerheto; set => elerheto = value; }
        }
        List<item> Menu = new List<item>();

        public Menu_elemek()
        {
            InitializeComponent();
        }

        private void Menu_elemek_Load(object sender, EventArgs e)
        {

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
                string sql = "SELECT `foodName`, `foodDesc`,`foodPrice`,`foodStatus` FROM `food`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Menu.Add(new item(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), Convert.ToBoolean(Convert.ToInt32(rdr[3]))));
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
