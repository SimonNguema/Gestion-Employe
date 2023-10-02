using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace APP_GESTION_SALAIRE
{
    public class RetirerEmploye
    {
        // Connexion à la base
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void retirer_employe()
        {
            int arret = 0;

            do
            {
                Console.WriteLine("VOULEZ-VOUS RETIRER UN EMPLOYE ? ");
                Console.WriteLine("TAPER 1 POUR CONTINUER, 0 POUR QUITTER : ");

                // Contrôle de saisie
                try
                {
                    arret = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("SAISIE INCORRECTE ! VEUILLEZ SAISIR 0 OU 1 !");
                }
            } while (arret != 0 && arret != 1);

            while (arret == 1)
            {
                Console.Write("ENTRER L'ID DE L'EMPLOYE QUE VOUS SOUHAITEZ SUPPRIMER : ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("ENTRER UN ID D'EMPLOYE VALIDE !");
                    Console.Write("ENTRER L'ID DE L'EMPLOYE QUE VOUS SOUHAITEZ SUPPRIMER : ");
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Supprimer les informations d'emploi de l'employé
                    string supprimerInfoRequete = "delete from information_emploi where idEmploye = @idEmploye";
                    using (MySqlCommand supprimerInfoCommande = new MySqlCommand(supprimerInfoRequete, connection))
                    {
                        supprimerInfoCommande.Parameters.AddWithValue("@idEmploye", id);
                        supprimerInfoCommande.ExecuteNonQuery();
                    }

                    // Supprimer l'employé
                    string supprimerEmployeRequete = "DELETE FROM employe WHERE idEmploye = @idEmploye";
                    using (MySqlCommand supprimerEmployeCommande = new MySqlCommand(supprimerEmployeRequete, connection))
                    {
                        supprimerEmployeCommande.Parameters.AddWithValue("@idEmploye", id);
                        int lignesAffectees = supprimerEmployeCommande.ExecuteNonQuery();

                        if (lignesAffectees > 0)
                        {
                            Console.WriteLine("L'EMPLOYE AVEC L'ID " + id + " A ETE RETIRE DE LA BASE DE DONNEES AVEC SUCCES.");
                        }
                        else
                        {
                            Console.WriteLine("L'EMPLOYE AVEC L'ID " + id + " N'A PAS ETE TROUVE DANS LA BASE DE DONNEES.");
                        }
                    }
                }

                Console.WriteLine("RETIRER UN NOUVEL EMPLOYE : 1 | QUITTER : 0");
                arret = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
