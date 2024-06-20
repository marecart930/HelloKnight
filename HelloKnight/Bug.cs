using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;

namespace HelloKnight
{
    internal class Bug
    {
        public Image currentBugSprite;
        public int x, y;
        public int width = 100;
        public int height = 100;

        public Bug(int startX, int startY)
        {
            x = startX;
            y = startY;
            currentBugSprite = Form1.bug[0]; // Initialize the bug sprite
        }
    }
}

