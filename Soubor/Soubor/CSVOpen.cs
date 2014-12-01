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

        protected override List<DataTable> open()
        {

            StreamReader sr = File.OpenText(getP());

            string s = "";
            List<DataTable> dtList = new List<DataTable>();
            DataTable dt = new DataTable();
            int j = 0, test = 1; ;
            while ((s = sr.ReadLine()) != null)
            {
                string[] split = s.Split(';');


                if (j == 0)
                {
                    foreach (string i in split)
                        dt.Columns.Add(i);
                }
                else
                {
                    if (split.Length != 1)
                        dt.Rows.Add(split);
                    else
                    {
                        dtList.Add(dt);
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
                }
            }
            dtList.Add(dt);
            return dtList;
        }
    }
}
