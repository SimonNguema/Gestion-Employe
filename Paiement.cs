using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using static APP_GESTION_SALAIRE.Enums;

namespace APP_GESTION_SALAIRE
{
    public class Paiement
    {
        // Connexion à la base
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void paiement_employe()
        {
            int nbrHeureTravail;
            decimal salaireParHeure;
            decimal salaireFixe;
            decimal primeAssiduite;
            decimal primeAnciennete;
            decimal prime;
            decimal salaireTotal;
            int choixAssiduite = 0;
            int arret = 0;
           



            do
            {
                Console.WriteLine("VOULEZ VOUS CALCULER DE SALAIRE D'UN EMPLOYE?: ");
                Console.WriteLine("TAPER 1  CONTINUER, 0 POUR QUITTER : "); 

                //CONTROLE DE SAISIE
                // toDO : Verifier que les saisies ne comportent pas de caractere speciaux sauf @(seulement accepte dans l'email) et -
                try
                {
                    arret = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("SAISIE INCORECTE! VEUILLEZ SAISIR (0) OU (1)!");
                }
            } while (arret != 0 && arret != 1);




          while (arret == 1)
            {

                Console.Write("ENTREZ l'ID DE L'EMPLOYE DONT VOUS VOULEZ CALCULER LE  SALAIRE : ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Entrez un ID d'employé valide !");
                Console.Write("Entrez 'ID de l'employé que vous recherchez : ");
            }

                // Vérifier si l'ID de l'employé existe dans la base de données
       if (RechercherEmployeeBase(id)) 
       { 
                

                    Console.Write("Entrez le nombre d'heures de travail : ");
            while (!int.TryParse(Console.ReadLine(), out nbrHeureTravail) || nbrHeureTravail < 0)
            {
                Console.WriteLine("Entrez un nombre d'heures de travail valide !");
                Console.Write("Entrez le nombre d'heures de travail : ");
            }

            Console.Write("Entrez le salaire par heure de travail : ");
            while (!decimal.TryParse(Console.ReadLine(), out salaireParHeure) || salaireParHeure < 0)
            {
                Console.WriteLine("Entrez un salaire par heure de travail valide !");
                Console.Write("Entrez le salaire par heure de travail : ");
            }

            salaireFixe = nbrHeureTravail * salaireParHeure;

            Console.WriteLine($"Le salaire fixe de l'employé avec l'ID {id} est : {salaireFixe}");

            // Saisie du type d'Assiduité
            do
            {
                Console.WriteLine("");
                Console.WriteLine("ASSIDUITE DE L'EMPLOYE : ");
                Console.WriteLine("");

                Console.WriteLine("ENTREZ UNE VALEUR ENTRE 0 ET 1 : ");
                Console.WriteLine("");

                Console.WriteLine("0: {0}", Assiduite.assidu);
                Console.WriteLine("1: {0}", Assiduite.pas_assidu);

                try
                {
                    choixAssiduite = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("VEUILLEZ ENTRER UN CHIFFRE VALIDE !");
                }
            }
            while (choixAssiduite < 0 || choixAssiduite > 1);

            // Associer la valeure numerique  saisi a sa chaine de caractere
            string assiduiteEmploye = ((Assiduite)choixAssiduite).ToString();



            if ((Assiduite)choixAssiduite == Assiduite.assidu)
            {
                Console.Write("Entrez le pourcentage de la prime d'assiduité : ");
                while (!decimal.TryParse(Console.ReadLine(), out primeAssiduite) || primeAssiduite < 0)
                {
                    Console.WriteLine("Entrez un pourcentage valide pour la prime d'assiduité !");
                    Console.Write("Entrez le pourcentage de la prime d'assiduité : ");
                }
            }
            else
            {
                Console.WriteLine("Aucune prime d'assiduité.");
                primeAssiduite = 0;
            }

            // Calcul de la prime d'ancienneté
            DateTime dateEmbauche = ObtenirDateEmbauche(id);
            TimeSpan anciennete = DateTime.Now - dateEmbauche;
            int anneesAnciennete = (int)(anciennete.TotalDays / 365);

            if (anneesAnciennete >= 10)
            {
                Console.Write("Entrez le POURCENTAGE de la prime d'ancienneté : ");
                while (!decimal.TryParse(Console.ReadLine(), out primeAnciennete) || primeAnciennete < 0)
                {
                    Console.WriteLine("Entrez un montant valide pour la prime d'ancienneté !");
                    Console.Write("Entrez le POURCENTAGE de la prime d'ancienneté : ");
                }
            }
            else
            {
                Console.WriteLine("Aucune prime d'ancienneté.");
                primeAnciennete = 0;
            }
            // Calcul de la prime totale
             prime= primeAssiduite + primeAnciennete;
            // Calcul du salaire total
           


            // Calcul du salaire total
            salaireTotal = salaireFixe + (salaireFixe * (prime / 100));


            Console.WriteLine($"Le salaire total de l'employé avec l'ID {id} est : {salaireTotal}");


            InsererPaiementBase(id, nbrHeureTravail,  salaireParHeure, salaireFixe, assiduiteEmploye,  prime, salaireTotal);
            Console.WriteLine("\nEMPLOYE AJOUTER AVEC SUCCES!.");

                Console.WriteLine("INFORMATIONS DE L'EMPLOYÉ AJOUTÉES AVEC SUCCÈS !");
       }
            else
            {
                Console.WriteLine("L'ID DE L'EMPLOYÉ N'EXISTE PAS DANS LA BASE DE DONNÉES.");
            }

            Console.WriteLine("VOULEZ VOUS FAIRE UN NOUVEAU CALCULE DE SALAIRE?  :1  QUITTER : 0");
            arret = Convert.ToInt32(Console.ReadLine());

        }

    }


