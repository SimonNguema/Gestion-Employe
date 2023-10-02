using System;
using System.Linq;
using MySql.Data.MySqlClient;
using static APP_GESTION_SALAIRE.Enums;

namespace APP_GESTION_SALAIRE
{
    public class AjouterInformationEmpoye
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void information_employe()
        {
            int choixPoste = 0;
            int choixContrat = 0;
            int choixStatut = 0;
            int arret = 0;

            do
            {
                Console.WriteLine("VOULEZ VOUS ENREGISTRER LES INFORMATIONS D'EMPLOI D'UN EMPLOYE?: ");
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

                Console.Write("ENTRER L'ID DE L'EMPLOYÉ DONT VOUS SOUHAITEZ COMPLÉTER LES INFORMATIONS : ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ENTRER UN ID VALIDE !");
                Console.Write("ENTRER L'ID DE L'EMPLOYÉ DONT VOUS SOUHAITEZ COMPLÉTER LES INFORMATIONS : ");
            }

            // Vérifier si l'ID de l'employé existe dans la base de données
            if (RechercherEmployeeBase(id))
            {
                Console.Write("ENTRER LA DATE D'EMBAUCHE DE L'EMPLOYÉ (Format : YYYY-MM-DD) : ");
                DateTime dateEmbauche;
                while (!DateTime.TryParse(Console.ReadLine(), out dateEmbauche))
                {
                    Console.WriteLine("ENTRER UNE DATE VALIDE !");
                    Console.Write("ENTRER LA DATE D'EMBAUCHE DE L'EMPLOYÉ (Format : YYYY-MM-DD) : ");
                }

                // Saisie du type de Contrat
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("TYPE DE CONTRAT : ");
                    Console.WriteLine("");

                    Console.WriteLine("ENTREZ UNE VALEUR ENTRE 0 ET 3 : ");
                    Console.WriteLine("");

                    Console.WriteLine("0: {0}", Contrat.Stage);
                    Console.WriteLine("1: {0}", Contrat.CDD);
                    Console.WriteLine("2: {0}", Contrat.CDI);
                    Console.WriteLine("3: {0}", Contrat.Prestation);

                    try
                    {
                        choixContrat = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("VEUILLEZ ENTRER UN CHIFFRE VALIDE !");
                    }
                }
                while (choixContrat < 0 || choixContrat > 3);

                // Associer la valeure numerique  saisi a sa chaine de caractere
                string contratEmploye = ((Contrat)choixContrat).ToString();

                // Choix du poste de l'employé
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("POSTE DE L'EMPLOYE : ");
                   

                    Console.WriteLine("ENTREZ UNE VALEUR ENTRE 0 ET 3 : ");
                    Console.WriteLine("");
                    Console.WriteLine("0: {0}", Poste.Caissier);
                    Console.WriteLine("1: {0}", Poste.Secretaire);
                    Console.WriteLine("2: {0}", Poste.Manageur);
                    Console.WriteLine("3: {0}", Poste.Gestionnaire);
                    Console.WriteLine("4: {0}", Poste.Developpeur);
                    Console.WriteLine("5: {0}", Poste.Technicien);
                    Console.WriteLine("6: {0}", Poste.Aucun);

                    try
                    {
                        choixPoste = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("VEUILLEZ ENTRER UN CHIFFRE VALIDE !");
                    }
                }
                while (choixPoste < 0 || choixPoste > 6);

                // Associer la valeure numerique  saisi a sa chaine de caractere
                string posteEmploye = ((Poste)choixPoste).ToString();

                // Saisie du statut de l'employé
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("STATUT DE L'EMPLOYE: ");
                  

                    Console.WriteLine("ENTREZ UNE VALEUR ENTRE 0 ET 3 : ");
                    Console.WriteLine("");
                    Console.WriteLine("0: {0}", Statut.Actif);
                    Console.WriteLine("1: {0}", Statut.Mis_a_pied);
                    Console.WriteLine("2: {0}", Statut.Retraite);
                    Console.WriteLine("3: {0}", Statut.Conge);

                    try
                    {
                        choixStatut = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("VEUILLEZ ENTRER UN CHIFFRE VALIDE!");
                    }
                }
                while (choixStatut < 0 || choixStatut > 3);

                // Associer la valeure numerique  saisi a sa chaine de caractere
                string statutEmploye = ((Statut)choixStatut).ToString();

                // Ajouter les informations de l'employé dans la base de données
                AjoutInformationsEmploye(id, dateEmbauche, contratEmploye, posteEmploye, statutEmploye);

                Console.WriteLine("INFORMATIONS DE L'EMPLOYÉ AJOUTÉES AVEC SUCCÈS !");
            }
            else
            {
                Console.WriteLine("L'ID DE L'EMPLOYÉ N'EXISTE PAS DANS LA BASE DE DONNÉES.");
            }


                Console.WriteLine("ENREGISTRER LES INFORMATIONS D'EMPLOI D'UN NOUVEL EMPLOYE :1  QUITTER : 0");
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

        private static void AjoutInformationsEmploye(int employeeId, DateTime dateEmbauche, string contrat, string poste, string statut)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string requete = "insert into information_emploi(DateEmbaucheEmploye, ContratEmploye, PosteEmploye, StatutEmploye, IdEmploye) " +
                                 "values (@DateEmbaucheEmploye, @ContratEmploye, @PosteEmploye, @StatutEmploye, @IdEmploye)";
                MySqlCommand command = new MySqlCommand(requete, connection);
                command.Parameters.AddWithValue("@DateEmbaucheEmploye", dateEmbauche);
                command.Parameters.AddWithValue("@ContratEmploye", contrat);
                command.Parameters.AddWithValue("@PosteEmploye", poste);
                command.Parameters.AddWithValue("@StatutEmploye", statut);
                command.Parameters.AddWithValue("@IdEmploye", employeeId);

                command.ExecuteNonQuery();
            }
        }
    }
}
