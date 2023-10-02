using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class ControleSaisie
    {
        public static bool controleSaisie(string saisi)
        {
            foreach (char c in saisi)
            {
                if (char.IsDigit(c) || char.IsWhiteSpace(c))
                {
                    return false; // Chiffre ou espace trouvé
                }
            }

            return true; // Aucun chiffre ou espace trouvé
        }



    }
}
