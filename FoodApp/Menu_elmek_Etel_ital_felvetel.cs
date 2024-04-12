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
    public partial class Menu_elmek_Etel_ital_felvetel : UserControl
    {
        public Menu_elmek_Etel_ital_felvetel()
        {
            InitializeComponent();
        }
        private void adat_beiras()
        {
            if (ar=="" || ar==null)
            {
                label2.Hide();
            }
            else
            {
                label2.Text = ar + "Ft";
            }
            if (leiras=="" || leiras==null)
            {
                textBox1.Hide();
            }
            else
            {
                textBox1.Text = leiras;
            }
            label1.Text = nev;
            label3.Text = "#" + id;
        }

        private string nev;
        private string leiras;
        private string ar;
        private bool elerheto;
        private string id;
        private string allergeneID;
        private string afa;

        public string Nev { get => nev; set => nev = value; }
        public string Leiras { get => leiras; set => leiras = value; }
        public string Ar { get => ar; set => ar = value; }
        public bool Elerheto { get => elerheto; set => elerheto = value; }
        public string Id { get => id; set => id = value; }
        public string AllergeneID { get => allergeneID; set => allergeneID = value; }
        public string Afa { get => afa; set => afa = value; }

        private void Menu_elmek_Etel_ital_felvetel_Load(object sender, EventArgs e)
        {
            adat_beiras();
        }

        private void modositBtn_Click(object sender, EventArgs e)
        {
            if (leiras!="" && leiras!=null)
            {
                Etel_Ital_felvetel.adat.Modositas = "food";
                Etel_Ital_felvetel.adat.Nev = nev;
                Etel_Ital_felvetel.adat.Leiras = leiras;
                Etel_Ital_felvetel.adat.Ar = ar;
                Etel_Ital_felvetel.adat.Elerhetoseg = elerheto;
                Etel_Ital_felvetel.adat.Id = id;
                Etel_Ital_felvetel.adat.Allergen = allergeneID;
                Etel_Ital_felvetel.adat.Afa = afa;
            }
            else
            {
                Etel_Ital_felvetel.adat.Modositas = "drink";
                Etel_Ital_felvetel.adat.Nev = nev;
                Etel_Ital_felvetel.adat.Ar = ar;
                Etel_Ital_felvetel.adat.Id = id;
            }

        }
    }
}
