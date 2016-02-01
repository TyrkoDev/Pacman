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
    class Fantome
    {
        private ContentManager content;
        private ObjetAnime ghost;
        private int xPos;
        private int yPos;
        private int vitesse;
        private Stopwatch chrono;
        private String nom;
        private bool afraid;
        private String chemin;

        public Fantome(ContentManager c, Carte carte, Collision collision, String nomFantome)
        {
            content = c;
            nom = nomFantome;
            if (nom == "fantomeRouge")
            {
                xPos = 14 * 20;
                yPos = 11 * 20;
            }
            else if (nom == "fantomeVert")
            {
                xPos = 13 * 20;
                yPos = 15 * 20;
            }
            else if (nom == "fantomeBleu")
            {
                xPos = 16 * 20;
                yPos = 15 * 20;
            }
            else if (nom == "fantomeRose")
            {
                xPos = 11 * 20;
                yPos = 15 * 20;
            }
            vitesse = 1;
            chemin = @"sprites\" + nomFantome;
            ghost = new ObjetAnime(content.Load<Texture2D>(chemin), new Vector2(0f, 0f), new Vector2(20f, 20f), "fantome");
            chrono = new Stopwatch();
        }

        public void draw(SpriteBatch sb)
        {
            Vector2 pos = new Vector2(xPos, yPos);

            sb.Begin();
            sb.Draw(ghost.Texture, pos, Color.White);
            sb.End();
        }

        public void update()
        {
            if (afraid)
            {
                if (!chrono.IsRunning)
                    chrono.Start();
                if (chrono.ElapsedMilliseconds < 5000)
                {
                    ghost.Texture = content.Load<Texture2D>(@"sprites\FantomePeur0");
                }
                else if ((chrono.ElapsedMilliseconds > 5000) && (chrono.ElapsedMilliseconds < 8000))
                {
                    if (chrono.ElapsedMilliseconds < 5500)
                        ghost.Texture = content.Load<Texture2D>(@"sprites\FantomePeur1");
                    else if ((chrono.ElapsedMilliseconds > 6000) && (chrono.ElapsedMilliseconds < 6500))
                        ghost.Texture = content.Load<Texture2D>(@"sprites\FantomePeur0");
                    else if ((chrono.ElapsedMilliseconds > 7000) && (chrono.ElapsedMilliseconds < 7500))
                        ghost.Texture = content.Load<Texture2D>(@"sprites\FantomePeur1");
                    else
                        ghost.Texture = content.Load<Texture2D>(@"sprites\FantomePeur0");
                }
                else if (chrono.ElapsedMilliseconds > 8000)
                {
                    chrono.Stop();
                    chrono.Reset();
                    afraid = false;
                    ghost = new ObjetAnime(content.Load<Texture2D>(chemin), new Vector2(0f, 0f), new Vector2(20f, 20f), chemin);
                }
            }
        }

        public void setAfraid(bool set)
        {
            afraid = set;
        }

        public bool isAfraid()
        {
            return afraid;
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
