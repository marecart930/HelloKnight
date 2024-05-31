using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;

namespace HelloKnight
{
    internal class Player
    {
        int spriteNumber = 0;
        public Image currentSprite = Properties.Resources.idle;

        public int x, y;
        public int width = 30;
        public int height = 50;
        public int speed = 5;


        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Move(string direction)
        {
            if (direction == "left")
            {
                x -= speed;
                currentSprite = Form1.run[spriteNumber];
                spriteNumber++;

            }
            else if (direction == "right")
            {
                x += speed;
                currentSprite = Form1.rrun[spriteNumber];
                spriteNumber++;
            }


            if (spriteNumber > 3)
            {
                spriteNumber = 0;
            }
        }
    }
}