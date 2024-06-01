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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace HelloKnight
{
    public partial class GameScreen : UserControl
    {
        bool leftArrowDown, rightArrowDown, spaceKeyDown;

        Player hero;
        public static int width, height;

        private int jumpSpeed = 12; // Determines how much the character moves up or down per tick when jumping or falling
        private int force = 15; // Determines how long the character keeps moving up when the jump starts
        private int groundLevel = 290;

        int spriteNumber = 0;
        int animationTick = 0; // Counter for controlling animation speed
        int animationSpeed = 5; // Number of ticks per frame change

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
            hero = new Player(400, 290); // Initial position of hero
            this.groundLevel = hero.y; // Set the initial ground level
        }

        private void UpdateJumpSprite()
        {
            animationTick++;
            if (animationTick >= animationSpeed)
            {
                animationTick = 0;
                spriteNumber++;
                if (spriteNumber >= Form1.jump.Count)
                {
                    spriteNumber = 0;
                }
                hero.currentSprite = Form1.jump[spriteNumber];
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Move the player
            if (rightArrowDown)
            {
                hero.Move("right");
            }
            else if (leftArrowDown)
            {
                hero.Move("left");
            }
            else if (!spaceKeyDown && hero.y >= groundLevel) // Ensure not to override jump animation
            {
                hero.SetIdle(); // Set the player to idle if no keys are pressed and on the ground
            }

            // Handle jumping logic
            if (spaceKeyDown && force > 0)
            {
                hero.y -= jumpSpeed; // Move up
                force--;
                UpdateJumpSprite(); // Update jump sprite
            }
            else if (hero.y < groundLevel)
            {
                hero.y += jumpSpeed; // Move down
                UpdateJumpSprite(); // Update jump sprite

                if (hero.y >= groundLevel)
                {
                    hero.y = groundLevel; // Ensure character doesn't fall below ground level
                    force = 15; // Reset the jump force

                    // Reset to idle sprite
                    hero.currentSprite = Properties.Resources.idle;
                    spriteNumber = 0;
                }
            }

            Refresh();
        }
    }
}