using MySql.Data.MySqlClient;
using System;

namespace APP_GESTION_SALAIRE
{
    public class AfficherInfoEmploie
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void afficher_info_emploie()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "select * from information_emploi";

                using (MySqlCommand command = new MySqlCommand(requete, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("LISTE DES INFORMATIONS CONCERNANT L'EMPLOIE:");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("| ID LISTE INFO EMPLOI  | ID EMPLOYE   |  DATE EMBAUCHE |      POSTE     |      STATUT    |     CONTRAT    |");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            int idListe = reader.GetInt32("IdListeInfoEmploi");
                            int employeId = reader.GetInt32("idEmploye");
                            DateTime dateembauche = reader.GetDateTime("DateEmbaucheEmploye");
                            string poste = reader.GetString("PosteEmploye");
                            string statut = reader.GetString("StatutEmploye");

                            string contrat= reader.GetString("ContratEmploye");

                            Console.WriteLine($"| {idListe,-21} |{employeId,-14} | {dateembauche.ToShortDateString(),-14} | {poste,-14} | {statut,-14} | {contrat,-14} |");
                        }

                        Console.WriteLine("----------------------------------------------------------------------------");
                    }
                }
            }
        }
    }
}
