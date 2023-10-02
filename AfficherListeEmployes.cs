using MySql.Data.MySqlClient;
using System;

namespace APP_GESTION_SALAIRE
{
    public class AfficherListeEmployes
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void afficher_LesEmployes()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "SELECT * FROM employe";

                using (MySqlCommand command = new MySqlCommand(requete, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("LISTE DES EMPLOYÉS:");
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("| ID   |       NOM      |     PRÉNOM     |  DATE DE NAISSANCE   |     ADRESSE MAIL      |   TELEPHONE  |");
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            int employeId = reader.GetInt32("idEmploye");
                            string nom = reader.GetString("nomEmploye");
                            string prenom = reader.GetString("prenomEmploye");
                            DateTime dateNaissance = reader.GetDateTime("dateNaissanceEmploye");
                            string mail = reader.GetString("mailEmploye");
                            string numTel = reader.GetString("numTelephoneEmploye");

                            Console.WriteLine($"| {employeId,4} | {nom,-14} | {prenom,-14} | {dateNaissance.ToShortDateString(),-20} | {mail,-20} | {numTel,-13}|");
                        }

                        Console.WriteLine("---------------------------------------------------------------------------------------------------------");
                    }
                }
            }
        }
    }
}
