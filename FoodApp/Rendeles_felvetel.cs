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
        private byte elvitel=1;
        private byte fizeszk;
        private DateTime DueDate = new DateTime();
        private int customerID, orderDestID;
        double kiszDij;


        List<food> foodMenu = new List<food>();
        List<drink> drinkMenu = new List<drink>();
        List<customer> customerek = new List<customer>();
        public static List<string> foodID = new List<string>();
        public static List<string> drinkID = new List<string>();
        List<tetel> rendeles_tetelek = new List<tetel>();

        public class food
        {
            private string nev;
            private string leiras;
            private string ar;
            private bool elerheto;
            private string id;
            private string allergenes;
            public food(string nev, string leiras, string ar, bool elerheto, string id, string allergenes) => (this.nev, this.leiras, this.ar, this.elerheto, this.id, this.allergenes) = (nev, leiras, ar, elerheto, id, allergenes);

            public string Nev { get => nev; set => nev = value; }
            public string Leiras { get => leiras; set => leiras = value; }
            public string Ar { get => ar; set => ar = value; }
            public bool Elerheto { get => elerheto; set => elerheto = value; }
            public string Id { get => id; set => id = value; }
            public string Allergenes { get => allergenes; set => allergenes = value; }
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
        public class tetel {
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

        public Rendeles_felvetel()
        {
            InitializeComponent();
        }

        private void Rendeles_felvetel_Load(object sender, EventArgs e)
        {
            hibaLabel.Hide();

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
            szalldij_betolt();
            etelek_betolt();
        }

        private void mentesBtn_Click(object sender, EventArgs e)
        {
            ures_rendeles_kezeles();
        }

        private void ures_rendeles_kezeles()
        {
            hibaLabel.Font = new Font("Century Gothic", 12);
            hibaLabel.Hide();
            if (textBox1.Text=="Telefonszám")
            {
                hibaLabel.Text ="A telefonszám mező üres!";
                hibaLabel.Show();
            }
            else if (textBox2.Text == "Név:")
            {
                hibaLabel.Text = "A név mező üres!";
                hibaLabel.Show();
            }
            else if (textBox3.Text == "Utca:")
            {
                hibaLabel.Text = "Az utca mező üres!";
                hibaLabel.Show();
            }
            else if (textBox4.Text == "Házszám:")
            {
                hibaLabel.Text = "A házszám mező üres!";
                hibaLabel.Show();
            }
            else if (comboBox1.SelectedItem==""||comboBox1.SelectedItem==null)
            {
                hibaLabel.Text = "Válassz egy fizetési módot!";
                hibaLabel.Show();
            }
            else if (foodID.Count()==0 && drinkID.Count()==0)
            {
                hibaLabel.Text = "A rendelésnek nincsenek tételei!";
                hibaLabel.Font = new Font("Century Gothic" , 9);
                hibaLabel.Show();
            }
            else
            {
                szalldij_betolt();
                uj_rendeles();
                customerek_betolt();
                mezok_kiurit();
            }
        }
        #region
        private void etelBtn_Click(object sender, EventArgs e)
        {
            etelek_betolt();
        }

        private void italBtn_Click(object sender, EventArgs e)
        {
            italok_betolt();
        }

        private void refreshPctbox_Click(object sender, EventArgs e)
        {
            rend_tetelekFeltoltes();
        }

        private void tetelAdd_Click(object sender, EventArgs e)
        {
            string addID = kivalasztott_tetelTextBox.Text.Split(' ').FirstOrDefault();
            if (addID.Contains('d'))
            {
                drinkID.Add(addID);
            }
            else
            {
                foodID.Add(addID);
            }
            rend_tetelekFeltoltes();
        }

        private void tetelMinus_Click(object sender, EventArgs e)
        {
            string addID = kivalasztott_tetelTextBox.Text.Split(' ').FirstOrDefault();
            if (addID.Contains('d'))
            {
                drinkID.Remove(addID);
            }
            else
            {
                foodID.Remove(addID);
            }
            rend_tetelekFeltoltes();
        }


        private void szalldij_betolt()
        {
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                string sql = "SELECT `price` FROM `deliveryfee` WHERE ID=1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    kiszDij = Convert.ToDouble(rdr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void customerek_betolt()
        {
            comboBox2.Items.Clear();
            customerek.Clear();
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

            foreach (customer item in customerek)
            {
                comboBox2.Items.Add(item.Nev);
            }
            comboBox2.SelectedIndex = -1;
        }

        private void uj_rendeles()
        {
            //Elvitel- e a rendelés
            string fizID="";

            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //customer tábla record létrehozás
            try
            {
                conn.Open();
                if (comboBox2.SelectedIndex==-1)
                {
                    string sql = $"INSERT INTO `customer`(`name`, `phoneNumber`) VALUES ('{textBox2.Text}','{textBox1.Text}')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                else if(comboBox2.SelectedIndex!=-1 && textBox2.Text!=comboBox2.SelectedItem.ToString()){
                    string sql = $"UPDATE customer SET name='{textBox2.Text}' WHERE name='{comboBox2.SelectedItem}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            //customerID kiválasztása a destination tábla customerID mező kitöltéséhez
            if (comboBox2.SelectedIndex>-1)
            {
                try
                {
                    conn.Open();
                    string sql = $"SELECT ID FROM `customer` WHERE name='{comboBox2.SelectedItem.ToString()}'";
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
            }
            else
            {
                try
                {
                    conn.Open();
                    string sql = $"SELECT ID FROM `customer` WHERE name='{textBox2.Text}'";
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
                foodID.Sort();
                drinkID.Sort();
                conn.Open();
                string sql = $"INSERT INTO `orders`(`orderNote`, `orderTime`, `orderDueTime`, `orderDestID`, `orderDispatchID`, `orderStatus`, `foodID`, `drinkID`, `paymentID`) VALUES ('{richTextBox4.Text}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt")}','{DueDate.ToString("yyyy-MM-dd HH:mm:ss tt")}','{orderDestID}','[value-5]','0','{string.Join(" ", foodID)}','{string.Join(" ", drinkID)}','0'   )";
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

            //payment tábla record létrehozás
            if (elvitel==1)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO `payment`(`paymentType`, `packagingCost`, `deliveryCost`, `sum`) VALUES ('{fizeszk}','[value-2]','{kiszDij}','[value-4]')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }
            else
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO `payment`(`paymentType`, `packagingCost`, `deliveryCost`, `sum`) VALUES ('{fizeszk}','[value-2]','0','[value-4]')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conn.Close();
            }

            try
            {
                conn.Open();
                string sql = $"SELECT `ID` FROM `payment` ORDER BY ID DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    fizID = rdr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            try
            {
                conn.Open();
                string sql = $"UPDATE `orders` SET `paymentID`={fizID} ORDER BY orderID DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void mezok_kiurit()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            checkBox1.Checked = false;
            comboBox2.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            richTextBox4.Clear();
            rend_tetelek.Items.Clear();
            kivalasztott_tetelTextBox.Clear();
            foodID.Clear();
            drinkID.Clear();
        }

        private void etelek_betolt()
        {
            menuFlowLayoutPanel.Controls.Clear();
            foodMenu.Clear();
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                conn.Open();
                string sql = "SELECT `foodName`, `foodDesc`,`foodPrice`,`foodStatus`,`ID`,`foodAllergenID`   FROM `food`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    foodMenu.Add(new food(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), Convert.ToBoolean(Convert.ToInt32(rdr[3])), rdr[4].ToString(), rdr[5].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

            Menu_elemek[] menu_items = new Menu_elemek[foodMenu.Count];

            for (int i = 0; i < foodMenu.Count(); i++)
            {
                if (foodMenu[i].Elerheto)
                {
                    menu_items[i] = new Menu_elemek();
                    menu_items[i].Nev = foodMenu[i].Nev;
                    menu_items[i].Ar = foodMenu[i].Ar;
                    menu_items[i].Leiras = foodMenu[i].Leiras;
                    menu_items[i].Elerheto = foodMenu[i].Elerheto;
                    menu_items[i].Id = foodMenu[i].Id;
                    menu_items[i].Allergenek = foodMenu[i].Allergenes;

                    menuFlowLayoutPanel.Controls.Add(menu_items[i]);

                }
            }

        }

        private void italok_betolt()
        {
            drinkMenu.Clear();
            menuFlowLayoutPanel.Controls.Clear();
            //TODO: watch?v=u71RJZm7Gdc&t=0s&ab_channel=AaricAaiden
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

            Menu_elemek[] Dmenu_items = new Menu_elemek[drinkMenu.Count];

            for (int i = 0; i < drinkMenu.Count(); i++)
            {
                if (drinkMenu[i].DElerheto)
                {
                    Dmenu_items[i] = new Menu_elemek();
                    Dmenu_items[i].Nev = drinkMenu[i].DNev;
                    Dmenu_items[i].Ar = drinkMenu[i].DAr;
                    Dmenu_items[i].Elerheto = drinkMenu[i].DElerheto;
                    Dmenu_items[i].Id = drinkMenu[i].DId;

                    menuFlowLayoutPanel.Controls.Add(Dmenu_items[i]);

                }
            }
        }

        private void rend_tetelekFeltoltes()
        {
            rend_tetelek.Items.Clear();
            rendeles_tetelek.Clear();
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);

            //cutomerID és név kiválasztása a destination tábla customerID mező kitöltéséhez
            try
            {
                if (foodID.Count > 0)
                {
                    for (int i = 0; i < foodID.Count; i++)
                    {
                        conn.Open();
                        string sql = $"SELECT ID, foodName, foodPrice FROM `food` WHERE ID={foodID[i].TrimStart('f')} ORDER BY foodName ASC;";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            rendeles_tetelek.Add(new tetel(foodID[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));
                        }
                        conn.Close();
                    }
                }
                if (drinkID.Count > 0)
                {
                    for (int i = 0; i < drinkID.Count; i++)
                    {
                        conn.Open();
                        string sql = $"SELECT ID, drinkName, drinkPrice FROM `drink` WHERE ID={drinkID[i].TrimStart('d')} ORDER BY drinkName ASC;";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            rendeles_tetelek.Add(new tetel(drinkID[i], rdr[1].ToString(), Convert.ToDouble(rdr[2])));
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            foreach (tetel item in rendeles_tetelek)
            {
                rend_tetelek.Items.Add(item.TetelID + " " + item.TetelName + "\t" + item.TetelPrice);
            }
            rend_tetelek.Sorted = true;
        }

        private void customer_select(object sender, EventArgs e)
        {
            int index;
            index = comboBox2.SelectedIndex;
            textBox2.Text = customerek[index].Nev;
            textBox1.Text = customerek[index].Telszam;
        }

        private void elvitel_ellenorzes(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                elvitel = 0;
                textBox3.Text = "Étterem utcája";
                textBox4.Text = "Étteremhsz.";
            }
            else
            {
                textBox3.Text = "Utca:";
                textBox4.Text = "Házszám:";
                elvitel = 1;
            }

        }

        private void rend_tetelek_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (rend_tetelek.SelectedItem.ToString() != "" && rend_tetelek.SelectedItem.ToString() != null)
            {
                string kiv_tetel = rend_tetelek.SelectedItem.ToString();
                kivalasztott_tetelTextBox.Text = kiv_tetel.TrimEnd('\t').Remove(kiv_tetel.LastIndexOf('\t') + 1);
            }
        }



        //rend_tetelek fél-automata frissítés
        private void menuFlowLayoutPanel_MouseEnter(object sender, EventArgs e)
        {
            rend_tetelekFeltoltes();
        }
        private void menuFlowLayoutPanel_MouseLeave(object sender, EventArgs e)
        {
            rend_tetelekFeltoltes();
        }


        #endregion
        //placeholder attribútum hiányában ez a csunyaság van megoldásképp
        #region Makeshift Placeholder
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
                textBox1.Text = "Telefonszám:";

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
        #endregion
    }
}