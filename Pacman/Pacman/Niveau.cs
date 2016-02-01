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

namespace Pacman
{
    class Niveau
    {
        private int niveau;
        private SpriteFont sf;
        private static Niveau lvl;

        private Niveau(ContentManager cm)
        {
            niveau = 1;
            sf = cm.Load<SpriteFont>("SpriteFont1");
        }

        public static Niveau instanceNiveau(ContentManager cm)
        {
            if (lvl == null)
            {
                lvl = new Niveau(cm);
                return lvl;
            }
            else
                return lvl;
        }

        public bool addNiveau(Carte c)
        {
            bool sortie = true;
            ObjetAnime[,] oa = c.getListeObjets();

            for(int i = 0; i < c.getNbLig(); i++)
            {
                for (int j = 0; j < c.getNbCol(); j++)
                {
                    if (oa[i, j] != null)
                    {
                        if (oa[i, j].nom == "haricot")
                            sortie = false;
                    }
                }
            }

            if (sortie)
                niveau++;

            return sortie;
        }

        public void draw(SpriteBatch sb)
        {
            string text1 = "Niveau : " + niveau;
            sb.Begin();
            sb.DrawString(this.sf, text1, new Vector2(600, 60), Color.Red);
            sb.End();
        }

        public void reset()
        {
            niveau = 1;
        }
    }
}
