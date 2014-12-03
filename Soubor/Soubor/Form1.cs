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


        private TypSoubor ts;
        private int index;
        private DataTable dt;
        private void MenuClickOpen(object sender, EventArgs e)
        {
            //openFileDialog1.ShowDialog();
            //string path = openFileDialog1.FileName;
            string path = "data.csv";
            ts = new TypSoubor(path);
            label1.Text = ts.getLabel();
            dt = ts.getData()[0];
            dataGridView1.DataSource = dt;
            this.index = 0;
            naplnBox();
        }

        private void naplnBox() {
            for (int i = 0; i < dt.Columns.Count; i++) {
                cilovaSkupina.Items.Add(dt.Columns[i].ColumnName);
                prediktori.Items.Add(dt.Columns[i].ColumnName); 
            }            
        }

        
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

        int pomIC, pomIP, count = 0; 
        DataColumn pomC = new DataColumn(), pomP = new DataColumn();
        private void button3_Click(object sender, EventArgs e)
        {
            int C = cilovaSkupina.SelectedIndex;
            int P = prediktori.SelectedIndex;

            if (count > 0) {
                count = 0;
                dt.Columns[pomIC].ColumnName = pomC.ColumnName;
                dt.Columns[pomIP].ColumnName = pomP.ColumnName;
            }            

            pomC.ColumnName = dt.Columns[C].ColumnName;
            pomIC = C;
            pomP.ColumnName = dt.Columns[P].ColumnName;
            pomIP = P;

            dt.Columns[C].ColumnName += "   C";
            dt.Columns[P].ColumnName += "   P";

            count++;
        }
    }
}
