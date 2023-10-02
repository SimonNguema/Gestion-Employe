using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class RechercherSalaire
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void rechercher_employe()
        {
            int arret = 0;

            do
            {
                Console.WriteLine("VOULEZ-VOUS RECHERCHER LE SALAIRE D'UN EMPLOYÉ ?");
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

                    string requete = "SELECT employe.IdEmploye, nomEmploye, prenomEmploye, SalaireTotal FROM employe JOIN paiement ON employe.IdEmploye = paiement.IdEmploye WHERE employe.IdEmploye = @IdEmploye;";
                    using (MySqlCommand command = new MySqlCommand(requete, connection))
                    {
                        command.Parameters.AddWithValue("@IdEmploye", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("LISTE DES INFORMATIONS CONCERNANT L'EMPLOYÉ :");
                            Console.WriteLine("-----------------------------------------------------------");
                            Console.WriteLine("| ID |      NOM       |     PRÉNOM     |   SALAIRE TOTAL |");
                            Console.WriteLine("-----------------------------------------------------------");

                            if (reader.Read())
                            {
                                int employeId = reader.GetInt32("IdEmploye");
                                string nom = reader.GetString("nomEmploye");
                                string prenom = reader.GetString("prenomEmploye");
                                decimal salaire = reader.GetDecimal("SalaireTotal");

                                Console.WriteLine($"| {employeId,2} | {nom,-14} | {prenom,-14} | {salaire,-14} |");
                            }
                            else
                            {
                                Console.WriteLine("AUCUN SALAIRE TROUVÉ POUR L'EMPLOYÉ AVEC CET ID.");
                            }

                            Console.WriteLine("-----------------------------------------------------------");
                        }
                    }

                    Console.WriteLine("RECHERCHER UN NOUVEL EMPLOYÉ : 1  QUITTER : 0");
                    arret = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
    }
}
