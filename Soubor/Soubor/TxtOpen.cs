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
    class TxtOpen : Open
    {

        private DataTable data;
        public TxtOpen(string p)
            : base(p)
        {
            data = open();
        }

        public DataTable getData()
        {
            return this.data;
        }

        protected override DataTable open()
        {

            StreamReader sr = File.OpenText(getP());
            
            string s = "";
            DataTable dt = new DataTable();
            int j = 0;
            while ((s = sr.ReadLine()) != null)
            {
                string[] split = s.Split('\t');


                if (j == 0)
                {
                    foreach (string i in split)
                        dt.Columns.Add(i);
                }
                else
                {
                    dt.Rows.Add(split);
                }

                j++;
            }            

            return dt;
        }

    }
}
