using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;


namespace Soubor
{
    abstract class Open
    {

        private string p;
        protected Kategorie[] k;
        protected List<DataTable> data;


        public Open(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        protected string getP()
        {
            return this.p;
        }

        public List<DataTable> getDataList()
        {
            return this.data;
        }

        public string getJmSoubor()
        {
            string[] parser = this.p.Split('\\');
            return "Oteviram soubor: " + parser[parser.Length - 1]; ;
        }

        public DataTable[] getKategoryTables() {
            /*
             * vytvari tabulky s cetnostmi kategorii - taha je ze slovniku
             */
            DataTable[] cetnostiKat = new DataTable[this.k.Length];

            for (int i = 0; i < cetnostiKat.Length; i++) {
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

        public Kategorie[] getKategory()
        {
            //pouze getter pole Kategorii - prvotne pro pouziti v entropii
            return this.k;
        }
        
        //metoda pro otevirani souboru se vstupnimi daty
        protected abstract List<DataTable> open();
    }
}