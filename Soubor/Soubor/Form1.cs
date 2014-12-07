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

        private TypSoubor ts;   //struktura pro otevirani neznameho souboru (zatim txt nebo csv)
        private int index, pocetKliknuti;
        private DataTable dt;
        private CheckBox[] prediktori;
        private string selectIndex = "";
        private ComboBox cilovaSkupina = new ComboBox();

        private void MenuClickOpen(object sender, EventArgs e)
        {
            //openFileDialog1.ShowDialog();
            //string path = openFileDialog1.FileName;
            string path = "data.csv";
            ts = new TypSoubor(path);
            label1.Text = ts.getLabel();
            dt = ts.getData()[0];
            dataGridView1.DataSource = dt;
            dataGridView1.AutoResizeColumns();            
            this.index = 0;
            check();            
        }
        
        private void check() {
            /*            
             * Vytvari checkboxy s popiskama
             */

            int size = dt.Columns.Count;
            Label pred = new Label();
            int x = 180, y = 30, posun = 120;

            pred.Text = "Vyber atributu:";
            pred.Location = new Point(x, y-8);
            prediktori = new CheckBox[size];
            Controls.Add(pred);

            for (int i = 0; i < size; i++) {
                prediktori[i] = new CheckBox();
                prediktori[i].Name = dt.Columns[i].ColumnName;
                prediktori[i].Text = dt.Columns[i].ColumnName;
                prediktori[i].Checked = false;
                prediktori[i].Location = new Point(x, y+10);
                prediktori[i].Click += new System.EventHandler(this.naplnBox);
                x += posun;

                Controls.Add(prediktori[i]);
            }            
        }        
        
        private void naplnBox(object sender, EventArgs e)
        {
            //naplni combobox nazvama sloupcu tabulky
            if (pocetKliknuti > 0)
            {
                object[] oj = new object[cilovaSkupina.Items.Count];
                int ind = 0;

                foreach(object o in cilovaSkupina.Items){
                    oj[ind] = new object();
                    oj[ind] = o;
                    ind++;
                }

                for (int i = 0; i < oj.Length; i++) {
                    cilovaSkupina.Items.Remove(oj[i]);
                }
            }

            initComboLine();

            for (int i = 0; i < dt.Columns.Count; i++) {
                if (!prediktori[i].Checked)                
                    cilovaSkupina.Items.Add(prediktori[i].Name);                
            }
            pocetKliknuti++;
            if (selectIndex != "") {
                for (int i = 0; i < cilovaSkupina.Items.Count; i++) {
                    if (selectIndex.Equals(cilovaSkupina.Items[i].ToString())){
                        cilovaSkupina.SelectedIndex = i;
                        break;
                    }
                }
            } else
                cilovaSkupina.SelectedIndex = 0;
            Controls.Add(cilovaSkupina);
            
        }

        private void initComboLine() 
        {
            /*
             * vytvari lajnu s rozhranim pro uzivatele (combobox, tlacitka, popisky)
             */


            Label cil = new Label();
            Button enter = new Button();
            Button kategory = new Button();

            int x = 180, y = 80;
            string s = "Vyber predikovany atribut: ";

            cil.Location = new Point(x, y);
            cil.Size = new System.Drawing.Size(130, 13);
            cil.Text = s;
            
            x += 130;
            cilovaSkupina.FormattingEnabled = true;
            cilovaSkupina.Location = new System.Drawing.Point(x, y);
            cilovaSkupina.Name = "cilovaSkupina";
            cilovaSkupina.Size = new System.Drawing.Size(121, 21);
            cilovaSkupina.TabIndex = 6;

            x += 121;
            enter.Text = "Enter";
            enter.Location = new Point(x, y);
            enter.Click += new System.EventHandler(this.clickEnter);

            x += enter.Width;
            kategory.Text = "Kategory";
            kategory.Location = new Point(x, y);
            kategory.Click += new System.EventHandler(this.clickKategory);

            Controls.Add(enter);
            Controls.Add(kategory);
            Controls.Add(cil);
        }

        private void clickKategory(object sender, EventArgs e)
        {
            //odchyceni udalosti kliknuti na tlacitko kategorie
            kategoryTable();
        }        

        private void ClickPrev(object sender, EventArgs e)
        {
            //odchyceni udalosti kliknuti na tlacitko prev
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
            //odchyceni udalosti kliknuti na tlacitko next
            if (this.index == ts.getData().Count-1)
            {
                MessageBox.Show("Zadna dalsi tabulka");
            }
            else {
                index++;
                dataGridView1.DataSource = ts.getData()[index];
            }
        }        
        
        private void clickEnter(object sender, EventArgs e)
        {
            /*vymenuje po kliknuti na tl. nastaveni cilove skupiny a prediktora 
             * (uklada si predchozi nastaveni a po zmene vraci nastavuje na puvodni)*/

            string cil = cilovaSkupina.SelectedItem.ToString();

            for (int i = 0; i < prediktori.Length; i++) {
                if (prediktori[i].Checked)
                {
                    if (dt.Columns[i].ColumnName[dt.Columns[i].ColumnName.Length - 3].Equals('-') == false)
                        dt.Columns[i].ColumnName += " - P";
                }
                else
                    dt.Columns[i].ColumnName = prediktori[i].Name;
                if (dt.Columns[i].ColumnName.Equals(cil))
                {
                    dt.Columns[i].ColumnName += " - C";
                }
            }

            selectIndex = cilovaSkupina.SelectedItem.ToString();
        }

        private void kategoryTable() 
        {
            /**
             * vytvari tabulky kategorii - taha je ze slovniku kategorii (trida Kategorie)
             * */

            DataGridView[] dtv = new DataGridView[ts.getKats().Length];
            int posunX = 13, posunY = 450;
            for (int i = 1; i < ts.getKats().Length; i++)
            {
                dtv[i] = new DataGridView();             
                dtv[i].Location = new System.Drawing.Point(posunX, posunY);
                dtv[i].Size = new System.Drawing.Size(210, 140);                
                dtv[i].DataSource = ts.getKats()[i];
                Controls.Add(dtv[i]); 
                dtv[i].AutoResizeColumns();
                posunX += 215;
                if (posunX + 200 > this.Width) 
                {
                    posunX = 13;
                    posunY += 150;
                }                
            }            
        }
    }
}
