using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soubor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        TypSoubor ts;
        private void MenuClickOpen(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            ts = new TypSoubor(path);
            label1.Text = ts.getLabel();
            dataGridView1.DataSource = ts.getData()[0];
        }

        private int index = 0;
        private void ClickPrev(object sender, EventArgs e)
        {
            if (this.index == 0)
            {
                MessageBox.Show("Zadna predchazejici tabulka");
            }
            else {
                index--;
                dataGridView1.DataSource = ts.getData()[index];
            }
        }

        private void ClickNext(object sender, EventArgs e)
        {
            if (this.index == ts.getData().Count-1)
            {
                MessageBox.Show("Zadna dalsi tabulka");
            }
            else {
                index++;
                dataGridView1.DataSource = ts.getData()[index];
            }
        }
    }
}
