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
                    button5.Hide();
                    button6.Hide();
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button3.Height;
            KijeloltPanel.Top = button3.Top;
            aktiv_Rendelesek1.BringToFront();
            exitBtn.BringToFront();
            aktiv_Rendelesek1.rendelesek_betolt();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button4.Height;
            KijeloltPanel.Top = button4.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button5.Height;
            KijeloltPanel.Top = button5.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button6.Height;
            KijeloltPanel.Top = button6.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button7.Height;
            KijeloltPanel.Top = button7.Top;
            rendeles_felvetel2.BringToFront();
            exitBtn.BringToFront();
        }


    }
}
