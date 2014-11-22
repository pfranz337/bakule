using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;


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

        public string getJmSoubor() {
            string []parser = this.p.Split('\\');
            return parser[parser.Length - 1]; ;
        }

        protected abstract DataTable open();
    }
}
