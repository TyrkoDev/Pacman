using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class HitBox
    {
        private int xPos;
        private int yPos;
        private int width;
        private int height;

        public HitBox(int x, int y, int width, int height)
        {
            xPos = x;
            yPos = y;
            this.width = width;
            this.height = height;
        }

        public bool collide(HitBox h1)
        {
            return true;
        }
    }
}
