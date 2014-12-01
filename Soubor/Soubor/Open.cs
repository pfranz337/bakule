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
    abstract class Open
    {

        private string p;
        protected List<DataTable> data;


        public Open(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        protected string getP()
        {
            return this.p;
        }

        public List<DataTable> getDataList()
        {
            return this.data;
        }

        public string getJmSoubor()
        {
            string[] parser = this.p.Split('\\');
            return "Oteviram soubor: " + parser[parser.Length - 1]; ;
        }        

        protected abstract List<DataTable> open();
    }
}