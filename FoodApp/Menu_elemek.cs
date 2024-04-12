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
    public partial class Menu_elemek : UserControl
    { 
        public Menu_elemek()
        {
            InitializeComponent();
        }

        private void Menu_elemek_Load(object sender, EventArgs e)
        {
            adat_beiras();
        }

        private void adat_beiras()
        {

             label1.Text = nev;
             label2.Text = ar + "Ft";
             textBox1.Text = leiras;
             label3.Text="#"+id;
            label4.Text = allergenek;
        }

        private string nev;
        private string leiras;
        private string ar;
        private bool elerheto;
        private string id;
        private string allergenek;

        public string Nev { get => nev; set => nev = value; }
        public string Leiras { get => leiras; set => leiras = value; }
        public string Ar { get => ar; set => ar = value; }
        public bool Elerheto { get => elerheto; set => elerheto = value; }
        public string Id { get => id; set => id = value; }
        public string Allergenek { get => allergenek; set => allergenek = value; }

        private void hozzaadasBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                Rendeles_felvetel.drinkID.Add("d" + label3.Text.Trim('#'));
                textBox1.Hide();
            }
            else
            {
                Rendeles_felvetel.foodID.Add("f" + label3.Text.Trim('#'));
            }
        }
    }
}
