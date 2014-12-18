using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    abstract class AData
    {
        protected Kategorie[] kat;
        protected int suma;
        protected Dictionary<string, double> zisk;

        public AData(int i) {
            this.suma = i;  // i = pocet radku v tabulce => celkovy pocet hodnot (SUMA)
        }

        public void setKat(Kategorie[] k)
        {
            this.kat = k;
        }


        abstract public Dictionary<string, double> vypocet(string cil); 
    }
}
