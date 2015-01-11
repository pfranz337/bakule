﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Soubor
{
    class Rozpad
    {   /*
         * trida pro vyvoreni rozpadu z tabulky podle nejvyhodnejsi kategorie spocitane z alg.
         */
        private DataGridView[] data;
        private DataTable dt;
        private Form f;
        private Kategorie proMaxPre;    //pro maximalniho prediktora
        private string predikovanyAtribut;
        private List<DataTable> listRozpadu;

        public Rozpad(Kategorie[] k, string maxPrediktor, DataTable aktualni, string cil) {
            //inicializace
            for (int i = 0; i < k.Length; i++) {
                if (k[i].getJmeno().Equals(maxPrediktor))
                {
                    this.data = new DataGridView[k[i].getKat().Count];
                    this.proMaxPre = k[i];
                    break;
                }
            }
            this.dt = aktualni;
            this.predikovanyAtribut = cil;
        }

        public DataGridView[] getGridy(){
            return this.data;
        }

        public void showForm()
        {
            //zobrazi form s rozpadem
            f = new Form();
            
            f.ClientSize = new System.Drawing.Size(700, 800);

            rozpad();
            f.Show();
            MessageBox.Show("Vybirame podle: " + this.proMaxPre.getJmeno());
        }

        private void rozpad() {
            //samotny rozpad
            DataTable newDt;
            this.listRozpadu = new List<DataTable>();
            int ind = 0, posunX = 0, posunY = 0;
            foreach (KeyValuePair<string, int> key in proMaxPre.getKat())
            {
                newDt = new DataTable();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    newDt.Columns.Add(dt.Columns[i].ToString());
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = dt.Rows[i][proMaxPre.getJmeno()].ToString();
                    string radek = dt.Rows[i][0].ToString();
                    for (int j = 1; j < dt.Columns.Count; j++)
                    {
                        radek += " " + dt.Rows[i][j].ToString();
                    }
                    string[] split = radek.Split(' ');
                    if (s.Equals(key.Key.ToString()))
                    {
                        newDt.Rows.Add(split);
                    }
                }
                data[ind] = new DataGridView();
                data[ind].Location = new System.Drawing.Point(posunX, posunY);
                data[ind].Size = new System.Drawing.Size(650, 250);
                data[ind].DataSource = newDt;
                this.listRozpadu.Add(newDt);                
                f.Controls.Add(data[ind]);
                ind++;
                posunY += 280;
            }
        }

        public DataTable getTable() {
            /*
             * vraci tabulku ktera neni jednoznacne urcena
             */
            bool test = true;
            int indexCile = 0;
            for (int i = 0; i < this.dt.Columns.Count; i++) {
                if (dt.Columns[i].ToString().Equals(this.predikovanyAtribut)) {
                    indexCile = i;
                    break;
                }
            }

            int j = 0;
            DataTable ret = null;
            foreach (DataTable d in this.listRozpadu) 
            {
                
                string s = d.Rows[0][indexCile].ToString();
                for (int i = 1; i < d.Rows.Count; i++) {
                    if (!d.Rows[i][indexCile].ToString().Equals(s))
                    {                        
                        test = false;
                    }
                    if (!test)
                    {
                        data[j].Columns[predikovanyAtribut].DefaultCellStyle.BackColor = Color.Red;
                        ret = d;
                    }
                }
                j++;
            }
            return ret;
        }

        public void obarveni() {
            for (int i = 0; i < data.Length; i++) {
                data[i].Columns[proMaxPre.getJmeno()].DefaultCellStyle.BackColor = Color.Pink;
                data[i].Columns[predikovanyAtribut].DefaultCellStyle.BackColor = Color.Green;
            }
        }
    }
}