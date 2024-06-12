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

                if (x < 0)
                    x = 0;
            }
            else if (direction == "right")
            {
                x += speed;
                if (x + width > GameScreen.width - 50)
                {
                    x = GameScreen.width - width - 50;
                }
            }
        }

        public void SetIdle()
        {
            currentSprite = Properties.Resources.idle;
        }
    }
}