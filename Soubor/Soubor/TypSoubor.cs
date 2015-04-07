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
        private Kategorie[][] k_ent;        //pole pro spojeni a vytvoreni kategorii (prediktor vs cilova skupina)
        private IFZ ifz;  //trida informacniho zisku s vypoctem
        private Entropy entropy;  //trida entropie s vypoctem
        public TypSoubor(string cesta) 
        {   //otevreni souboru
            this.cesta = cesta;
            openType();
        }

        private string getPrip()
        {   //vraci nazev souboru s priponou
            string[] parser = this.cesta.Split('.');
            return parser[parser.Length - 1]; ;
        }

        public string getLabel() 
        {   //nastavuje text do labelu s nazvem souboru
            return this.text;
        }

        public List<DataTable> getData()
        {   //vraci list dat jak sli po krocich za sebou
            return this.data;
        }

        public DataTable[] getKats()
        {   //vraci pole tabulek s cetnostmi kategorii
            return getKategoryTables();
        }

        public Dictionary<string, double> spustIFZ(string cil)
        {
            // metoda pro spusteni vypoctu nepodminene entropie
            return this.ifz.vypocet(cil);
        }

        public Dictionary<string, double> spustEntropy()
        {
            // metoda pro spusteni vypoctu nepodminene entropie
            return this.entropy.vypocet();
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
            
            setKategory(this.data[this.data.Count-1]);  //nastavuje posledni vlozenou tabulku do listu pro pocitani kategorii
            
        }

        public void initIFZ() {
            //inicializace IFZ
            setIFZ(data[this.data.Count - 1].Rows.Count);
            ifz.setKat(getKategory());
        }

        public void initEntropy(string cil)
        {
            //inicializace Entropy
            setEntropy(data[this.data.Count - 1].Rows.Count);
            entropy.setKat(getKategory());
            entropy.setE_kat(getK_ent());
        }

        public void setIFZ(int i) {
            this.ifz = new IFZ(i);
        }

        public void setEntropy(int i) {
            this.entropy = new Entropy(i);
        }

        public void setKategory(DataTable dt) {
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

        public void setCountTridaKategory(DataTable dt, string cil) {
            /*
             *  metoda pro vytvoreni tabulek zavislosti prediktor vs cilova skupina
             */

            k_ent = new Kategorie[dt.Columns.Count][];
            for (int i = 0; i < dt.Columns.Count; i++) {
                int pocetTrid = getKategory()[i].getKat().Count;
                k_ent[i] = new Kategorie[pocetTrid];
                int j = 0;
                foreach (string nameTrida in getKategory()[i].getKat().Keys)
                {
                    //inicializace
                    k_ent[i][j] = new Kategorie();
                    k_ent[i][j].setJmeno(dt.Columns[i].ColumnName);
                    k_ent[i][j].setTrida(nameTrida);    // trida = vysoky, vysoke, muz, zena,....

                    j++;
                }
                
            }

            int k = 0;
            while (k < k_ent.Length)
            {
                for (int i = 0; i < k_ent[k].Length; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (k_ent[k][i].getJmeno() != cil) {
                            string bunka = dt.Rows[j][k_ent[k][i].getJmeno()].ToString();
                            if (bunka == k_ent[k][i].getTrida()) {
                                string cilovaBunka = dt.Rows[j][cil].ToString();
                                if (k_ent[k][i].getKat().ContainsKey(cilovaBunka))
                                {
                                    k_ent[k][i].getKat()[cilovaBunka]++;
                                }
                                else {
                                    k_ent[k][i].pridejKat(cilovaBunka);
                                }
                            }
                        }
                    }
                }
                k++;
            }                
        }

        public Kategorie[][] getK_ent() {
            return this.k_ent;
        }

        public Kategorie[] getKategory()
        {   //vraci pole kategorii
            return this.k;
        }

        public AData getIFZ() {
            return this.ifz;
        }

        public AData getEntropy() {
            return this.entropy;
        }
    }
}
