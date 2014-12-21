using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    class Entropy : AData
    {
        public Entropy(int i) : base(i) { }

        override public Dictionary<string, double> vypocet(string cil)
        {
            //POUZE PREKOPIROVANA Z IFZ!!!!! JE TREBA NAPSAT NOVY VYPOCET PRO POD.E.

            double vysledek = 0;
            this.zisk = new Dictionary<string, double>();
            for (int i = 0; i < kat.Length; i++)
            {

                foreach (float j in kat[i].getKat().Values)
                {
                    vysledek = vysledek + (j / suma) * Math.Log((j / suma), 2);
                }
                vysledek = Math.Round(vysledek, 4);
                this.zisk.Add(kat[i].getJmeno(), (-1) * vysledek);
                vysledek = 0;
            }

            for (int i = 0; i < kat.Length; i++)
            {
                zisk[kat[i].getJmeno()] = zisk[cil] - zisk[kat[i].getJmeno()];
            }

            return this.zisk;
        }
    }
}
