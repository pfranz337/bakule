using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Soubor
{
    class TxtOpen : Open
    {
        
        public TxtOpen(string p)
            : base(p)
        {
            data = open();
        }        

        protected override List<DataTable> open()
        {

            StreamReader sr = File.OpenText(getP());

            string s = "";
            List<DataTable> dtList = new List<DataTable>();
            DataTable dt = new DataTable();
            int j = 0, test = 1, ind = 0;
            while ((s = sr.ReadLine()) != null)
            {
                string[] split = s.Split('\t');


                if (j == 0)
                {
                    k = new Kategorie[split.Length];
                    foreach (string i in split)
                    {
                        dt.Columns.Add(i);
                        k[ind] = new Kategorie();
                        k[ind].setJmeno(i);
                        ind++;
                    }
                    j++;
                }
                else
                {
                    dt.Rows.Add(split);
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (k[i].getKat().ContainsKey(split[i]))
                        {
                            k[i].getKat()[split[i]]++;
                        }
                        else
                        {
                            k[i].pridejKat(split[i]);
                        }
                    }
                }
                // verze pro ukladani tabulek do listu pro posouvani se v krocich alg
                /*else
                {
                    if (split.Length != 1)
                        dt.Rows.Add(split);
                    else
                    {
                        this.data.Add(dt);
                        test = 0;
                        dt = new DataTable();
                    }
                }
                if (test == 1)
                    j++;
                else
                {
                    j = 0;
                    test = 1;
                }*/
            }
            dtList.Add(dt);
            return dtList;
        }

        /*  metoda pro jednodusi parsovani tabulek po prazdnych radcich
         * private bool isEmpty(string[] s) {
            foreach (string i in s) {
                if (i != String.Empty) return false;
            }
            
            return true;
        }*/
    }
}
