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

        public static bool onBlock;

        public int x, y;
        public int width = 70;
        public int height = 90;
        public int speed = 5;
        Region playerRegion;
        PointF prevPosition = new PointF();
        

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
            playerRegion = new Region(new RectangleF(x, y, width, height));
        }

        public void Move(string direction)
        {
            prevPosition = new PointF(x, y);
            //Wall collisions
            if (direction == "left")
            {
                x -= speed;

                if (x < 0)
                    x = 0;
            }
            else if (direction == "right")
            {
                x += speed;
                if (x + width > GameScreen.width)
                {
                    x = GameScreen.width - width ;
                }
            }
            playerRegion = new Region(new RectangleF(x, y, width, height));
        }

        public bool BlockCollision(RectangleF b)
        {
            RectangleF playerRec = new RectangleF(x, y, width, height);

            // Find overlap area
            float overlapX = Math.Max(0, Math.Min(x + width, b.Right) - Math.Max(x, b.Left));
            float overlapY = Math.Max(0, Math.Min(y + height, b.Bottom) - Math.Max(y, b.Top));

            // Check if there is an intersection
            if (overlapX > 0 && overlapY > 0)
            {
                if (overlapX < overlapY)
                {
                    // Horizontal collisions
                    if (x + width / 2 < b.X + b.Width / 2)
                    {
                        // LEft collsions
                        x = (int)(b.X - width);
                    }
                    else
                    {
                        // Rigth collisons
                        x = (int)b.Right;
                    }
                }
                else
                {
                    // vertical collsions
                    if (y + height / 2 < b.Y + b.Height / 2)
                    {
                        // top collision 
                        y = (int)(b.Y - height);
                        onBlock = true;
                    }
                    else
                    {
                        // Bottom collision
                        y = (int)b.Bottom;

                    }
                }
                return true; 
            }
            onBlock = false;
            return false; 
        }

        public void SetIdle()
        {
            currentSprite = Properties.Resources.idle;
        }
    }
}