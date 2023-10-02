using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace APP_GESTION_SALAIRE
{
    public class ModifierInformationEmploye
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void ModifierInformations()
        {
            Console.WriteLine("ENTREZ L'ID DE L'EMPLOYÉ QUE VOUS SOUHAITEZ MODIFIER :");
            int employeId = Convert.ToInt32(Console.ReadLine());

            // Vérifier si l'employé existe dans la base de données
            if (EmployeExiste(employeId))
            {
                Console.WriteLine("");
                Console.WriteLine("L'EMPLOYÉ EXISTE DANS LA BASE DE DONNÉES. ENTREZ LES NOUVELLES INFORMATIONS :");
                Console.WriteLine("");

                string nom = SaisirNom();
                string prenom = SaisirPrenom();
                DateTime dateNaissance = SaisirDateNaissance();
                string mail = SaisirMail();
                string numTelephone = SaisirNumeroTelephone();

                // Mettre à jour les informations de l'employé dans la base de données
                MettreAJourEmploye(employeId, nom, prenom, dateNaissance, mail, numTelephone);
                Console.WriteLine("");
                Console.WriteLine("LES INFORMATIONS DE L'EMPLOYÉ ONT ÉTÉ MISES À JOUR AVEC SUCCÈS.");
            }
            else
            {
                Console.WriteLine("L'EMPLOYÉ AVEC L'ID SPÉCIFIÉ N'EXISTE PAS DANS LA BASE DE DONNÉES.");
            }
        }

        private static bool EmployeExiste(int employeId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "select count(*) from employe where idEmploye = @employeId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@employeId", employeId);
                    int compter = Convert.ToInt32(command.ExecuteScalar());
                    return compter > 0;
                }
            }
        }

        private static string SaisirNom()
        {
            string nom;
            do
            {
                Console.Write("ENTREZ LE NOUVEAU NOM DE L'EMPLOYE : ");
                nom = Console.ReadLine();
                Console.WriteLine("");

                if (string.IsNullOrEmpty(nom))
                {
                    Console.WriteLine("VEUILLEZ ENTRER UN NOM VALIDE.");
                }

            } while (string.IsNullOrEmpty(nom));

            return nom;
        }

        private static string SaisirPrenom()
        {
            string prenom;
            do
            {
                Console.Write("ENTREZ LE NOUVEAU PRENOM DE L'EMPLOYE: ");
                prenom = Console.ReadLine();
                Console.WriteLine("");

                if (string.IsNullOrEmpty(prenom))
                {
                    Console.WriteLine("VEUILLEZ ENTRER UN PRENOM VALIDE.");
                }

            } while (string.IsNullOrEmpty(prenom));

            return prenom;
        }

        private static DateTime SaisirDateNaissance()
        {
            DateTime dateNaissance;
            do
            {
                Console.Write("ENTREZ LA NOUVELLE DATE DE NAISSANCE DE L'EMPLOYE (FORMAT JJ/MM/AAAA) : ");
                string input = Console.ReadLine();
                Console.WriteLine("");

                if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateNaissance))
                {
                    if (DateTime.Now.Year - dateNaissance.Year < 18)
                    {
                        Console.WriteLine("L'EMPLOYE DOIT AVOIR PLUS DE 18 ANS.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("VEUILLEZ ENTRER UNE DATE VALIDE (FORMAT JJ/MM/AAAA).");
                    Console.WriteLine("");
                }
            } while (true);

            return dateNaissance;
        }

        private static string SaisirMail()
        {
            string mail;
            do
            {
                Console.Write("ENTREZ LE NOUVEL E-MAIL DE L'EMPLOYÉ : ");
                mail = Console.ReadLine();
                Console.WriteLine("");

                if (string.IsNullOrEmpty(mail))
                {
                    Console.WriteLine("VEUILLEZ ENTRER UNE ADRESSE E-MAIL VALIDE.");
                }

            } while (string.IsNullOrEmpty(mail));

            return mail;
        }

        private static string SaisirNumeroTelephone()
        {
            string numTelephone;
            do
            {
                Console.Write("ENTREZ LE NOUVEAU NUMÉRO DE TÉLÉPHONE DE L'EMPLOYÉ : ");
                numTelephone = Console.ReadLine();
                Console.WriteLine("");

                if (string.IsNullOrEmpty(numTelephone))
                {
                    Console.WriteLine("VEUILLEZ ENTRER UN NUMÉRO DE TÉLÉPHONE VALIDE.");
                }

            } while (string.IsNullOrEmpty(numTelephone));

            return numTelephone;
        }

        private static void MettreAJourEmploye(int employeId, string nom, string prenom, DateTime dateNaissance, string mail, string numTelephone)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "update employe set nomEmploye = @nom, prenomEmploye = @prenom, dateNaissanceEmploye = @dateNaissance, " +
                    "mailEmploye = @mail, numTelephoneEmploye = @numTelephone where idEmploye = @employeId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@prenom", prenom);
                    command.Parameters.AddWithValue("@dateNaissance", dateNaissance);
                    command.Parameters.AddWithValue("@mail", mail);
                    command.Parameters.AddWithValue("@numTelephone", numTelephone);
                    command.Parameters.AddWithValue("@employeId", employeId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("LES INFORMATIONS DE L'EMPLOYÉ ONT ÉTÉ MISES À JOUR DANS LA BASE DE DONNÉES.");
                    }
                    else
                    {
                        Console.WriteLine("ERREUR LORS DE LA MISE À JOUR DES INFORMATIONS DE L'EMPLOYÉ DANS LA BASE DE DONNÉES.");
                    }
                }
            }
        }
    }
}
