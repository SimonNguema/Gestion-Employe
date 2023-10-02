using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class RechercherInfoEmploi
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void rechercher_information()
        {
            int arret = 0;

            do
            {
                Console.WriteLine("VOULEZ VOUS RECHERCHER LES INFORMATIONS D'EMPLOI D'UN EMPLOYE ?");
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
                Console.Write("ENTRER L'ID DE L'EMPLOYE DONT VOUS SOUAITEZ RECHERCHER LES INFORMATIONS : ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("ENTRER UN ID D'EMPLOYE VALIDE !");
                    Console.Write("ENTRER L'ID DE L'EMPLOYE DONT VOUS SOUAITEZ RECHERCHER LES INFORMATIONS : ");
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string requete = "SELECT * FROM information_emploi WHERE idEmploye = @idEmploye";
                    using (MySqlCommand command = new MySqlCommand(requete, connection))
                    {
                        command.Parameters.AddWithValue("@idEmploye", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("LISTE DES INFORMATIONS EMPLOI DE L'EMPLOYÉ :");
                            Console.WriteLine("-------------------------------------------------------------------------");
                            Console.WriteLine("| ID |   DATE EMBANCHE  |     POSTE      |     STATUT      |  CONTRAT   |");
                            Console.WriteLine("-------------------------------------------------------------------------");

                            if (reader.Read())
                            {
                                int employeId = reader.GetInt32("idEmploye");
                                DateTime dateEmbauche = reader.GetDateTime("DateEmbaucheEmploye");
                                string poste = reader.GetString("PosteEmploye");
                                string statut = reader.GetString("StatutEmploye");
                                string contrat = reader.GetString("ContratEmploye");

                                Console.WriteLine($"| {employeId,2} | {dateEmbauche.ToShortDateString(),-16} | {poste,-14} | {statut,-14} | {contrat,-54} |");
                               
                            }
                            else
                            {
                                Console.WriteLine("AUCUNE INFORMATION EMPLOI TROUVÉE POUR L'EMPLOYÉ AVEC CET ID.");
                            }

                            Console.WriteLine("--------------------------------------------------------------------------");
                        }
                    }
                }

                Console.WriteLine("RECHERCHER LES INFORMATIONS D'EMPLOI D'UN NOUVEL EMPLOYÉ : 1  QUITTER : 0");
                arret = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
