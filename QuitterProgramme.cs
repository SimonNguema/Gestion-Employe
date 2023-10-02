using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
         class QuitterProgramme
    {
        public static void Quitter()
        {


            Console.WriteLine("AU REVOIR  ! APPUYEZ SUR ENTRER");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
