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

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            //TypSoubor typSouboru = new TypSoubor();
            //TxtOpen op = new TxtOpen(path);
            //CSVOpen op = new CSVOpen(path);
            //label1.Text = label1.Text + op.getJmSoubor();
           // dataGridView1.DataSource = op.getData();

            TypSoubor ts = new TypSoubor(path);
            label1.Text = label1.Text + ts.getLabel();
            dataGridView1.DataSource = ts.getData();
        }
    }
}
