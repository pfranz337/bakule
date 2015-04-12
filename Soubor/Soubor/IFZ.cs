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
            AllocConsole();
            double vysledek = 0;
            //double vysledek2 = 0;
            this.zisk = new Dictionary<string, double>();
            for (int i = 0; i < kat.Length; i++) {
                Console.Write(kat[i].getJmeno() + "\n");
                foreach (float j in kat[i].getKat().Values) 
                {                    
                    double mezi = (j / suma) * Math.Log((j / suma), 2);
                    //Console.Write(mezi + "\n");
                    
                    vysledek = vysledek + mezi;
                    
                    
                }
                vysledek *= (-1);
                /*foreach (float jj in kat[i].getKat().Values)
                {
                    double mezi2 = (jj / suma) * vysledek;
                    //Console.Write(mezi2 + "\n");

                    vysledek2 = vysledek2 + mezi2;
                }*/

                Console.Write("Vysledek: " + vysledek + "\n");
                vysledek = Math.Round(vysledek, 4);
                this.zisk.Add(kat[i].getJmeno(), vysledek);
                vysledek = 0;
                //vysledek2 = 0;
            }

            for (int i = 0; i < kat.Length; i++) {

                if (!kat[i].getJmeno().Equals(cil))
                {
                    Console.Write("Vysledek2: " + kat[i].getJmeno() + ": " + (zisk[cil] - zisk[kat[i].getJmeno()]) + "\n");
                    //zisk[kat[i].getJmeno()] = Math.Abs(zisk[cil] - zisk[kat[i].getJmeno()]);
                    zisk[kat[i].getJmeno()] = (zisk[cil] - zisk[kat[i].getJmeno()]);
                }
            }
            
            return this.zisk;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
