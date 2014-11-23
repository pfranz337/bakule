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
        private DataTable data;
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

        public DataTable getData()
        {
            return this.data;
        }
        
        public void openType(){


            switch (getPrip())
            {
                case "txt": 
                    TxtOpen txt = new TxtOpen(this.cesta);
                    this.data = txt.getData();
                    this.text = txt.getJmSoubor();
                    break;
                case "csv":
                    CSVOpen csv = new CSVOpen(this.cesta);
                    this.data = csv.getData();
                    this.text = csv.getJmSoubor();
                    break;
            }

        }
    }
}
