using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    class Entropy : AData
    {

        
        public Entropy(int i) : base(i) {}



        override public Dictionary<string, double> vypocet() {
            Dictionary<string, float> entropieT = new Dictionary<string,float>();
            this.zisk = new Dictionary<string, double>();
            double vysledek = 0;

            for (int i = 0; i < e_kat.Length; i++) {
                for (int j = 0; j < e_kat[i].Length; j++) {
                    foreach (float k in e_kat[i][j].getKat().Values) {
                        //ziskani entropie pro jednotlive tridy prediktora
                        double mezi = (k / kat[i].getKat()[e_kat[i][j].getTrida()]) * 
                            Math.Log((k / kat[i].getKat()[e_kat[i][j].getTrida()]), 2);

                        vysledek += mezi;                        
                    }
                    entropieT.Add(e_kat[i][j].getTrida(), (float)((-1)*vysledek));
                    vysledek = 0;
                }
                
                double vysledekE = 0;
                foreach (string s in kat[i].getKat().Keys) {
                    //celkova entropie prediktora
                    int delenec = kat[i].getKat()[s]; 
                    float nasob = entropieT[s];
                    float mezi2 = (delenec * nasob) / this.suma;

                    vysledekE += mezi2;
                }
                zisk.Add(kat[i].getJmeno(), Math.Round(vysledekE, 4));
                entropieT = new Dictionary<string, float>();
            }


            return zisk;
        }

        override public Dictionary<string, double> vypocet(string cil)
        {

            return null;
        }
    }
}
