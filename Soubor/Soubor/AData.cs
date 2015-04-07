using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    abstract class AData
    {/*
      * abstraktni trida pro vypocty IFZ, pod. e., GINI....
      */
        protected Kategorie[] kat;
        protected Kategorie[][] e_kat;
        protected int suma;
        protected Dictionary<string, double> zisk;

        public AData(int i) {
            this.suma = i;  // i = pocet radku v tabulce => celkovy pocet hodnot (SUMA)
        }

        public void setKat(Kategorie[] k)
        {
            this.kat = k;
        }

        public void setE_kat(Kategorie[][] e_kat) {
            this.e_kat = e_kat;
        }   

        abstract public Dictionary<string, double> vypocet(string cil); //pro IFZ

        abstract public Dictionary<string, double> vypocet();   //pro entropii
    }
}
