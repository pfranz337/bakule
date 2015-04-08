using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    class Uzel
    {   //trida uzel pro vytvoreni stromu
        private string jmUzlu;          //jmeno vybraneho prediktora
        private string jmKategorie;     //jednotlive kategorie pro prediktora
        private bool status;            //urcuje zda pro kategorii existuje jeste dalsi rozpad

        public Uzel()
        {            
            this.status = true;
            this.jmKategorie = "";
            this.jmUzlu = "";
        }

        public void setStatus() 
        {
            this.status = false;
        }

        public void setJmUzlu(string jmPrediktora) {
            this.jmUzlu = jmPrediktora;
        }

        public void setJmKategorie(string jmKat) 
        {
            this.jmKategorie = jmKat;
        }

        public bool getStatus() {
            return this.status;
        }

        public string getJmKategorie()
        {
            return this.jmKategorie;
        }

        public string getJmUzlu() {
            return this.jmUzlu;
        }
        
    }
}
