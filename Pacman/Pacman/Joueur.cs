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
    class Joueur
    {
        private ContentManager content;
        private ObjetAnime pacman;
        private int xPos;
        private int yPos;
        private int vitesse;
        private ControleurDeplacement cd;

        public Joueur(ContentManager c)
        {
            content = c;
            xPos = 14 * 20;
            yPos = 17 * 20;
            vitesse = 1;
            cd = new ControleurDeplacement(this);
            pacman = new ObjetAnime(content.Load<Texture2D>(@"sprites\pacman"), new Vector2(0f, 0f), new Vector2(20f, 20f));
        }

        public void draw(SpriteBatch sb)
        {
            Vector2 pos = new Vector2(xPos, yPos);

            sb.Begin();
            sb.Draw(pacman.Texture, pos, Color.White);
            sb.End();
        }

        public void update()
        {
            cd.seDeplacer();
            cd.update();
        }

        public int x
        {
            get
            {
                return xPos;
            }
            set
            {
                xPos = value;
            }
        }

        public int y
        {
            get
            {
                return yPos;
            }
            set
            {
                yPos = value;
            }
        }

        public int v
        {
            get
            {
                return vitesse;
            }
            set
            {
                vitesse = value;
            }
        }

    }
}
