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
    public partial class Etel_Ital_felvetel : UserControl
    {
        public Etel_Ital_felvetel()
        {
            InitializeComponent();
        }

        byte checkboxvalue;
        string modositas="";
        string kategoria="food";
        List<food> foodMenu = new List<food>();
        List<drink> drinkMenu = new List<drink>();
        List<allergene> allergenes = new List<allergene>();
        public class food
        {
            private string nev;
            private string leiras;
            private string afa;
            private string ar;
            private bool elerheto;
            private string foodallergene;
            private string id;
            public food(string nev, string leiras, string afa, string ar, bool elerheto, string id, string foodallergene) => (this.nev, this.leiras, this.afa, this.ar, this.elerheto, this.id, this.foodallergene) = (nev, leiras, afa, ar, elerheto, id, foodallergene);

            public string Nev { get => nev; set => nev = value; }
            public string Leiras { get => leiras; set => leiras = value; }
            public string Afa { get => afa; set => afa = value; }
            public string Ar { get => ar; set => ar = value; }
            public bool Elerheto { get => elerheto; set => elerheto = value; }
            public string Id { get => id; set => id = value; }
            public string Foodallergene { get => foodallergene; set => foodallergene = value; }
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
        public class allergene
        {
            private string id;
            private string nev;

            public allergene(string id, string nev)
            {
                this.id = id;
                this.nev = nev;
            }

            public string Id { get => id; set => id = value; }
            public string Nev { get => nev; set => nev = value; }
        }
        public class adat
        {
            static string modositas = "n";
            static string nev;
            static string leiras;
            static string ar;
            static string allergen;
            static string afa;
            static string id;
            static bool elerhetoseg;

            public static string Modositas { get => modositas; set => modositas = value; }
            public static string Nev { get => nev; set => nev = value; }
            public static string Leiras { get => leiras; set => leiras = value; }
            public static string Ar { get => ar; set => ar = value; }
            public static string Allergen { get => allergen; set => allergen = value; }
            public static string Afa { get => afa; set => afa = value; }
            public static bool Elerhetoseg { get => elerhetoseg; set => elerhetoseg = value; }
            public static string Id { get => id; set => id = value; }
        }



        private void etelek_betolt()
        {
            tetelekFlowLayoutPanel.Controls.Clear();
            foodMenu.Clear();

            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string sql = "SELECT `foodName`,`foodDesc`,`foodVAT`,`foodPrice`,`foodStatus`,`ID`,`foodAllergenID`  FROM `food`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    foodMenu.Add(new food(rdr[0].ToString(), rdr[1].ToString(),rdr[2].ToString(), rdr[3].ToString(), Convert.ToBoolean(Convert.ToInt32(rdr[4])), rdr[5].ToString(), rdr[6].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            Menu_elmek_Etel_ital_felvetel[] ar_menu_items = new Menu_elmek_Etel_ital_felvetel[foodMenu.Count];

            for (int i = 0; i < foodMenu.Count(); i++)
            {
                ar_menu_items[i] = new Menu_elmek_Etel_ital_felvetel();
                ar_menu_items[i].Nev = foodMenu[i].Nev;
                ar_menu_items[i].Ar = foodMenu[i].Ar;
                ar_menu_items[i].Leiras = foodMenu[i].Leiras;
                ar_menu_items[i].Elerheto = foodMenu[i].Elerheto;
                ar_menu_items[i].Id = foodMenu[i].Id;
                ar_menu_items[i].AllergeneID = foodMenu[i].Foodallergene;
                ar_menu_items[i].Afa = foodMenu[i].Afa;
                tetelekFlowLayoutPanel.Controls.Add(ar_menu_items[i]);
            }

        }
        private void csomagolas_betolt()
        {
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string sql = "SELECT `price` FROM `packaging` WHERE ID=1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    csomagolasNumericUpDown.Value = Convert.ToInt32(rdr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
        private void italok_betolt()
        {
            drinkMenu.Clear();
            tetelekFlowLayoutPanel.Controls.Clear();

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

            Menu_elmek_Etel_ital_felvetel[] ar_Dmenu_items = new Menu_elmek_Etel_ital_felvetel[drinkMenu.Count];

            for (int i = 0; i < drinkMenu.Count(); i++)
            {
                    ar_Dmenu_items[i] = new Menu_elmek_Etel_ital_felvetel();
                    ar_Dmenu_items[i].Nev = drinkMenu[i].DNev;
                    ar_Dmenu_items[i].Ar = drinkMenu[i].DAr;
                    ar_Dmenu_items[i].Elerheto = drinkMenu[i].DElerheto;
                    ar_Dmenu_items[i].Id = drinkMenu[i].DId;

                    tetelekFlowLayoutPanel.Controls.Add(ar_Dmenu_items[i]);
            }
        }
        private void allergenek_betolt()
        {
            tetelekFlowLayoutPanel.Controls.Clear();
            allergenes.Clear();

            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT `allergeneID`, `allergeneName` FROM `allergene`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    allergenes.Add(new allergene(rdr[0].ToString(), rdr[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            Allergen[] ar_menu_items = new Allergen[allergenes.Count];

            for (int i = 0; i < allergenes.Count(); i++)
            {
                ar_menu_items[i] = new Allergen();
                ar_menu_items[i].Nev = allergenes[i].Nev;
                ar_menu_items[i].Id = allergenes[i].Id;

                tetelekFlowLayoutPanel.Controls.Add(ar_menu_items[i]);
            }

        }
        private void adatbeiras()
        {
            modositas = adat.Modositas;
            adat.Modositas = "n";
            modnevTextBox.Text = adat.Nev;
            modleirasTextBox.Text = adat.Leiras;
            modarTextBox.Text = adat.Ar;
            modallergenIdTextBox.Text = adat.Allergen;
            if (adat.Afa!="" && adat.Afa!=null)
            {
                modafaNumericUpDown.Value = Convert.ToInt32(adat.Afa.Trim('%'));
            }
            elerhetoCheckBox.Checked = adat.Elerhetoseg;
            ertek_nullazas();
        }
        private void mezok_kiurit()
        {
            nevTextBox.Clear();
            leirasTextBox.Clear();
            arTextBox.Clear();
            allergenIdTextBox.Clear();
            afaNumericUpDown.Value = 0;

            modnevTextBox.Clear();
            modleirasTextBox.Clear();
            modarTextBox.Clear();
            modallergenIdTextBox.Clear();
            modafaNumericUpDown.Value = 0;
            elerhetoCheckBox.Checked = false;
        }
        private void ertek_nullazas()
        {
            adat.Nev = "";
            adat.Leiras = "";
            adat.Ar = "";
            adat.Allergen = "";
            adat.Afa = "";
            adat.Elerhetoseg = false;
        }
        private void adatmodositas_ellenorzes()
        {
            switch (adat.Modositas)
            {
                case "food":
                    panel2.BringToFront();
                    modleirasTextBox.Enabled = true;
                    modarTextBox.Enabled = true;
                    modallergenIdTextBox.Enabled = true;
                    modafaNumericUpDown.Enabled = true;
                    elerhetoCheckBox.Enabled = true;
                    adatbeiras();
                    break;
                case "allergene":
                    panel2.BringToFront();
                    modleirasTextBox.Enabled = false;
                    modarTextBox.Enabled = false;
                    modallergenIdTextBox.Enabled = false;
                    modafaNumericUpDown.Enabled = false;
                    elerhetoCheckBox.Enabled = false;
                    adatbeiras();
                    break;
                case "drink":
                    panel2.BringToFront();
                    modleirasTextBox.Enabled = false;
                    modarTextBox.Enabled = true;
                    modallergenIdTextBox.Enabled = false;
                    modafaNumericUpDown.Enabled = false;
                    elerhetoCheckBox.Enabled = true;
                    adatbeiras();
                    break;
            }
        }
        private void checkbox_atalakitas() 
        {
            if (elerhetoCheckBox.Checked == true)
            {
                checkboxvalue = 1;
            }
            else
            {
                checkboxvalue = 0;
            }
        }
        private void elem_modositas() 
        {
            checkbox_atalakitas();
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                string sql = "";
                conn.Open();
                switch (modositas)
                {
                    case "food":
                        sql = $"UPDATE `{modositas}` SET `foodName`='{modnevTextBox.Text}',`foodDesc`='{modleirasTextBox.Text}',`foodVAT`='{modafaNumericUpDown.Value.ToString()}%',`foodPrice`='{modarTextBox.Text}',`foodAllergenID`='{modallergenIdTextBox.Text}',`foodStatus`='{checkboxvalue.ToString()}' WHERE ID='{adat.Id}'";
                        break;
                    case "drink":
                        sql = $"UPDATE `{modositas}` SET `drinkName`='{modnevTextBox.Text}',`drinkPrice`='{modarTextBox.Text}',`drinkStatus`='{checkboxvalue.ToString()}' WHERE ID='{adat.Id}'";
                        break;
                    case "allergene":
                        sql = $"UPDATE `{modositas}` SET `allergeneName`='{modnevTextBox.Text}' WHERE `allergeneID`='{adat.Id}'";
                        break;
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Elem sikeresen módosítva!");
                mezok_kiurit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void etelBtn_Click(object sender, EventArgs e)
        {
            kategoria = "food";

            //Gomb reszponzivitás
            ujElemPanel.BringToFront();
            etelek_betolt();
            etelBtn.BackColor = Color.FromArgb(146, 173, 44);
            etelBtn.ForeColor = Color.White;
            italBtn.BackColor = Color.White;
            italBtn.ForeColor = Color.Black;
            allergenBtn.BackColor = Color.White;
            allergenBtn.ForeColor = Color.Black;

            //Elem felvétel hibakezelés
            nevTextBox.Enabled = true;
            leirasTextBox.Enabled = true;
            arTextBox.Enabled = true;
            allergenIdTextBox.Enabled = true;
            afaNumericUpDown.Enabled = true;
        }

        private void italBtn_Click(object sender, EventArgs e)
        {
            kategoria = "drink";

            //Gomb reszponzivitás
            ujElemPanel.BringToFront();
            italok_betolt();
            italBtn.BackColor = Color.FromArgb(146, 173, 44);
            italBtn.ForeColor = Color.White;
            etelBtn.BackColor = Color.White;
            etelBtn.ForeColor = Color.Black;
            allergenBtn.BackColor = Color.White;
            allergenBtn.ForeColor = Color.Black;

            //Elem felvétel hibakezelés
            arTextBox.Enabled = true;
            leirasTextBox.Enabled = false;
            allergenIdTextBox.Enabled = false;
            afaNumericUpDown.Enabled = false;
        }

        private void allergenBtn_Click(object sender, EventArgs e)
        {
            kategoria = "allergene";

            //Gomb reszponzivitás
            ujElemPanel.BringToFront();
            allergenek_betolt();
            allergenBtn.BackColor = Color.FromArgb(146, 173, 44);
            allergenBtn.ForeColor = Color.White;
            italBtn.BackColor = Color.White;
            italBtn.ForeColor = Color.Black;
            etelBtn.BackColor = Color.White;
            etelBtn.ForeColor = Color.Black;

            //Elem felvétel hibakezelés
            arTextBox.Enabled = false;
            leirasTextBox.Enabled = false;
            allergenIdTextBox.Enabled = false;
            afaNumericUpDown.Enabled = false;
        }

        private void elemFelvetelButton_Click(object sender, EventArgs e)
        {

            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql="";
                switch (kategoria)
                {
                    case "food":
                         sql = $"INSERT INTO `food`(`foodName`, `foodDesc`, `foodVAT`, `foodPrice`, `foodAllergenID`, `foodStatus`) VALUES ('{nevTextBox.Text}','{leirasTextBox.Text}','{afaNumericUpDown.Value.ToString()}%','{arTextBox.Text}','{allergenIdTextBox.Text}','0')";
                        break;
                    case "drink":
                        sql = $"INSERT INTO `drink`(`drinkName`, `drinkPrice`, `drinkStatus`) VALUES ('{nevTextBox.Text}','{arTextBox.Text}','0')";
                        break;
                    case "allergene":
                        sql = $"INSERT INTO `allergene`(`allergeneName`) VALUES ('{nevTextBox.Text}')";
                        break;
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Elem sikeresen felvéve az adatbázisba!");
                mezok_kiurit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az elem felvétele közben!");
                MessageBox.Show(ex.Message);
            }
            switch (kategoria)
            {
                case "food":
                    etelek_betolt();
                    break;
                case "drink":
                    italok_betolt();
                    break;
                case "allergene":
                    allergenek_betolt();
                    break;
            }
        }

        private void elemModositBtn_Click(object sender, EventArgs e)
        {
            elem_modositas();
        }

        private void Etel_Ital_felvetel_Load(object sender, EventArgs e)
        {
            etelek_betolt();
            csomagolas_betolt();
        }

        private void tetelekFlowLayoutPanel_MouseEnter(object sender, EventArgs e)
        {
            adatmodositas_ellenorzes();
        }

        private void tetelekFlowLayoutPanel_MouseLeave(object sender, EventArgs e)
        {
            adatmodositas_ellenorzes();
        }

        private void csomagMentesBtn_Click(object sender, EventArgs e)
        {
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                string sql = "";
                conn.Open();
                sql = $"UPDATE `packaging` SET `price`='{csomagolasNumericUpDown.Value.ToString()}' WHERE ID=1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Csomagolás ára sikeresen módosítva!");
                mezok_kiurit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }
    }
}
