using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace HelloKnight
{
    public partial class GameScreen : UserControl
    {
        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        Player hero;
        public static int width, height;
        
        public Image currentSprite = Properties.Resources.idle;


        private void GameScreen_KeyUp(object sender, KeyEventArgs e)

        {
            currentSprite = Form1.player[1];

            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(hero.currentSprite, hero.x, hero.y, hero.width + 50, hero.height + 50);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        public GameScreen()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            width = this.Width;
            height = this.Height;
            hero = new Player(400, 290);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move the player
            if (upArrowDown)
            {
                hero.Move("up");
            }

            if (downArrowDown)
            {
                hero.Move("down");
            }

            if (rightArrowDown)
            {
                hero.Move("right");
            }

            if (leftArrowDown)
            {
                hero.Move("left");
            }
            Refresh();
        }

    }
}