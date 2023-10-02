using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APP_GESTION_SALAIRE.Enums;

namespace APP_GESTION_SALAIRE
{
   public class GestionPaie 
    {
        private int NbreHeureTravail { get; set; }
        private decimal SalaireHeureEmploye { get; set; }
        private decimal SalaireFixe { get; set; }
        private Assiduite AssiduiteEmploye { get; set; }
        private double PrimeEmploye { get; set; }
        public decimal SalaireTotalEmploye { get; set; }


        public GestionPaie(int nbreHeureTravail, decimal salaireHeureEmploye, Assiduite assiduiteEmploye, decimal primeEmploye, decimal salaireTotalEmploye)
        {
            NbreHeureTravail = nbreHeureTravail;
            SalaireHeureEmploye = salaireHeureEmploye;
            AssiduiteEmploye = assiduiteEmploye;
            PrimeEmploye = PrimeEmploye;
            SalaireTotalEmploye = salaireTotalEmploye;
        }
    }
}
