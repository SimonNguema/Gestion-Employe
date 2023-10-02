using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  static APP_GESTION_SALAIRE.ControleSaisie;
using Org.BouncyCastle.Asn1.X509;

namespace APP_GESTION_SALAIRE
{
   public class AjouterEmploye
    {

        //Connexion a la base

        private static string connectionString = "Server=localhost;Database=app_gestion_paie;Uid=simon;Pwd=Simonglain7;";

        public static void ajouterEmploye()
        {
            int arret = 0;
            int id = 0;
            string nom = String.Empty;
            string prenom = String.Empty;
            string mail = String.Empty;
            string numTelephone = String.Empty;
            
            DateTime dateN = DateTime.MinValue;


            do
            {
                Console.WriteLine("Voulez vous enregistrer un employe : ");
                Console.WriteLine("Taper 1 pour enregistrer, 0 pour quitter : ");

                //CONTROLE DE SAISIE
                // toDO : Verifier que les saisies ne comportent pas de caractere speciaux sauf @(seulement accepte dans l'email) et -
                try
                {
                    arret = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Saisie incorrecte! Veuillez saisir (0) ou (1)!");
                }
            } while (arret != 0 && arret != 1);




            while (arret == 1)
            {

                // ENTRER LE NOM DE L'EMPLOYE
                do
                {
                    Console.Write("ENTRER LE NOM DE L'EMPLOYE  : ");
                    nom = Console.ReadLine();

                    if (!controleSaisie(nom))
                    {
                        Console.WriteLine("VEUILLEZ ENTRER UN NOM VALIDE SVP");
                    }

                } while (!controleSaisie(nom) || String.IsNullOrEmpty(nom));


                // ENTRER LE PRENOM DE L'EMPLOYE
                do
                {
                    Console.Write("ENTRER LE PRENOM DE L'EMPLOYE: ");

                    prenom = Console.ReadLine();

                    if (!controleSaisie(prenom))
                    {
                        Console.WriteLine("VEUILLEZ ENTRER UN PRENOM VALIDE SVP !");

                    }

                } while (!controleSaisie(prenom) || String.IsNullOrEmpty(prenom));

                // ENTRER LA DATE DE NAISSANCE DE L'EMPLOYE



                do
                {
                    Console.Write("ENTRER LA DATE DE NAISSANCE DE L'EMPLOYE (FORMAT JJ/MM/AAAA) : ");
                    Console.WriteLine("L'EMPLOYE DOIT AVOIR PLUS DE 18 ANS ");
                    try
                    {
                        DateTime DateEntrer = DateTime.Parse(Console.ReadLine());
                        if (DateTime.Now.Year - DateEntrer.Year >= 18)
                        {
                            dateN = DateEntrer;
                        }
                        else
                        {
                            Console.WriteLine("L'EMPLOYE DOIT AVOIR PLUS DE 18 ANS");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("ENTRER UNE DATE VALIDE !");
                    }
                }
                while (dateN == DateTime.MinValue);






                //ENTRER L'ADRESSE MAIL DE L'EMPLOYE
                do
                {
                    Console.Write("ENTRER L'ADRESSE MAIL DE L'EMPLOYE : ");
                    try
                    {
                        mail = Console.ReadLine();

                    }
                    catch
                    {
                        Console.WriteLine("ENTRER UNE ADRESSE VALIDE !");
                    }
                }
                while (String.IsNullOrEmpty(mail));


                //SAISIE Numero Telephone
                do
                {

                    Console.WriteLine("Numero de telephone de l'employe : ");
                    numTelephone = Console.ReadLine();
                }
                while (String.IsNullOrEmpty(numTelephone));



                InsererEmbloyeBase(nom, prenom, dateN, mail, numTelephone);
                Console.WriteLine("\nEMPLOYE AJOUTER AVEC SUCCES!.");

                Console.WriteLine("Enregistrer un nouveau employe :1  QUITTER : 0");
                arret = Convert.ToInt32(Console.ReadLine());

            }

        }


        public  static void InsererEmbloyeBase(string nom, string prenom, DateTime dateN, string mail, string numTelephone)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "insert into employe( nomEmploye, prenomEmploye, dateNaissanceEmploye, mailEmploye, numTelephoneEmploye)" +
                    "values ( @nomEmploye, @prenomEmploye, @dateNaissanceEmploye, @mailEmploye, @numTelephoneEmploye)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@nomEmploye", nom);
                    command.Parameters.AddWithValue("@prenomEmploye", prenom);
                    command.Parameters.AddWithValue("@dateNaissanceEmploye", dateN);
                    command.Parameters.AddWithValue("@mailEmploye", mail);
                    command.Parameters.AddWithValue("@numTelephoneEmploye", numTelephone);


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
    }
}
