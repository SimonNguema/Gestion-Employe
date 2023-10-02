using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace APP_GESTION_SALAIRE
{
    public class AfficherHistorique
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void AfficherHistoriquePaiement()
        {
            Console.WriteLine("HISTORIQUE DE LA LISTE DE PAIEMENTS");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Id Historique| ID PAIEMENT | ID EMPLOYE | HEURES | SALAIRE/HR | SALAIRE FIXE | ASSIDUITE | PRIME | SALAIRE TOTAL | DATE PAIEMENT |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            string selectQuery = "SELECT * FROM HistoriqueListePaiement";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {   int idHistorique= reader.GetInt32("IdHistoriquePaiement");
                            int idPaiement = reader.GetInt32("IdPaiement");
                            int idEmploye = reader.GetInt32("IdEmploye");
                            int nbreHeureTravail = reader.GetInt32("NbreHeureTravail");
                            decimal salaireHeureEmploye = reader.GetDecimal("SalaireHeureEmploye");
                            decimal salaireFixe = reader.GetDecimal("SalaireFixe");
                            string assiduite = reader.GetString("Assiduite");
                            decimal primeEmploye = reader.GetDecimal("PrimeEmploye");
                            decimal salaireTotal = reader.GetDecimal("SalaireTotal");
                            DateTime datePaiement = reader.GetDateTime("DatePaiement");

                            Console.WriteLine(" | {0,-10} | {0,-10} | {1,-10} | {2,-6} | {3,-11} | {4,-12} | {5,-9} | {6,-5} | {7,-13} | {8,-14} |",
                               idHistorique, idPaiement, idEmploye, nbreHeureTravail, salaireHeureEmploye, salaireFixe, assiduite, primeEmploye, salaireTotal, datePaiement.ToString("yyyy-MM-dd"));
                        }
                    }
                }
            }

            Console.WriteLine("----------------------------------------------");
        }
    }
}
