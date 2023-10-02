using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP_GESTION_SALAIRE
{
    public class Enregistrement
    {
        public void Menu()
        {
         

            int choix;
            do
            {
              

                Console.WriteLine("*****************************************************************");
                Console.WriteLine("*****************************************************************");
                Console.WriteLine("**************       FAITE VOTRE CHOIX:  ************************");
                Console.WriteLine("*****               1: AJOUTER UN EMPLOYE                   *****");
                Console.WriteLine("*****               2: SUPPRIMER UN EMPLOYE                 *****");
                Console.WriteLine("*****               3: RECHERCHER UN EMPLOYE                *****");
                Console.WriteLine("*****               4: MODIFIER INFORMATION EMPLOYE         *****");
                Console.WriteLine("*****               5: AFFICHER LA LISTE DES EMPLOYES       *****");
                Console.WriteLine("*****               6: INFORMATIONS SUR L'EMPLOI            *****");
                Console.WriteLine("*****               7: AFFICHER LES INFOS CONCERNANT L'EMPLOI *****");
                Console.WriteLine("*****               8: SUPPRIMER LES INFORMATION CONCERNANT L'EMPLOI     *****");
                Console.WriteLine("*****               9: MODIFIER LES INFORMATION CONCERNANT L'EMPLOI     *****");
                Console.WriteLine("*****               10: RECHERCHER LES INFORMATION D'EMPLOI D'UN EMPLOYE  *****");
                Console.WriteLine("*****               11: CALCULER SALAIRE                     *****");
                Console.WriteLine("*****               12: AFFICHER SALAIRE EMPLOYE             *****");
                Console.WriteLine("*****               13: RECHERCHEZ LE SALAIRE D'UN EMPLOYE   *****");
                Console.WriteLine("*****               14: AFFICHER LISTE DE PAIEMENT           *****");
                Console.WriteLine("*****               15: RECHERCHER  PAIEMENT D'UN EMPLOYE    *****");
                Console.WriteLine("*****               16: SUPPRIMER LES INFORMATION DE PAIEMENT DE L'EMPLOYE  *****");
                Console.WriteLine("*****               17: HISTORIQUE DES PAIEMENTS             *****");
                Console.WriteLine("*****               18: SUPPRIMER HISTORIQUE DES PAIEMENTS   *****");
                Console.WriteLine("*****               19: AFFICHER HISTORQUE                   *****");
                Console.WriteLine("*****               20: QUITTER                              *****");
                Console.WriteLine("*****************************************************************");
                Console.WriteLine("*****************************************************************");
                choix = Convert.ToInt32(Console.ReadLine());




                switch (choix)
                {

                    case 1:

                        AjouterEmploye.ajouterEmploye();
                        break;

                    case 2:
                        RetirerEmploye.retirer_employe();
                        break;

                    case 3:
                        RechercherEmploye.rechercher_employe();
                        break;


                    case 4:
                        ModifierInformationEmploye.ModifierInformations();
                        break;

                    case 5:
                        AfficherListeEmployes.afficher_LesEmployes();
                        break;

                    case 6:

                        AjouterInformationEmpoye.information_employe();
                       break;

                    case 7:
                        AfficherInfoEmploie.afficher_info_emploie();
                        break;

                    case 8:
                        SupprimerInfoEmploi.supprimer_information_employe();
                        break;

                    case 9:
                        ModifierInfoEmploi.ModifierInformationsEmploi();
                        break;

                    case 10:
                        RechercherInfoEmploi.rechercher_information();
                                           break;

                    case 11:
                        Paiement.paiement_employe();
                        break;


                    case 12:
                        SalaireEmploye.afficher_salaire_employe();
                        break;

                  

                    case 13:
                        RechercherSalaire.rechercher_employe();
                        break;

                    case 14:
                       AfficherListePaiement.afficherListe_paiement();
                        break;

                    case 15:
                        RechercherPairement.informationsPaiement();
                        break;

                    case 16:
                        SupprimerInfoPaiement.supprimer_information_paiement();
                        break;

                    case 17:
                       HistoriquePaiement.EnregistrerHistoriquePaiement();
                        break;

                    case 18:
                        SupprimerHistoriquePaiement.SupprimerHistorique();
                        break;

                    case 19:
                        AfficherHistorique.AfficherHistoriquePaiement();
                        break;


                    case 20:
                        QuitterProgramme.Quitter();
                        break;

                    default:

                        break;

                }
            } while (choix != 20);
        }
    }
}
