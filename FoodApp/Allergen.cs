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
    public partial class Allergen : UserControl
    {
        private string nev;
        private string id;

        public string Nev { get => nev; set => nev = value; }
        public string Id { get => id; set => id = value; }

        public Allergen()
        {
            InitializeComponent();
        }
        private void Allergen_Load(object sender, EventArgs e)
        {
            adat_beiras();
        }
        private void modositBtn_Click(object sender, EventArgs e)
        {
            Etel_Ital_felvetel.adat.Modositas = "allergene";
            Etel_Ital_felvetel.adat.Nev = nev;
            Etel_Ital_felvetel.adat.Id = id;
        }

        private void adat_beiras()
        {
            label1.Text = nev;
            label3.Text = "#" + id;
        }
    }
}
