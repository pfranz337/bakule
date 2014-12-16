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
        private CheckBox[] prediktori;          //checkboxy pro nastaveni predikovanych atributu
        private CheckBox[] nechteneAtributy;    //checkboxy pro vypnuti nechtenych atributu
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
            Label check = new Label();
            int x = 180, y = 30, posun = 120;

            //vse posunuto dolu o radek s nechtenymi atributy!!!! -> zvetseni Formu

            pred.Text = "Vyber atributu:";
            check.Text = "Vyber atributy, ktere nechces zahrnout:";
            pred.Location = new Point(x, y + 40);
            check.Location = new Point(x, y - 8);
            check.Size = new System.Drawing.Size(250, pred.Size.Height);
            prediktori = new CheckBox[size];
            nechteneAtributy = new CheckBox[size];
            Controls.Add(pred);
            Controls.Add(check);
            for (int i = 0; i < size; i++) {
                //nastaveni checkboxu s prediktory
                prediktori[i] = new CheckBox();
                prediktori[i].Name = dt.Columns[i].ColumnName;
                prediktori[i].Text = dt.Columns[i].ColumnName;
                prediktori[i].Checked = false;
                prediktori[i].Location = new Point(x, y+60);
                prediktori[i].Click += new System.EventHandler(this.naplnBox);

                //nastaveni checkboxu s nechtenymi atributy
                nechteneAtributy[i] = new CheckBox();
                nechteneAtributy[i].Name = dt.Columns[i].ColumnName;
                nechteneAtributy[i].Text = dt.Columns[i].ColumnName;
                nechteneAtributy[i].Checked = false;
                nechteneAtributy[i].Location = new Point(x, y + 10);
                nechteneAtributy[i].Click += new System.EventHandler(this.vypniPrediktora);
                x += posun;

                Controls.Add(prediktori[i]);
                Controls.Add(nechteneAtributy[i]);
            }            
        }

        private void vypniPrediktora(object sender, EventArgs e)
        {
            /*
             * metoda pro disable checkboxu pro vyber atributu podle zaskrtnutych checkboxu v nechtenych atributech
             */
            for (int i = 0; i < nechteneAtributy.Length; i++) {
                if (nechteneAtributy[i].Checked)
                {
                    prediktori[i].Enabled = false;
                }
                else {
                    prediktori[i].Enabled = true;
                }
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
                if (!prediktori[i].Checked && !nechteneAtributy[i].Checked) 
                    //pridana podminka za ANDem - pridaji se jen ty itemy ktere nejsou vynute a ktere jsou zaskrtnute          
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
                cilovaSkupina.SelectedIndex = cilovaSkupina.Items.Count-1;
            Controls.Add(cilovaSkupina);
            
        }

        private void initComboLine() 
        {
            /*
             * vytvari lajnu s rozhranim pro uzivatele (combobox, tlacitka, popisky)
             */


            Label cil = new Label();
            Button enter = new Button();

            int x = 180, y = 130;
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
            cilovaSkupina.SelectedValueChanged += new System.EventHandler(this.vypniCil);       //pridani udalosti

            x += 121;
            enter.Text = "Enter";
            enter.Location = new Point(x, y);
            enter.Click += new System.EventHandler(this.clickEnter);

            Controls.Add(enter);
            Controls.Add(cil);
        }

        private void vypniCil(object sender, EventArgs e) {
            /*
             * metoda pro disable checkboxu pri vybrani itemu z comboboxu
             */
            for (int i = 0; i < prediktori.Length; i++)
            {
                if (nechteneAtributy[i].Checked || cilovaSkupina.SelectedItem.ToString() == prediktori[i].Text.ToString())
                {
                    prediktori[i].Enabled = false;
                }
                else {
                    prediktori[i].Enabled = true;
                }
            }
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
                    dt.Columns[i].ColumnName += " CIL";     //zmena na " CIL" kvuli podmince Equals('-')
                }
            }

            this.selectIndex = cilovaSkupina.SelectedItem.ToString();
            kategoryTable();
        }

        private void kategoryTable() 
        {
            /**
             * vytvari tabulky kategorii - taha je ze slovniku kategorii (trida Kategorie)
             * */

            DataGridView[] dtv = new DataGridView[ts.getKats().Length];
            int posunX = 13, posunY = 500;
            for (int i = 0; i < ts.getKats().Length; i++)
            {
                if (!nechteneAtributy[i].Checked)
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
            informacniZisk(ts.spustEntropy(this.selectIndex));

        }

        private void informacniZisk(Dictionary<string, double> zisk) 
        {
            /*
             * metoda pro vypis inforomacniho zisku podle nepodminene entropie
             * 
             */

            int x=450, y=680;
            Label l = new Label();
            l.Location = new Point(x, y-16);
            l.Size = new System.Drawing.Size(200, 13);
            l.Text = "Informacni zisk:";
            Controls.Add(l);
            int i = 0;
            foreach (KeyValuePair<string, double> kvp in zisk)
            {
                if (!nechteneAtributy[i].Checked)
                {
                    l = new Label();
                    l.Location = new Point(x, y);
                    l.Size = new System.Drawing.Size(200, 13);
                    l.Text = kvp.Key + ": " + kvp.Value.ToString();
                    y += 16;
                    Controls.Add(l);
                }
                i++;
            }
            
        }
    }
}
