using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2
using Microsoft.Xna.Framework.Content;  //  for Vector2
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Pacman
{
    class Collision
    {
        private Score score;
        private Stopwatch chrono;

        public Collision(ContentManager cm)
        {
            this.score = Score.instanceScore(cm);
            chrono = new Stopwatch();
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
                            score.addScore("haricot");
                        }

                        else if ((liste[j, i].nom == "pouvoir") && (pacman.x == (i * 20)) && (pacman.y == (j * 20)))
                        {
                            liste[j, i] = null;
                            pacman.pouvoir(fantomes);
                            score.addScore("pouvoir");
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

        public String directionFantome(Carte carte, Fantome fantome, String dir)
        {
            ObjetAnime[,] liste = carte.getListeObjets();
            String[] direction = new String[3];
            String newDir = "";
            int i = 0;

            Random rnd = new Random();

            int caseY = fantome.caseY();
            int caseX = fantome.caseX();

            if ((caseY != -1) && (caseX != -1))
            {
                if (dir == "droite")
                {
                    if (liste[caseY, caseX + 1] != null)
                    {
                        if (liste[caseY, caseX + 1].nom != "mur")
                        {
                            newDir = "droite";
                        }
                    }
                    else
                    {
                        if (liste[caseY, caseX - 1] != null)
                        {
                            if (liste[caseY, caseX - 1].nom != "mur")
                            {
                                direction[i] = "gauche";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "gauche";
                            i++;
                        }

                        if (liste[caseY + 1, caseX] != null)
                        {
                            if (liste[caseY + 1, caseX].nom != "mur")
                            {
                                direction[i] = "bas";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "bas";
                            i++;
                        }

                        if (liste[caseY - 1, caseX] != null)
                        {
                            if (liste[caseY - 1, caseX].nom != "mur")
                            {
                                direction[i] = "haut";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "haut";
                            i++;
                        }

                        String[] sortie = new String[i];

                        for (int j = 0; j < sortie.Length; j++)
                        {
                            sortie[j] = direction[j];
                        }

                        int random = rnd.Next(0, sortie.Length);
                        newDir = sortie[random];

                    }
                }

                if (dir == "gauche")
                {
                    if (liste[caseY, caseX - 1] != null)
                    {
                        if (liste[caseY, caseX - 1].nom != "mur")
                        {
                            newDir = "gauche";
                        }
                    }
                    else
                    {
                        if (liste[caseY, caseX + 1] != null)
                        {
                            if (liste[caseY, caseX + 1].nom != "mur")
                            {
                                direction[i] = "droite";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "droite";
                            i++;
                        }

                        if (liste[caseY + 1, caseX] != null)
                        {
                            if (liste[caseY + 1, caseX].nom != "mur")
                            {
                                direction[i] = "bas";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "bas";
                            i++;
                        }

                        if (liste[caseY - 1, caseX] != null)
                        {
                            if (liste[caseY - 1, caseX].nom != "mur")
                            {
                                direction[i] = "haut";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "haut";
                            i++;
                        }

                        String[] sortie = new String[i];

                        for (int j = 0; j < sortie.Length; j++)
                        {
                            sortie[j] = direction[j];
                        }

                        int random = rnd.Next(0, sortie.Length);
                        newDir = sortie[random];

                    }
                }
                
                if (dir == "haut")
                {
                    if (liste[caseY - 1, caseX] != null)
                    {
                        if (liste[caseY - 1, caseX].nom != "mur")
                        {
                            newDir = "haut";
                        }
                    }
                    else
                    {
                        if (liste[caseY, caseX + 1] != null)
                        {
                            if (liste[caseY, caseX + 1].nom != "mur")
                            {
                                direction[i] = "droite";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "droite";
                            i++;
                        }

                        if (liste[caseY + 1, caseX] != null)
                        {
                            if (liste[caseY + 1, caseX].nom != "mur")
                            {
                                direction[i] = "bas";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "bas";
                            i++;
                        }

                        if (liste[caseY, caseX - 1] != null)
                        {
                            if (liste[caseY, caseX - 1].nom != "mur")
                            {
                                direction[i] = "gauche";
                                i++;
                            }
                        }
                        else
                        {
                            direction[i] = "gauche";
                            i++;
                        }

                        String[] sortie = new String[i];

                        for (int j = 0; j < sortie.Length; j++)
                        {
                            sortie[j] = direction[j];
                        }

                        int random = rnd.Next(0, sortie.Length);
                        newDir = sortie[random];

                    }
                }

                if (dir == "bas")
                {
                    if (liste[caseY + 1, caseX] != null)
                    {
                        if (liste[caseY + 1, caseX].nom != "mur")
                        {
                            newDir = "bas";
                            i++;
                        }
                        else
                        {
                            if (liste[caseY, caseX + 1] != null)
                            {
                                if (liste[caseY, caseX + 1].nom != "mur")
                                {
                                    direction[i] = "droite";
                                    i++;
                                }
                            }
                            else
                            {
                                direction[i] = "droite";
                                i++;
                            }

                            if (liste[caseY - 1, caseX] != null)
                            {
                                if (liste[caseY - 1, caseX].nom != "mur")
                                {
                                    direction[i] = "haut";
                                    i++;
                                }
                            }
                            else
                            {
                                direction[i] = "haut";
                                i++;
                            }

                            if (liste[caseY, caseX - 1] != null)
                            {
                                if (liste[caseY, caseX - 1].nom != "mur")
                                {
                                    direction[i] = "gauche";
                                    i++;
                                }
                            }
                            else
                            {
                                direction[i] = "gauche";
                                i++;
                            }

                            String[] sortie = new String[i];

                            for (int j = 0; j < sortie.Length; j++)
                            {
                                sortie[j] = direction[j];
                            }

                            int random = rnd.Next(0, sortie.Length);
                            newDir = sortie[random];

                        }
                    }
                }
            }

            return newDir;
        }
    }
}
