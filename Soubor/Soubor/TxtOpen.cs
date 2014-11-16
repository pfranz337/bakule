using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubory
{
    class TxtOpen : Open
    {

        private string cesta;
        public TxtOpen(string p)
            : base(p)
        {
            cesta = open();
        }

        public string getCesta()
        {
            return this.cesta;
        }

        public override string open()
        {
            return "Otevrel jsem soubor s nazvem: " + getP();
        }

    }
}
