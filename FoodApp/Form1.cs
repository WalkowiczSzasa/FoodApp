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
    {
        public FoodApp()
        {
            InitializeComponent();
            KijeloltPanel.Height = button2.Height;
            KijeloltPanel.Top = button2.Top;
            rendeles_felvetel2.BringToFront();
            button1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
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
            button1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button3.Height;
            KijeloltPanel.Top = button3.Top;
            aktiv_Rendelesek1.BringToFront();
            button1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button4.Height;
            KijeloltPanel.Top = button4.Top;
            rendeles_felvetel2.BringToFront();
            button1.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button5.Height;
            KijeloltPanel.Top = button5.Top;
            rendeles_felvetel2.BringToFront();
            button1.BringToFront();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            KijeloltPanel.Height = button6.Height;
            KijeloltPanel.Top = button6.Top;
            rendeles_felvetel2.BringToFront();
            button1.BringToFront();
        }
    }
}
