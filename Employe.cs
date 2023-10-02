using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public abstract class Employe
    {
        private int IdEmploye { get; set; }
        private string nomEmploye { get; set; }
        private string prenommEmploye { get; set; }
        private DateTime dateNaissanceEmploye{ get; set; }
        private string mailEmploye { get; set; }
        private string numTelephoneEmploye { get; set; }
       


        //CONSTRUCTEUR
        public Employe(int id, string nom, string prenom, DateTime dateN, string mail, string telephone)
        {
            this.IdEmploye = id;
            this.nomEmploye = nom;
            this.prenommEmploye = prenom;
            this.dateNaissanceEmploye = dateN;
            this.mailEmploye = mail;
            this.numTelephoneEmploye = telephone;
          
        }

    }
}
