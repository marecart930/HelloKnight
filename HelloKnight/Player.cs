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
        Region playerRegion;
        PointF prevPosition = new PointF();
        public Rectangle block = new Rectangle(330, 330, 195, 60);

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
            playerRegion = new Region(new RectangleF(x, y, width, height));
        }

        public void Move(string direction)
        {
            prevPosition = new PointF(x, y);
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
            playerRegion = new Region(new RectangleF(x, y, width, height));
        }

        public bool BlockCollision(RectangleF b, Graphics g)
        {
            //RectangleF blockRec = new RectangleF(p.x, p.y, p.width, p.height);
            RectangleF playerRec = new RectangleF(x, y, width, height);

            Region blockRegion = new Region(b);
            blockRegion.Intersect(playerRegion);

            if (!blockRegion.IsEmpty(g))
            {
                RectangleF location = blockRegion.GetBounds(g);
                if (location.Width >= location.Height)
                {
                    if (b.Y + (b.Height / 2) > prevPosition.Y + (height / 2)) //above
                    {
                        y = (int)b.Y - height;
                        speed = 0;
                    }
                    else
                    {
                        y = (int)b.Y + (int)b.Height;
                        speed *= -1;
                    }
                }
                else
                {
                    if (b.X + (b.Width / 2) > x + (width / 2)) //to the left
                    {
                        x = (int)b.X - width;
                    }
                    else
                    {
                        x = (int)b.X + (int)b.Width;
                    }
                }
            }
            return b.IntersectsWith(playerRec);
        }

        public void SetIdle()
        {
            currentSprite = Properties.Resources.idle;
        }
    }
}