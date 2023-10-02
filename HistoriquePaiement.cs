using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class HistoriquePaiement
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void EnregistrerHistoriquePaiement()
        {
            int choix;
            do
            {
                Console.WriteLine("VOULEZ-VOUS ENREGISTRER L'HISTORIQUE DES PAIEMENTS ?");
                Console.WriteLine("TAPER 1 POUR CONTINUER, 0 POUR QUITTER : ");

                // Contrôle de saisie
                while (!int.TryParse(Console.ReadLine(), out choix))
                {
                    Console.WriteLine("SAISIE INCORRECTE ! VEUILLEZ SAISIR 0 OU 1 !");
                    Console.WriteLine("TAPER 1 POUR CONTINUER, 0 POUR QUITTER : ");
                }
            } while (choix != 0 && choix != 1);

            if (choix == 1)
            {
                string selectPaiementsQuery = "SELECT * FROM paiement";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand selectCommand = new MySqlCommand(selectPaiementsQuery, connection))
                    {
                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            bool enregistrementEffectue = false;

                            while (reader.Read())
                            {
                                int idPaiement = reader.GetInt32("IdPaiement");

                                if (!VerifierExistencePaiementDansHistorique(idPaiement))
                                {
                                    enregistrementEffectue = true;

                                    int idEmploye = reader.GetInt32("IdEmploye");
                                    int nbreHeureTravail = reader.GetInt32("NbreHeureTravail");
                                    decimal salaireHeureEmploye = reader.GetDecimal("SalaireHeureEmploye");
                                    decimal salaireFixe = reader.GetDecimal("SalaireFixe");
                                    string assiduite = reader.GetString("Assiduite");
                                    decimal primeEmploye = reader.GetDecimal("PrimeEmploye");
                                    decimal salaireTotal = reader.GetDecimal("SalaireTotal");
                                    DateTime datePaiement = DateTime.Now;

                                    EnregistrerPaiementDansHistorique(idPaiement, idEmploye, nbreHeureTravail,
                                        salaireHeureEmploye, salaireFixe, assiduite, primeEmploye, salaireTotal, datePaiement);
                                }
                            }

                            if (enregistrementEffectue)
                            {
                                Console.WriteLine("HISTORIQUE ENREGISTRÉ AVEC SUCCÈS");
                            }
                            else
                            {
                                Console.WriteLine("AUCUN PAIEMENT ENREGISTRÉ DANS L'HISTORIQUE");
                            }
                        }
                    }
                }
            }
        }

        private static bool VerifierExistencePaiementDansHistorique(int idPaiement)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT COUNT(*) FROM HistoriqueListePaiement WHERE IdPaiement = @IdPaiement";

                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdPaiement", idPaiement);

                    int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private static void EnregistrerPaiementDansHistorique(int idPaiement, int idEmploye, int nbreHeureTravail,
            decimal salaireHeureEmploye, decimal salaireFixe, string assiduite, decimal primeEmploye, decimal salaireTotal, DateTime datePaiement)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO HistoriqueListePaiement (IdPaiement, IdEmploye, NbreHeureTravail, " +
                    "SalaireHeureEmploye, SalaireFixe, Assiduite, PrimeEmploye, SalaireTotal, DatePaiement) " +
                    "VALUES (@IdPaiement, @IdEmploye, @NbreHeureTravail, @SalaireHeureEmploye, @SalaireFixe, " +
                    "@Assiduite, @PrimeEmploye, @SalaireTotal, @DatePaiement)";

                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@IdPaiement", idPaiement);
                    insertCommand.Parameters.AddWithValue("@IdEmploye", idEmploye);

                    insertCommand.Parameters.AddWithValue("@NbreHeureTravail", nbreHeureTravail);
                    insertCommand.Parameters.AddWithValue("@SalaireHeureEmploye", salaireHeureEmploye);
                    insertCommand.Parameters.AddWithValue("@SalaireFixe", salaireFixe);
                    insertCommand.Parameters.AddWithValue("@Assiduite", assiduite);
                    insertCommand.Parameters.AddWithValue("@PrimeEmploye", primeEmploye);
                    insertCommand.Parameters.AddWithValue("@SalaireTotal", salaireTotal);
                    insertCommand.Parameters.AddWithValue("@DatePaiement", datePaiement);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
