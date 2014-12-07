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
        private DataTable[] dtKat;
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
            return this.dtKat;
        }

        public Dictionary<string, double> spustEntropy()
        {
            // metoda pro spusteni vypoctu nepodminene entropie
            return this.e.vypocet();
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
                    this.dtKat = txt.getKategoryTables();
                    e = new Entropy(data[0].Rows.Count);
                    e.setKat(txt.getKategory());    //entropii predavam do settru pole Kategorii
                    break;
                case "csv":
                    CSVOpen csv = new CSVOpen(this.cesta);
                    
                    this.data = csv.getDataList();
                    this.text = csv.getJmSoubor();
                    this.dtKat = csv.getKategoryTables();
                    e = new Entropy(data[0].Rows.Count);
                    e.setKat(csv.getKategory());    //entropii predavam do settru pole Kategorii
                    break;
            }

        }
    }
}
