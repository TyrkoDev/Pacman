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

        public ControleurDeplacement(Joueur j) 
        {
            pacman = j;
            direction = "droite";
        }

        public void seDeplacer()
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                direction = "droite";
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                direction = "gauche";
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                direction = "haut";
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                direction = "bas";
            }
        }

        public void update()
        {
            if (direction == "droite")
            {
                pacman.x += pacman.v;
            }

            if (direction == "gauche")
            {
                pacman.x -= pacman.v;
            }

            if (direction == "haut")
            {
                pacman.y -= pacman.v;
            }

            if (direction == "bas")
            {
                pacman.y += pacman.v;
            }
        }

    }
}
