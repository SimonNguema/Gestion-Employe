using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class Enums
    {
        public enum Statut
        {
            Actif, Mis_a_pied, Retraite, Conge, Demission
        }

        public enum Contrat
        {
            Stage, CDD, CDI, Prestation
        }

        public enum Poste
        {
            Caissier, Secretaire, Manageur, Gestionnaire, Developpeur, Technicien, Aucun
        }
        public enum Assiduite
        {
           assidu, pas_assidu
        }

        public enum Taxe
    {
        [EnumMember(Value = "1%")]
        UnPourcent = 1,

        [EnumMember(Value = "2%")]
        DeuxPourcent = 2,

        [EnumMember(Value = "3%")]
        TroisPourcent = 3
    }

}
}
