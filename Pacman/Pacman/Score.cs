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
    class Score
    {
        private int score;
        private SpriteFont sf;
        private static Score sc;

        private Score(ContentManager cm)
        {
            score = 0;
            sf = cm.Load<SpriteFont>("SpriteFont1");
        }

        public static Score instanceScore(ContentManager cm)
        {
            if (sc == null)
            {
                sc = new Score(cm);
                return sc;
            }
            else
                return sc;
        }

        public void addScore(String mange)
        {
            if (mange.Equals("pouvoir"))
                score += 10;
            else if (mange.Equals("haricot"))
                score += 2;
            else if (mange.Equals("fantome"))
                score += 50;
        }

        public void draw(SpriteBatch sb)
        {
            string text1 = "Score : " + score;
            sb.Begin();
            sb.DrawString(this.sf, text1, new Vector2(600, 20), Color.Red);
            sb.End();
        }

        public void reset()
        {
            score = 0;
        }
    }
}
