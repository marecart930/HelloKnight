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
        bool leftArrowDown, rightArrowDown, spaceKeyDown;

        Player hero;
        public static int width, height;

        public Image currentSprite = Properties.Resources.idle;

        private int jumpSpeed = 10;
        private int force = 10;
        private int groundLevel = 290;



        private void GameScreen_KeyUp(object sender, KeyEventArgs e)

        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceKeyDown = false;
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
                case Keys.Space:
                    spaceKeyDown = true;
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
            hero = new Player(400, 290);// Initial position of hero
            this.groundLevel = hero.y; // Set the initial ground level
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move the player
            if (rightArrowDown)
            {
                hero.Move("right");
            }

            if (leftArrowDown)
            {
                hero.Move("left");
            }

            // Handle jumping logic
            if (spaceKeyDown && force > 0)
            {
                hero.y -= jumpSpeed; // Move up
                force--;
            }
            else if (hero.y < groundLevel)
            {
                hero.y += jumpSpeed; // Move down
                if (hero.y >= groundLevel)
                {
                    hero.y = groundLevel; // Ensure character doesn't fall below ground level
                    force = 10; // Reset the jump force
                }
             
            }
             Refresh();
        }
    }
}