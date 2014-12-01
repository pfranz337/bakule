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

        public Kategorie() {
            this.kat = new Dictionary<string,int>();
            this.jmeno = "";
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

        public string getJmeno() {
            return this.jmeno;
        }
    }
}
