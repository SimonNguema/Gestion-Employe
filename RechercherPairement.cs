using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class RechercherPairement
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void informationsPaiement()
        {
            int arret = 0;

            do
            {
                Console.WriteLine("VOULEZ-VOUS AFFICHER LES INFORMATION DE PAIEMENT D'UN EMPLOYE?");
                Console.WriteLine("TAPER 1 POUR CONTINUER, 0 POUR QUITTER : ");

                // CONTROLE DE SAISIE
                try
                {
                    arret = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("SAISIE INCORRECTE ! VEUILLEZ SAISIR (0) OU (1) !");
                }
            } while (arret != 0 && arret != 1);

            while (arret == 1)
            {
                Console.Write("ENTREZ L'ID DE L'EMPLOYÉ QUE VOUS RECHERCHEZ : ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("ENTREZ UN ID D'EMPLOYÉ VALIDE !");
                    Console.Write("ENTREZ UN ID D'EMPLOYÉ POUR LEQUEL VOUS RECHERCHEZ LE SALAIRE : ");
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string requete = " SELECT * FROM paiement WHERE IdPaiement= @IdPaiement";
                    using (MySqlCommand command = new MySqlCommand(requete, connection))
                    {
                        command.Parameters.AddWithValue("@IdPaiement", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("LISTE DES INFORMATIONS CONCERNANT L'EMPLOYÉ :");
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| ID |    IdEmploye     |     Id ListeInfoEmploi    | HeureTravail | SalaireHeureEmploye| SalaireFixe | Assiduite |  Prime  |  SALAIRE TOTAL |");
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------");

                            if (reader.Read())
                            {
                                int idpaiement = reader.GetInt32("IdPaiement");
                                int employeId = reader.GetInt32("IdEmploye");
                                int emploiId = reader.GetInt32("IdListeInfoEmploi");
                                decimal salaire_heure = reader.GetDecimal("SalaireHeureEmploye");
                                decimal salaire_fixe = reader.GetDecimal("SalaireFixe");
                                string assiduite = reader.GetString("Assiduite");
                                decimal laprime= reader.GetDecimal("PrimeEmploye");
                                decimal salaireT = reader.GetDecimal("SalaireTotal");

                                Console.WriteLine($" | {idpaiement,7} | {employeId,7} | {emploiId,-14} | {salaire_heure,-14} | {salaire_fixe,-14} | {assiduite,-14} | {laprime,-14} | {salaireT,-14} |");
                            }
                            else
                            {
                                Console.WriteLine($"AUCUN PAIEMENT TROUVÉ POUR L'EMPLOYÉ AVEC  l'ID {id}");
                            }

                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------");
                        }
                    }

                    Console.WriteLine("RECHERCHER UN NOUVEL EMPLOYÉ : 1  QUITTER : 0");
                    arret = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
    }
}