        private static bool RechercherEmployeeBase(int IdEmploye)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "select count(*) from employe where IdEmploye = @IdEmploye";
                MySqlCommand command = new MySqlCommand(requete, connection);
                command.Parameters.AddWithValue("@IdEmploye", IdEmploye);

                int compter = Convert.ToInt32(command.ExecuteScalar());

                return compter > 0;
            }
        }



        private static DateTime ObtenirDateEmbauche(int IdListe)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "select DateEmbaucheEmploye from information_emploi where IdListeInfoEmploi = @IdListeInfoEmploi";
                MySqlCommand command = new MySqlCommand(requete, connection);
                command.Parameters.AddWithValue("@IdListeInfoEmploi", IdListe);

                MySqlDataReader reader = command.ExecuteReader();

                DateTime dateEmbauche = DateTime.MinValue;

                if (reader.Read())
                {
                    dateEmbauche = reader.GetDateTime(0);
                }

                reader.Close();

                return dateEmbauche;
            }
        }




        public static void InsererPaiementBase(int employeeId, int nbrHeureTravail, decimal salaireParHeure, decimal salaireFixe, string choixAssiduite, decimal prime, decimal salaireTotal)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "insert into paiement(IdEmploye,IdListeInfoEmploi, NbreHeureTravail, SalaireHeureEmploye, SalaireFixe,Assiduite,  PrimeEmploye, SalaireTotal)" +
                    "values (@IdEmploye,@IdListeInfoEmploi, @NbreHeureTravail, @SalaireHeureEmploye, @SalaireFixe, @Assiduite, @PrimeEmploye, @SalaireTotal)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdEmploye", employeeId);
                    command.Parameters.AddWithValue("@IdListeInfoEmploi", ObtenirIdListeInfoEmploi(employeeId));
                    command.Parameters.AddWithValue("@NbreHeureTravail", nbrHeureTravail);
                    command.Parameters.AddWithValue("@SalaireHeureEmploye", salaireParHeure);
                    command.Parameters.AddWithValue("@SalaireFixe", salaireFixe);
                    command.Parameters.AddWithValue("@Assiduite", choixAssiduite);
                    command.Parameters.AddWithValue("@PrimeEmploye", prime);
                    command.Parameters.AddWithValue("@SalaireTotal", salaireTotal);


                    int LigneRecu = command.ExecuteNonQuery();

                    if (LigneRecu > 0)
                    {
                        Console.WriteLine("L'employé a été inséré avec succès dans la base de données.");
                    }
                    else
                    {
                        Console.WriteLine("Erreur lors de l'insertion de l'employé dans la base de données.");
                    }
                }
            }
        }

        private static int ObtenirIdListeInfoEmploi(int IdEmploye)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "select IdListeInfoEmploi from information_emploi where IdEmploye = @IdEmploye";
                MySqlCommand command = new MySqlCommand(requete, connection);
                command.Parameters.AddWithValue("@IdEmploye", IdEmploye);

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0; 
                }
            }
        }

    }
}


