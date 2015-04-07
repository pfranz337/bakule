using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    class Kategorie
    {

        private Dictionary<string, int> kat;
        private string jmeno;
        private string trida;   //podkategorie prediktora - vysoky, nizka, muz, zena, ano, ne, ...

        public Kategorie() {
            this.kat = new Dictionary<string,int>();
            this.jmeno = "";
            this.trida = "";
        }

        public void pridejKat(string s){
            this.kat.Add(s, 1);
        }

        public Dictionary<string, int> getKat()
        {
            return this.kat;
        }

        public void setJmeno(string s) {
            this.jmeno = s;
        }

        public void setTrida(string s)
        {
            this.trida = s;
        }

        public string getJmeno() {
            return this.jmeno;
        }

        public string getTrida()
        {
            return this.trida;
        }
    }
}
