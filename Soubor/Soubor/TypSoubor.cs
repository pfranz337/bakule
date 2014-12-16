using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Soubor
{
    class TypSoubor
    {
        private string cesta = "", text = "";
        private List<DataTable> data;
        private Kategorie[] k;
        private Entropy e;  //trida entropie s vypoctama
        public TypSoubor(string cesta) 
        {
            this.cesta = cesta;
            openType();
        }

        private string getPrip()
        {
            string[] parser = this.cesta.Split('.');
            return parser[parser.Length - 1]; ;
        }

        public string getLabel() 
        {
            return this.text;
        }

        public List<DataTable> getData()
        {
            return this.data;
        }

        public DataTable[] getKats()
        {
            return getKategoryTables();
        }

        public Dictionary<string, double> spustEntropy(string cil)
        {
            // metoda pro spusteni vypoctu nepodminene entropie
            return this.e.vypocet(cil);
        }

        public DataTable[] getKategoryTables()
        {
            /*
             * vytvari tabulky s cetnostmi kategorii - taha je ze slovniku
             */
            DataTable[] cetnostiKat = new DataTable[this.k.Length];

            for (int i = 0; i < cetnostiKat.Length; i++)
            {
                cetnostiKat[i] = new DataTable();
                cetnostiKat[i].Columns.Add(k[i].getJmeno());
                cetnostiKat[i].Columns.Add("Pocet");

                foreach (KeyValuePair<string, int> d in k[i].getKat())
                {
                    string[] zapis = new string[2];
                    zapis[0] = d.Key.ToString();
                    zapis[1] = d.Value.ToString();

                    cetnostiKat[i].Rows.Add(zapis);
                }
            }
            return cetnostiKat;
        }

        public void openType(){
            /*
             *  metoda pro vybrani spravneho typu souboru
             */

            switch (getPrip())
            {
                case "txt": 
                    TxtOpen txt = new TxtOpen(this.cesta);                   
                    this.data = txt.getDataList();
                    this.text = txt.getJmSoubor();
                    break;

                case "csv":
                    CSVOpen csv = new CSVOpen(this.cesta);                    
                    this.data = csv.getDataList();
                    this.text = csv.getJmSoubor();
                    break;
            }
            e = new Entropy(data[0].Rows.Count);
            setKategory(this.data[0]);
            e.setKat(getKategory());
        }

        private void setKategory(DataTable dt) {
            /*
             *  pocita kategorie z tabulky
             */
            k = new Kategorie[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                k[i] = new Kategorie();
                k[i].setJmeno(dt.Columns[i].ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string s = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    if (k[j].getKat().ContainsKey(s))
                    {
                        k[j].getKat()[s]++;
                    }
                    else
                    {
                        k[j].pridejKat(s);
                    }
                }
            }                    
        }

        public Kategorie[] getKategory()
        {   //vraci pole kategorii
            return this.k;
        }
        
    }
}
