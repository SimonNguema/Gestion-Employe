using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class SalaireEmploye
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void afficher_salaire_employe()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "select employe.IdEmploye, nomEmploye,prenomEmploye, SalaireTotal from employe join paiement on employe.IdEmploye=paiement.IdEmploye;";

                using (MySqlCommand command = new MySqlCommand(requete, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("LISTE DES INFORMATIONS CONCERNANT L'EMPLOIE:");
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine("| ID |      NOM       |     PRENOM     |   SALAIRE TOTAL |");
                        Console.WriteLine("----------------------------------------------------------");

                        while (reader.Read())
                        {
                            int id= reader.GetInt32("IdEmploye");
                            String nom = reader.GetString("nomEmploye");
                            string prenom = reader.GetString("prenomEmploye");
                            string salaire = reader.GetString("SalaireTotal");

                        

                            Console.WriteLine($"| {id,2} | {nom,-14} | {prenom,-14} | {salaire,-14} |");
                        }

                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
            }
        }
    }
}
