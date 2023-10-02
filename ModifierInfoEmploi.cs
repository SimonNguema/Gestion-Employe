using System;
using MySql.Data.MySqlClient;
using static APP_GESTION_SALAIRE.Enums;

namespace APP_GESTION_SALAIRE
{
    public class ModifierInfoEmploi
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void ModifierInformationsEmploi()
        {
            Console.WriteLine("ENTREZ L'ID DE L'EMPLOYÉ DONT VOUS SOUHAITEZ MODIFIER LES INFORMATIONS :");
            int employeId = Convert.ToInt32(Console.ReadLine());

            // Vérifier si l'employé existe dans la base de données
            if (EmployeExiste(employeId))
            {
                Console.WriteLine("");
                Console.WriteLine("L'EMPLOYÉ EXISTE DANS LA BASE DE DONNÉES. ENTREZ LES NOUVELLES INFORMATIONS :");
                Console.WriteLine("");

                DateTime dateEmbauche = SaisirDateEmbauche();
                string contrat = SaisirContrat();
                string poste = SaisirPoste();
                string statut = SaisirStatut();

                // Mettre à jour les informations de l'employé dans la base de données
                MettreAJourInformationsEmploye(employeId, dateEmbauche, contrat, poste, statut);
                Console.WriteLine("");
                Console.WriteLine("LES INFORMATIONS DE L'EMPLOYÉ ONT ÉTÉ MISES À JOUR AVEC SUCCÈS.");
            }
            else
            {
                Console.WriteLine("L'EMPLOYÉ AVEC CET ID N'EXISTE PAS DANS LA BASE DE DONNÉES.");
            }
        }

        private static bool EmployeExiste(int employeId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "select count(*) from employe WHERE idEmploye = @employeId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@employeId", employeId);
                    int compter = Convert.ToInt32(command.ExecuteScalar());
                    return compter > 0;
                }
            }
        }

        private static DateTime SaisirDateEmbauche()
        {
            DateTime dateEmbauche;
            do
            {
                Console.Write("ENTREZ LA NOUVELLE DATE D'EMBAUCHE DE L'EMPLOYÉ (Format : YYYY-MM-DD) : ");
                string input = Console.ReadLine();
                Console.WriteLine("");

                if (DateTime.TryParse(input, out dateEmbauche))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("VEUILLEZ ENTRER UNE DATE VALIDE (Format : YYYY-MM-DD).");
                    Console.WriteLine("");
                }
            } while (true);

            return dateEmbauche;
        }

        private static string SaisirContrat()
        {
            string contrat;
            do
            {
                Console.Write("ENTREZ LE NOUVEAU TYPE DE CONTRAT DE L'EMPLOYÉ : ");
                contrat = Console.ReadLine();
                Console.WriteLine("");

                 Console.WriteLine("0: {0}", Contrat.Stage);
                    Console.WriteLine("1: {0}", Contrat.CDD);
                    Console.WriteLine("2: {0}", Contrat.CDI);
                    Console.WriteLine("3: {0}", Contrat.Prestation);

                if (string.IsNullOrEmpty(contrat))
                {
                    Console.WriteLine("VEUILLEZ ENTRER UN TYPE DE CONTRAT VALIDE.");
                }

            } while (string.IsNullOrEmpty(contrat));

            return contrat;
        }

        private static string SaisirPoste()
        {
            string poste;
            do
            {
                Console.Write("ENTREZ LE NOUVEAU POSTE DE L'EMPLOYÉ : ");
                poste = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("ENTREZ UNE VALEUR ENTRE 0 ET 3 : ");
                Console.WriteLine("");
                Console.WriteLine("0: {0}", Poste.Caissier);
                Console.WriteLine("1: {0}", Poste.Secretaire);
                Console.WriteLine("2: {0}", Poste.Manageur);
                Console.WriteLine("3: {0}", Poste.Gestionnaire);
                Console.WriteLine("4: {0}", Poste.Developpeur);
                Console.WriteLine("5: {0}", Poste.Technicien);
                Console.WriteLine("6: {0}", Poste.Aucun);

                if (string.IsNullOrEmpty(poste))
                {
                    Console.WriteLine("VEUILLEZ ENTRER UN POSTE VALIDE.");
                }

            } while (string.IsNullOrEmpty(poste));

            return poste;
        }

        private static string SaisirStatut()
        {
            string statut;
            do
            {
                Console.Write("ENTREZ LE NOUVEAU STATUT DE L'EMPLOYÉ : ");
                statut = Console.ReadLine();
                Console.WriteLine("");

                Console.WriteLine("ENTREZ UNE VALEUR ENTRE 0 ET 3 : ");
                Console.WriteLine("");
                Console.WriteLine("0: {0}", Statut.Actif);
                Console.WriteLine("1: {0}", Statut.Mis_a_pied);
                Console.WriteLine("2: {0}", Statut.Retraite);
                Console.WriteLine("3: {0}", Statut.Conge);

                if (string.IsNullOrEmpty(statut))
                {
                    Console.WriteLine("VEUILLEZ ENTRER UN STATUT VALIDE.");
                }

            } while (string.IsNullOrEmpty(statut));

            return statut;
        }

        private static void MettreAJourInformationsEmploye(int employeId, DateTime dateEmbauche, string contrat, string poste, string statut)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE information_emploi SET DateEmbaucheEmploye = @dateEmbauche, ContratEmploye = @contrat, " +
                               "PosteEmploye = @poste, StatutEmploye = @statut WHERE IdEmploye = @employeId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@employeId", employeId);
                    command.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
                    command.Parameters.AddWithValue("@contrat", contrat);
                    command.Parameters.AddWithValue("@poste", poste);
                    command.Parameters.AddWithValue("@statut", statut);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
