using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    class Entropy
    {
        private Kategorie[] kat;
        private int suma;
        private Dictionary<string, double> zisk;

        public Entropy(int i)   // i = pocet radku v tabulce => celkovy pocet hodnot (SUMA)
        {
            this.suma = i;
        }

        public void setKat(Kategorie[] k) {
            this.kat = k;
        }

        public Dictionary<string, double> vypocet(string cil) 
        {
            /*
             *metoda pro vypocet Informacniho zisku           
             */

            double vysledek = 0;
            this.zisk = new Dictionary<string, double>();
            for (int i = 0; i < kat.Length; i++) {
                
                foreach (float j in kat[i].getKat().Values) 
                {                    
                    vysledek = vysledek + (j / suma) * Math.Log((j / suma), 2);                    
                }
                this.zisk.Add(kat[i].getJmeno(), (-1)*vysledek);
                vysledek = 0;
            }

            for (int i = 0; i < kat.Length; i++) {
                zisk[kat[i].getJmeno()] = zisk[cil] - zisk[kat[i].getJmeno()];
            }         
            
            return this.zisk;
        }
    }
}
