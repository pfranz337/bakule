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
    class CSVOpen : Open
    {

        public CSVOpen(string p)
            : base(p)
        {
            data = open();
        }

        protected override char getParser()
        {   //nastaveni parseru
            return ';';
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
