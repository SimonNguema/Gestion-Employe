using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class SupprimerHistoriquePaiement
    {
        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void SupprimerHistorique()
        {
            int choix;
            do
            {
                Console.WriteLine("VOULEZ-VOUS SUPPRIMER L'HISTORIQUE DES PAIEMENTS ?");
                Console.WriteLine("TAPER 1 POUR CONTINUER, 0 POUR QUITTER : ");

                // Contrôle de saisie
                while (!int.TryParse(Console.ReadLine(), out choix))
                {
                    Console.WriteLine("SAISIE INCORRECTE ! VEUILLEZ SAISIR 0 OU 1 !");
                    Console.WriteLine("TAPER 1 POUR CONTINUER, 0 POUR QUITTER : ");
                }
            } while (choix != 0 && choix != 1);

            if (choix == 1)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM HistoriqueListePaiement";

                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("L'historique des paiements a été supprimé avec succès.");
                    }
                }
            }
        }
    }
}
