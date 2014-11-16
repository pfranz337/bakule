using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubory
{
    abstract class Open
    {

        private string p;



        public Open(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        protected string getP()
        {
            return this.p;
        }

        public abstract string open();
    }
}
