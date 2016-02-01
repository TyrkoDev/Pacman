using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pacman
{
    class Fantomes
    {
        Fantome[] listeFantome = new Fantome[4];
        int emp;

        public Fantomes()
        {
            emp = 0;
        }

        public void addFantome(Fantome f)
        {
            listeFantome[emp] = f;
            emp++;
        }

        public void setAfraid()
        {
            for (int i = 0; i < listeFantome.Length; i++)
            {
                listeFantome[i].setAfraid(true);
            }
        }

        public void update()
        {
            for (int i = 0; i < listeFantome.Length; i++)
            {
                listeFantome[i].update();
            }
        }

        public void draw(SpriteBatch sp)
        {
            for (int i = 0; i < listeFantome.Length; i++)
            {
                listeFantome[i].draw(sp);
            }
        }

        public void removeFantome(int place)
        {
            listeFantome = listeFantome.Where((source, index) => index != place).ToArray();
            emp--;
        }

        public Fantome[] liste
        {
            get
            {
                return listeFantome;
            }
            set
            {
                listeFantome = value;
            }
        }
    }
}
