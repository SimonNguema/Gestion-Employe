using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APP_GESTION_SALAIRE.Enums;

namespace APP_GESTION_SALAIRE
{
   public class GestionsDesEmployes
    {
        private DateTime DateEmbaucheEmploye { get; set; }
        private Contrat ContratEmploye { get; set; }

        private Poste PosteEmploye { get; set; }
        private Statut StatutEmploye { get; set; }
        


        public GestionsDesEmployes(DateTime laDateEmbauche, Contrat leContrat, Poste lePosteEmploye, Statut leStatutEmploye)
        {
            DateEmbaucheEmploye= laDateEmbauche;
            ContratEmploye= leContrat;
            PosteEmploye= lePosteEmploye;
            StatutEmploye= leStatutEmploye;
        }
        

    }
}
