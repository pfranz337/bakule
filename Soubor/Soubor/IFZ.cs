using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Soubor
{
    class IFZ : AData
    {

        public IFZ(int i) : base(i) { }

        public override Dictionary<string, double> vypocet() {
            return null;
        }

        public override Dictionary<string, double> vypocet(string cil) 
        {
            /*
             *metoda pro vypocet Informacniho zisku           
             */
            double vysledek = 0;
            this.zisk = new Dictionary<string, double>();
            for (int i = 0; i < kat.Length; i++) {
                foreach (float j in kat[i].getKat().Values) 
                {                    
                    double mezi = (j / suma) * Math.Log((j / suma), 2);
                    
                    vysledek = vysledek + mezi;
                    
                    
                }
                vysledek *= (-1);

                Console.Write("Vysledek: " + vysledek + "\n");
                vysledek = Math.Round(vysledek, 4);
                this.zisk.Add(kat[i].getJmeno(), vysledek);
                vysledek = 0;
            }

            for (int i = 0; i < kat.Length; i++) {

                if (!kat[i].getJmeno().Equals(cil))
                {
                    zisk[kat[i].getJmeno()] = (zisk[cil] - zisk[kat[i].getJmeno()]);
                }
            }
            
            return this.zisk;
        }
    }
}
