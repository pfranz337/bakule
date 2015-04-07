using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soubor
{
    class Uzel
    {   //trida uzel pro vytvoreni stromu
        private string jmRodice;        //jmeno predchoziho vybraneho prediktora
        private string jmUzlu;          //jmeno vybraneho prediktora
        private string jmKategorie;     //jednotlive kategorie pro prediktora
        private bool status;            //urcuje zda pro kategorii existuje jeste dalsi rozpad
        private List<Uzel> potomci;     //potomci - jednotlive kategorie jako dalsi uzly

        public Uzel()
        {            
            this.status = true;
            this.jmKategorie = "";
            this.jmRodice = "";
            this.jmUzlu = "";
            this.potomci = new List<Uzel>();
        }

        public void setStatus() 
        {
            this.status = false;
        }

        public void setJmUzlu(string jmPrediktora) {
            this.jmUzlu = jmPrediktora;
        }

        public void setJmKategorie(string jmKat) 
        {
            this.jmKategorie = jmKat;
        }

        public void setJmRodice(string predPrediktor) 
        {
            this.jmRodice = predPrediktor;
        }
        
        public void setPotomci(string jmPotomka, string jmRodice) 
        {
            Uzel u = new Uzel();
            u.setJmKategorie(jmPotomka);
            u.setJmRodice(jmRodice);
            this.potomci.Add(u);
        }

        public bool getStatus() {
            return this.status;
        }
        
        public Uzel getFalsePotomka() {
            //vraci z potomku takovy uzel ktery lze jeste rozlozit
            foreach (Uzel u in this.potomci) 
            {
                if (u.getStatus() == false) 
                {
                    return u;
                }
            }
            return this.potomci[this.potomci.Count-1];
        }

        public string getJmRodice() {
            return this.jmRodice;
        }

        public List<Uzel> getPotomci() {
            return this.potomci;
        }

        public string getJmUzlu() {
            return this.jmUzlu;
        }
        
    }
}
