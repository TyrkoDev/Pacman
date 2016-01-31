using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Collision
    {
        public Collision()
        {

        }

        public void update(Carte carte, Joueur pacman, Fantomes fantomes)
        {
            ObjetAnime[,] liste = carte.getListeObjets();
            for (int i = 0; i < carte.getNbCol(); i++)
            {
                for (int j = 0; j < carte.getNbLig(); j++)
                {
                    if (liste[j, i] != null)
                    {                        
                        if ((liste[j, i].nom == "haricot") && (pacman.x == (i * 20)) && (pacman.y == (j * 20)))
                        {
                            liste[j, i] = null;
                        }

                        else if ((liste[j, i].nom == "pouvoir") && (pacman.x == (i * 20)) && (pacman.y == (j * 20)))
                        {
                            liste[j, i] = null;
                            pacman.pouvoir(fantomes);
                            Console.WriteLine("Pouvoir !");
                        }

                        Fantome[] listeF = fantomes.liste;

                        for (int k = 0; k < listeF.Length; k++)
                        {
                            Fantome f = listeF[k];
                            if ((f.x >= (i * 20)) && (f.x <= (i * 21)) && (f.y >= (j * 20)) && (f.y <= (j * 21)) &&
                                (pacman.x >= (i * 20)) && (pacman.x <= (i * 20)) && (pacman.y >= (j * 20)) && (pacman.y <= (j * 20)))
                                pacman.vivant(fantomes, k);
                        }
                    }
                }
            }

            carte.setListeObjets(liste);
        }

        public bool verifyMur(Carte carte, Joueur pacman, String direction)
        {
            ObjetAnime[,] liste = carte.getListeObjets();
            bool sortie = true;
            int caseY = pacman.caseY();
            int caseX = pacman.caseX();
            if ((caseY != -1) && (caseX != -1))
            {
                if (direction == "droite")
                {
                    if (liste[caseY, caseX + 1] != null)
                    {
                        if (liste[caseY, caseX + 1].nom == "mur")
                        {
                            sortie = false;
                        }
                    }
                }

                if (direction == "gauche")
                {
                    if (liste[caseY, caseX - 1] != null)
                    {
                        if (liste[caseY, caseX - 1].nom == "mur")
                        {
                            sortie = false;
                        }
                    }
                }

                if (direction == "haut")
                {
                    if (liste[caseY - 1, caseX] != null)
                    {
                        if (liste[caseY - 1, caseX].nom == "mur")
                        {
                            sortie = false;
                        }
                    }
                }

                if (direction == "bas")
                {
                    if (liste[caseY + 1, caseX] != null)
                    {
                        if (liste[caseY + 1, caseX].nom == "mur")
                        {
                            sortie = false;
                        }
                    }
                }
            }

            return sortie;
        }
    }
}
