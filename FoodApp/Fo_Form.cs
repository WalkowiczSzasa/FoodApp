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
    public partial class FoodApp : Form
    {//Változók átvétele
        public string Dname { get; set; }
        public string role { get; set; }

        public FoodApp()
        {
            InitializeComponent();
            KijeloltPanel.Height = rendeles_felvetelBtn.Height;
            KijeloltPanel.Top = rendeles_felvetelBtn.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
        }
        private void FoodApp_Load(object sender, EventArgs e)
        {
            label2.Text = Dname;
            role_kezeles();
            allergene_lekerdezes();
        }

        private void role_kezeles()
        {
            switch (role)
            {
                case "admmin":
                    futar_nezet1.Show();
                    aktiv_Rendelesek1.Show();
                    rendeles_felvetel2.Show();
                    etel_Ital_felvetel1.Show();
                    rendeles_felvetelBtn.Show();
                    aktiv_rendelesekBtn.Show();
                    etel_ital_felvetelBtn.Show();
                    futar_nezetBtn.Show();
                    break;
                case "cook":
                    futar_nezet1.Hide();
                    aktiv_Rendelesek1.Show();
                    rendeles_felvetel2.Show();
                    etel_Ital_felvetel1.Hide();
                    rendeles_felvetelBtn.Show();
                    aktiv_rendelesekBtn.Show();
                    etel_ital_felvetelBtn.Hide();
                    futar_nezetBtn.Hide();
                    break;
                case "dispatch":
                    futar_nezet1.Show();
                    aktiv_Rendelesek1.Hide();
                    rendeles_felvetel2.Hide();
                    etel_Ital_felvetel1.Hide();
                    rendeles_felvetelBtn.Hide();
                    aktiv_rendelesekBtn.Hide();
                    etel_ital_felvetelBtn.Hide();
                    futar_nezetBtn.Show();
                    break;
            }
        }
        private void allergene_lekerdezes()
        {
            //Kapcsolódási adatok
            string connStr = "server=localhost;user=asd;database=restaurantapp;port=3306;password=asd";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT `allergeneID`,`allergeneName` FROM `allergene`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    allergeneListBox.Items.Add(rdr[0]+"\t"+rdr[1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Bejelentkezes login = new Bejelentkezes();
            this.Hide();
            login.Show();

        }

        private void rend_felvetel_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = rendeles_felvetelBtn.Height;
            KijeloltPanel.Top = rendeles_felvetelBtn.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
        }

        private void aktiv_rend_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = aktiv_rendelesekBtn.Height;
            KijeloltPanel.Top = aktiv_rendelesekBtn.Top;
            aktiv_Rendelesek1.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
            aktiv_Rendelesek1.rendelesek_betolt();
        }

        private void etel_ital_felvetel_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = etel_ital_felvetelBtn.Height;
            KijeloltPanel.Top = etel_ital_felvetelBtn.Top;
            etel_Ital_felvetel1.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
        }
        private void futar_nezet_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = futar_nezetBtn.Height;
            KijeloltPanel.Top = futar_nezetBtn.Top;
            futar_nezet1.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
        }

        #region Ablak mozgatás
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        private void Fo_Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion


    }
}
