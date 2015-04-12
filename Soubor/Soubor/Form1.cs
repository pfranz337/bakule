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
        private Uzel prvni;


        private void MenuClickOpen(object sender, EventArgs e)
        {   //otevreni souboru a pocatecni init
            //openFileDialog1.ShowDialog();
            //string path = openFileDialog1.FileName;
            string path = "data.csv";
            ts = new TypSoubor(path);
            label1.Text = ts.getLabel();
            dt = ts.getData()[ts.getData().Count-1];
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
            {   //mazani zaskrtnutych prediktoru s comboboxu
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
                    //pridana podminka za ANDem - pridaji se jen ty itemy ktere nejsou vypnute a ktere jsou zaskrtnute          
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
             * 
             */

            string cil = cilovaSkupina.SelectedItem.ToString();              

            for (int i = 0; i < dataGridView1.Columns.Count; i++) {                                
                if (prediktori[i].Checked)
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Pink;
                else
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.White;
                if (nechteneAtributy[i].Checked)
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.Gray;
                if (dataGridView1.Columns[i].Name.Equals(cil))
                    dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.LightGreen;
            }

            this.selectIndex = cilovaSkupina.SelectedItem.ToString();
            ts.setCountTridaKategory(dt, this.selectIndex);
            kategoryTable();

            //vytvoreni prvniho uzlu
            this.prvni = new Uzel();
            this.prvni.setJmUzlu("HLAVNI TABULKA");
            
        }

        DataGridView[] dtv;
        int krok = 1;
        Button entropy, ifz;
        private void kategoryTable() 
        {
            /**
             * vytvari tabulky kategorii - taha je ze slovniku kategorii (trida Kategorie)
             * */
            if (krok == 1)
                dtv = new DataGridView[ts.getKats().Length];
            int posunX = 13, posunY = 500;
            for (int i = 0; i < ts.getKats().Length; i++)
            {
                if (!nechteneAtributy[i].Checked && prediktori[i].Checked)
                {
                    if (krok == 1)
                    {
                        dtv[i] = new DataGridView();
                        dtv[i].Location = new System.Drawing.Point(posunX, posunY);
                        dtv[i].Size = new System.Drawing.Size(210, 140);

                        Controls.Add(dtv[i]);
                    }
                    dtv[i].DataSource = ts.getKats()[i];
                    dtv[i].AutoResizeColumns();
                    posunX += 215;
                    if (posunX + 200 > this.Width)
                    {
                        posunX = 13;
                        posunY += 150;
                    }
                }
                else
                    if (!prediktori[i].Checked)
                        Controls.Remove(dtv[i]);
            }
            if (krok == 1) 
            {
                ifz = new Button();
                ifz.Text = "IFZ";
                ifz.Location = new Point(posunX, posunY);
                ifz.Click += new System.EventHandler(this.clickIFZ);
                Controls.Add(ifz);

                entropy = new Button();
                entropy.Text = "Entropy";
                entropy.Location = new Point(posunX + ifz.Width+5, posunY);
                entropy.Click += new System.EventHandler(this.clickEntropy);
                Controls.Add(entropy);
            }

        }

        private bool clickEnt = true;
        private void clickEntropy(object sender, EventArgs e)
        {
            /*
             * spusteni algoritmu podminene entropie
             */

            clickIfz = false;   //pro rozliseni vyberu v getMax()
            try
            {
                ts.initEntropy(this.selectIndex);
                Dictionary<string, double> zisk = ts.spustEntropy();
                zobrazVypocty(zisk);
                string vybranyPrediktor = getMax(zisk).Key.ToString();
                Rozpad r = new Rozpad(ts.getKategory(), vybranyPrediktor, dt, this.selectIndex);
                r.showForm();
                r.obarveni();
                vytvorStrom(r); 
                ts.getData().Add(r.getTable());
                dt = ts.getData()[ts.getData().Count - 1];
                ts.setEntropy(dt.Rows.Count - 1);
                ts.setKategory(dt);
                ts.getEntropy().setKat(ts.getKategory());
                ts.setCountTridaKategory(dt, this.selectIndex);     //nastaveni novych spoju tabulek (prediktor vs cilova skupina)
                ClickNext(sender, e);
                kategoryTable();
                ifz.Enabled = false;
                dataGridView1.Columns[vybranyPrediktor].DefaultCellStyle.BackColor = Color.Yellow;



            }
            catch (NullReferenceException) 
            { 
                MessageBox.Show("Žádná další tabulka ke zpracování."); 
                ifz.Enabled = false; 
            }
        }

        ListBox lb = new ListBox();
        private void zobrazVypocty(Dictionary<string, double> zisk) 
        {
            /*
             * metoda pro vypis prubeznych vypoctu (vysledky) pro jednotlive kategorie
             * 
             */

            int x=450, y=650;            
            lb.Size = new System.Drawing.Size(200, 150);
            lb.Location = new Point(x, y);
            Controls.Add(lb);
            int i = 0;
            lb.Items.Add("Krok " + krok + ":");
            foreach (KeyValuePair<string, double> kvp in zisk)
            {
                if (!nechteneAtributy[i].Checked && prediktori[i].Checked)
                {
                    string s = kvp.Key + ": " + kvp.Value.ToString();
                    lb.Items.Add(s);
                }
                i++;
            }
            krok += 1;            
        }

        bool clickIfz = true;
        private void clickIFZ(object sender, EventArgs e)
        {
            /*
             * spusteni algoritmu IFZ
             */
            clickEnt = false;       //pro nastaveni vyberu v gatMax()
            ts.initIFZ();
            Dictionary<string, double> zisk = ts.spustIFZ(this.selectIndex);
            zobrazVypocty(zisk);
            string vybranyPrediktor = getMax(zisk).Key.ToString();
            Rozpad r = new Rozpad(ts.getKategory(), vybranyPrediktor, dt, this.selectIndex);
            r.showForm();
            r.obarveni();
            vytvorStrom(r);            
            try
            {
                ts.getData().Add(r.getTable());
                dt = ts.getData()[ts.getData().Count - 1];
                ts.setKategory(dt);
                ClickNext(sender, e);
                kategoryTable();
                entropy.Enabled = false;
                dataGridView1.Columns[vybranyPrediktor].DefaultCellStyle.BackColor = Color.Yellow;

            }
            catch (NullReferenceException) 
            { 
                MessageBox.Show("Žádná další tabulka ke zpracování."); 
                ifz.Enabled = false; 
            }
        }

        List<List<Uzel>> vetve = new List<List<Uzel>>();    //zde ulozeny vsechny vetve stromu       
        int x, y;
        private void vytvorStrom(Rozpad r) {      
            /*
             *  zobrazuje form se stromem rozpadu
             */

            Form strom = new Form();
            strom.ClientSize = new System.Drawing.Size(700, 500);
            strom.Paint += new System.Windows.Forms.PaintEventHandler(this.vykresliStrom);
            x = 10; y = 50;
            vetve.Add(r.getVetve());
            strom.Show();
        }

        
        private void vykresliStrom(object sender, PaintEventArgs e) {
            /*
             *  vykresluje strim rozpadu - vzdy cely od zacatku postupne do soucasneho kroku
             */

            e.Graphics.DrawString(this.prvni.getJmUzlu(), new Font(FontFamily.Families[0], 12), Brushes.Black, x, y);

            int xx = x, yy = y + 50;
            foreach (List<Uzel> u in vetve) {
                int pocetUzlu = u.Count;
                for (int i = 0; i < pocetUzlu; i++) {
                    e.Graphics.DrawLine(new Pen(Brushes.Black), x+10, y+20, xx, yy);
                    e.Graphics.DrawString(u[i].getJmUzlu() + " - " + u[i].getJmKategorie(), 
                        new Font(FontFamily.Families[0], 12), Brushes.Black, xx, yy);
                    if (i < pocetUzlu -1)
                        xx += 110;
                    if (u[i].getStatus() == false) {
                        x = xx;
                        y = yy;
                    }
                }
                yy += 50;
            }                          
        }
        

        private KeyValuePair<string, double> getMax(Dictionary<string, double> zisk) 
        {/*
          * vraci dvojici jmeno a hodnotu pro nejvyhodnejsi kategorii
          */

            //vyber porovnani podle vybraneho algoritmu
            double minH = 0;
            if (clickIfz)
                minH = -Double.MaxValue;
            if (clickEnt)
                minH = Double.MaxValue;
            int i = 0, index = 0;
            bool test = false;
            KeyValuePair<string, double> pom = new KeyValuePair<string, double>();
            KeyValuePair<string, double> max = new KeyValuePair<string, double>();
            foreach (KeyValuePair<string, double> kvp in zisk) {
                //double ziskH = Math.Abs(kvp.Value);
                double ziskH = kvp.Value;
                if (!kvp.Key.Equals(cilovaSkupina.SelectedItem.ToString()) && prediktori[i].Checked)
                {

                    //vyber porovnani podle vybraneho algoritmu
                    if (clickIfz){ 
                        if (ziskH > minH)
                        {
                            minH = ziskH;
                            max = kvp;
                            index = i;
                            test = true;
                        }
                    }
                    if (clickEnt){ 
                        if (ziskH < minH)
                        {
                            minH = ziskH;
                            max = kvp;
                            index = i;
                            test = true;
                        }
                    }
                }
                pom = kvp;
                i++;
            }
            if (test == false)
                max = pom;
            prediktori[index].Checked = false;
            return max;
        }

        int helpKlik = 0;
        private void helpMenuButton(object sender, EventArgs e)
        {
            if (helpKlik == 0)
            {
                helpKlik++;

                int x = 5, y = 5;
                Form f = new Form();
                Label l = new Label();
                l.Location = new Point(x, y);
                l.Size = new System.Drawing.Size(130, 13);
                l.Text = "Pro hlavní okno:";
                f.Controls.Add(l);

                y += 20;

                Label seda = new Label();
                seda.Location = new Point(x, y);
                seda.Size = new System.Drawing.Size(130, 13);
                seda.BackColor = Color.Gray;
                seda.Text = "Nepouzivany atribut";

                y += 20;

                Label ruzova = new Label();
                ruzova.Location = new Point(x, y);
                ruzova.Size = new System.Drawing.Size(130, 13);
                ruzova.BackColor = Color.Pink;
                ruzova.Text = "Nepouzity prediktor";
                f.Controls.Add(ruzova);

                y += 20;

                Label zluta = new Label();
                zluta.Location = new Point(x, y);
                zluta.Size = new System.Drawing.Size(130, 13);
                zluta.BackColor = Color.Yellow;
                zluta.Text = "Pouzity prediktor";

                y += 20;

                Label zelena = new Label();
                zelena.Location = new Point(x, y);
                zelena.Size = new System.Drawing.Size(130, 13);
                zelena.BackColor = Color.LightGreen;
                zelena.Text = "Predikovany atribut";
                f.Controls.Add(zelena);

                y += 40;

                l = new Label();
                l.Location = new Point(x, y);
                l.Size = new System.Drawing.Size(130, 13);
                l.Text = "Pro okno rozpadu:";

                y += 20;

                ruzova = new Label();
                ruzova.Location = new Point(x, y);
                ruzova.Size = new System.Drawing.Size(130, 13);
                ruzova.BackColor = Color.Pink;
                ruzova.Text = "Vybranný prediktor";

                y += 20;

                zelena = new Label();
                zelena.Location = new Point(x, y);
                zelena.Size = new System.Drawing.Size(130, 13);
                zelena.BackColor = Color.Green;
                zelena.Text = "Jednoznačně určeno";

                y += 20;

                Label cervena = new Label();
                cervena.Location = new Point(x, y);
                cervena.Size = new System.Drawing.Size(130, 13);
                cervena.BackColor = Color.Red;
                cervena.Text = "Nejednoznačně určeno";


                f.Controls.Add(l);
                f.Controls.Add(ruzova);
                f.Controls.Add(cervena);
                f.Controls.Add(zluta);
                f.Controls.Add(zelena);
                f.Controls.Add(seda);

                f.Show();

                f.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.zavriHelp);
            }
            else {
                MessageBox.Show("Help zu je otevreny");
            }
        }

        private void zavriHelp(object sender, FormClosedEventArgs e)
        {
            helpKlik = 0;
        }
    }
}
