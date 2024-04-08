using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodApp
{
    public partial class FoodApp : Form
    {//Változók átvétele
        public string Dname { get; set; }
        public string role { get; set; }

        public FoodApp()
        {
            InitializeComponent();
            KijeloltPanel.Height = button2.Height;
            KijeloltPanel.Top = button2.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
        }
        private void FoodApp_Load(object sender, EventArgs e)
        {
            label2.Text = Dname;
            role_kezeles();
        }

        private void role_kezeles()
        {
            switch (role)
            {
                case "cook":
                    button7.Hide();
                    break;
                case "dispatch":

                    break;
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Bejelentkezes login = new Bejelentkezes();
            this.Hide();
            login.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button2.Height;
            KijeloltPanel.Top = button2.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button3.Height;
            KijeloltPanel.Top = button3.Top;
            aktiv_Rendelesek1.BringToFront();
            exitBtn.BringToFront();
            move_PictureBox.BringToFront();
            aktiv_Rendelesek1.rendelesek_betolt();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button7.Height;
            KijeloltPanel.Top = button7.Top;
            rendeles_felvetel2.BringToFront();
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
