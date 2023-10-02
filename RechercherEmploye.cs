using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class RechercherEmploye
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void rechercher_employe()
        {
            int arret = 0;

            do
            {
                Console.WriteLine("VOULEZ VOUS RECHERCHER UN EMPLOYE ?");
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
                Console.Write("Entrez l'ID de l'employé que vous recherchez : ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Entrez un ID d'employé valide !");
                    Console.Write("Entrez l'ID de l'employé que vous recherchez : ");
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string requete = "SELECT * FROM employe WHERE idEmploye = @idEmploye";
                    using (MySqlCommand command = new MySqlCommand(requete, connection))
                    {
                        command.Parameters.AddWithValue("@idEmploye", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("LISTE DES INFORMATIONS CONCERNANT L'EMPLOYÉ :");
                            Console.WriteLine("-----------------------------------------------------------");
                            Console.WriteLine("| ID |      NOM       |     PRÉNOM     | DATE DE NAISSANCE |");
                            Console.WriteLine("-----------------------------------------------------------");

                            if (reader.Read())
                            {
                                int employeId = reader.GetInt32("idEmploye");
                                string nom = reader.GetString("nomEmploye");
                                string prenom = reader.GetString("prenomEmploye");
                                DateTime dateNaissance = reader.GetDateTime("dateNaissanceEmploye");

                                Console.WriteLine($"| {employeId,2} | {nom,-14} | {prenom,-14} | {dateNaissance.ToShortDateString(),-18} |");
                            }
                            else
                            {
                                Console.WriteLine("AUCUN EMPLOYÉ TROUVÉ AVEC CET ID.");
                            }

                            Console.WriteLine("-----------------------------------------------------------");
                        }
                    }
                }

                Console.WriteLine("RECHERCHER UN NOUVEL EMPLOYÉ : 1  QUITTER : 0");
                arret = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
