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
using System.Timers;
using System.Diagnostics;

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
        private Stopwatch chrono;

        public Joueur(ContentManager c, Carte carte, Collision collision)
        {
            content = c;
            xPos = 14 * 20;
            yPos = 17 * 20;
            vitesse = 1;
            cd = new ControleurDeplacement(this, carte, collision);
            pacman = new ObjetAnime(content.Load<Texture2D>(@"sprites\pacman"), new Vector2(0f, 0f), new Vector2(20f, 20f), "pacman");
            chrono = new Stopwatch();
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
            String direction = cd.dir;

            if(!chrono.IsRunning)
                chrono.Start();

            if (direction == "droite")
            {
                if (chrono.ElapsedMilliseconds < 200)
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanDroite0");
                }
                else if ((chrono.ElapsedMilliseconds > 200) && (chrono.ElapsedMilliseconds < 400))
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanDroite1");
                }
                else if (chrono.ElapsedMilliseconds > 400)
                {
                    chrono.Stop();
                    chrono.Reset();
                }
            }

            if (direction == "gauche")
            {
                if (chrono.ElapsedMilliseconds < 200)
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanGauche0");
                }
                else if ((chrono.ElapsedMilliseconds > 200) && (chrono.ElapsedMilliseconds < 400))
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanGauche1");
                }
                else if (chrono.ElapsedMilliseconds > 400)
                {
                    chrono.Stop();
                    chrono.Reset();
                }
            }

            if (direction == "haut")
            {
                if (chrono.ElapsedMilliseconds < 200)
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanHaut0");
                }
                else if ((chrono.ElapsedMilliseconds > 200) && (chrono.ElapsedMilliseconds < 400))
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanHaut1");
                }
                else if (chrono.ElapsedMilliseconds > 400)
                {
                    chrono.Stop();
                    chrono.Reset();
                }
            }

            if (direction == "bas")
            {
                if (chrono.ElapsedMilliseconds < 200)
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanBas0");
                }
                else if ((chrono.ElapsedMilliseconds > 200) && (chrono.ElapsedMilliseconds < 400))
                {
                    pacman.Texture = content.Load<Texture2D>(@"sprites\pacmanBas1");
                }
                else if (chrono.ElapsedMilliseconds > 400)
                {
                    chrono.Stop();
                    chrono.Reset();
                }
            }
        }

        public void pouvoir(Fantome[] fantomes)
        {
            vitesse = 2;
            for(int i = 0; i < fantomes.Length; i++)
            {
                fantomes[i].setAfraid(true);
            }
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

        public int caseX()
        {
            if (xPos % 20 == 0)
                return xPos / 20;
            else
                return -1;
        }

        public int caseY()
        {
            if (yPos % 20 == 0)
                return yPos / 20;
            else
                return -1;
        }

    }
}
