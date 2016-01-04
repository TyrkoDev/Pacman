using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class ControleurDeplacement
    {
        private KeyboardState keyboard;
        private String direction;
        private Joueur pacman;
        private Carte carte;
        private Collision collision;

        public ControleurDeplacement(Joueur j, Carte c, Collision collision) 
        {
            pacman = j;
            carte = c;
            this.collision = collision;
        }

        public void seDeplacer()
        {
            keyboard = Keyboard.GetState();
            if ((pacman.caseX() != -1) && (pacman.caseY() != -1))
            {
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    if (collision.verifyMur(carte, pacman, "droite"))
                    {
                        //pacman.v = 1;
                        direction = "droite";
                    }
                }

                if (keyboard.IsKeyDown(Keys.Left))
                {
                    if (collision.verifyMur(carte, pacman, "gauche"))
                    {
                        //pacman.v = 1;
                        direction = "gauche";
                    }
                }

                if (keyboard.IsKeyDown(Keys.Up))
                {
                    if (collision.verifyMur(carte, pacman, "haut"))
                    {
                        //pacman.v = 1;
                        direction = "haut";
                    }
                }

                if (keyboard.IsKeyDown(Keys.Down))
                {
                    if (collision.verifyMur(carte, pacman, "bas"))
                    {
                        //pacman.v = 1;
                        direction = "bas";
                    }
                }
            }
        }

        public void update()
        {
            if (direction == "droite")
            {
                if (collision.verifyMur(carte, pacman, "droite"))
                {
                    pacman.x += pacman.v;
                }
            }

            if (direction == "gauche")
            {
                if (collision.verifyMur(carte, pacman, "gauche"))
                {
                    pacman.x -= pacman.v;
                }
            }

            if (direction == "haut")
            {
                if (collision.verifyMur(carte, pacman, "haut"))
                {
                    pacman.y -= pacman.v;
                }
            }

            if (direction == "bas")
            {
                if (collision.verifyMur(carte, pacman, "bas"))
                {
                    pacman.y += pacman.v;
                }
            }
        }

        public String dir
        {
            get { return direction; }
            set { direction = value; }
        }
    }
}