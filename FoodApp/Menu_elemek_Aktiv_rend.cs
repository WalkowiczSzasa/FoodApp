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
    public partial class Menu_elemek_Aktiv_rend : UserControl
    {
        public Menu_elemek_Aktiv_rend()
        {
            InitializeComponent();
        }
        private void Menu_elemek_Aktiv_rend_Load(object sender, EventArgs e)
        {
            adat_beiras();
        }

        private void adat_beiras()
        {

            label1.Text = nev;
            label2.Text = ar + "Ft";
            textBox1.Text = leiras;
            label3.Text = "#" + id;
        }

        private string nev;
        private string leiras;
        private string ar;
        private bool elerheto;
        private string id;

        public string Nev { get => nev; set => nev = value; }
        public string Leiras { get => leiras; set => leiras = value; }
        public string Ar { get => ar; set => ar = value; }
        public bool Elerheto { get => elerheto; set => elerheto = value; }
        public string Id { get => id; set => id = value; }


        private void hozzaadasBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                Aktiv_Rendelesek.drinkID.Add("d" + label3.Text.Trim('#'));
                textBox1.Hide();
            }
            else
            {
                Aktiv_Rendelesek.foodID.Add("f" + label3.Text.Trim('#'));
            }
        }
    }

}

