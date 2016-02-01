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
    class Carte
    {
        private const int NB_COL = 28;
        private const int NB_LIG = 31;
        private ContentManager content;
        private ObjetAnime[,] listeObjets = new ObjetAnime[NB_LIG, NB_COL];
        private PathFinding pf;
        private int[,] map = {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 2, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

        };


        public Carte(ContentManager c)
        {
            content = c;
            initialisation();
            pf = new PathFinding(map);
            pf.dijkstra(10);
        }

        public void initialisation()
        {
            for(int i = 0; i < NB_LIG; i++)
            {
                for (int j = 0; j < NB_COL; j++)
                {
                    if ((i == 23) && (j == 1))
                    {
                        ObjetAnime pvr = load("pouvoir");
                        if (pvr != null)
                            listeObjets[i, j] = pvr;
                    }
                    else if ((i == 4) && (j == 1))
                    {
                        ObjetAnime pvr = load("pouvoir");
                        if (pvr != null)
                            listeObjets[i, j] = pvr;
                    }
                    else if ((i == 4) && (j == 26))
                    {
                        ObjetAnime pvr = load("pouvoir");
                        if (pvr != null)
                            listeObjets[i, j] = pvr;
                    }
                    else if ((i == 23) && (j == 26))
                    {
                        ObjetAnime pvr = load("pouvoir");
                        if (pvr != null)
                            listeObjets[i, j] = pvr;
                    }
                    else if (map[i, j] == 0)
                    {
                        ObjetAnime mur = load("mur");
                        if (mur != null)
                            listeObjets[i, j] = mur;
                    }
                    else if (map[i, j] == 1)
                    {
                        ObjetAnime haricot = load("haricot");
                        if (haricot != null)
                            listeObjets[i, j] = haricot;
                    }
                }
            }
            ObjetAnime murFantome = new ObjetAnime(content.Load<Texture2D>(@"sprites\barriereFantome"), new Vector2(0f, 0f), new Vector2(20f, 20f), "mur");
            listeObjets[12, 13] = murFantome;

        }

        public ObjetAnime load(String objet)
        {
            // on charge un objet mur 
            if (objet == "mur")
            {
                ObjetAnime mur = new ObjetAnime(content.Load<Texture2D>(@"sprites\mur"), new Vector2(0f, 0f), new Vector2(20f, 20f), "mur");
                return mur;
            }
            //On charge un haricot
            else if (objet == "haricot")
            {
                ObjetAnime haricot = new ObjetAnime(content.Load<Texture2D>(@"sprites\bean"), new Vector2(0f, 0f), new Vector2(20f, 20f), "haricot");
                return haricot;
            }
            //On charge un pouvoir
            else if (objet == "pouvoir")
            {
                ObjetAnime haricot = new ObjetAnime(content.Load<Texture2D>(@"sprites\pouvoir"), new Vector2(0f, 0f), new Vector2(20f, 20f), "pouvoir");
                return haricot;
            }

            return null;
        }

        public void draw(SpriteBatch sb)
        {
            for (int i = 0; i < NB_LIG; i++)
            {
                for (int j = 0; j < NB_COL; j++)
                {
                    int xpos, ypos;
                    xpos = i * 20;
                    ypos = j * 20;
                    Vector2 pos = new Vector2(ypos, xpos);
                    if (listeObjets[i, j] != null)
                    {
                        sb.Begin();
                        sb.Draw(listeObjets[i, j].Texture, pos, Color.White);
                        sb.End();
                    }
                }
            }
        }

        public int getNbCol() {
            return NB_COL;
        }

        public int getNbLig()
        {
            return NB_LIG;
        }

        public ObjetAnime[,] getListeObjets()
        {
            return listeObjets;
        }

        public void setListeObjets(ObjetAnime[,] newListeObjets)
        {
            this.listeObjets = newListeObjets;
        }
    }
}
