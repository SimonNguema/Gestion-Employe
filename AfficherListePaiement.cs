using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class AfficherListePaiement
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void afficherListe_paiement()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "select * from paiement";

                using (MySqlCommand command = new MySqlCommand(requete, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        Console.WriteLine("LISTE DES INFORMATIONS CONCERNANT L'EMPLOYÉ :");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine(" | ID |  IdEmploye | Id ListeInfoEmploi|  HeureTravail  | SalaireHeureEmploye|     SalaireFixe    |  Assiduite   |     Prime    |    SALAIRE TOTAL   |");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            int idpaiement = reader.GetInt32("IdPaiement");
                            int employeId = reader.GetInt32("IdEmploye");
                            int emploiId = reader.GetInt32("IdListeInfoEmploi");
                            int heure = reader.GetInt32("NbreHeureTravail");
                            decimal salaire_heure = reader.GetDecimal("SalaireHeureEmploye");
                            decimal salaire_fixe = reader.GetDecimal("SalaireFixe");
                            string assiduite = reader.GetString("Assiduite");
                            decimal laprime = reader.GetDecimal("PrimeEmploye");
                            decimal salaireT = reader.GetDecimal("SalaireTotal");

                            Console.WriteLine($" | {idpaiement,2} | {employeId,10} | {emploiId,17} | {heure,13}  |{salaire_heure,19} | {salaire_fixe,18} | {assiduite,12} | {laprime,12} | {salaireT,18} |");
                        }
                       

                        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------");
                    }
                }
            }
        }
    }
}
