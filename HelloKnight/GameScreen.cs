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
        bool aKeyDown, dKeyDown, spaceKeyDown, shiftKeyDown;

        Player hero;
        public static int width, height;

        private int jumpSpeed = 12; // Determines how much the character moves up or down per tick when jumping or falling
        private int force = 15; // Determines how long the character keeps moving up when the jump starts
        private int groundLevel = 290;

        int spriteNumber = 0;
        int animationTick = 0; // Counter for controlling animation speed
        int animationSpeed = 5; // Number of ticks per frame change
        int dashSpeed = 10;
        int normalSpeed = 5;
        int dashDuration = 15; // Number of ticks the dash lasts
        int dashTicksRemaining = 0; // Counter for remaining dash ticks

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

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.D:
                    dKeyDown = false;
                    break;
                case Keys.Space:
                    spaceKeyDown = false;
                    break;
                case Keys.ShiftKey:
                    shiftKeyDown = false;
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
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.D:
                    dKeyDown = true;
                    break;
                case Keys.Space:
                    spaceKeyDown = true;
                    break;
                case Keys.ShiftKey:
                    if (!shiftKeyDown && dashTicksRemaining <= 0)
                    {
                        shiftKeyDown = true;
                        Dash();
                    }
                    break;
            }
        }

        private void UpdateAnimation(List<Image> spriteList)
        {
            animationTick++;
            if (animationTick >= animationSpeed)
            {
                animationTick = 0;
                spriteNumber++;
                if (spriteNumber >= spriteList.Count)
                {
                    spriteNumber = 0;
                }
                hero.currentSprite = spriteList[spriteNumber];
            }
        }

        private void Dash()
        {
            dashTicksRemaining = dashDuration;
            hero.speed = dashSpeed; // Increase speed for dashing
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Handle dash duration
            if (dashTicksRemaining > 0)
            {
                dashTicksRemaining--;
                if (aKeyDown)
                {
                    hero.Move("left");
                }
                else if (dKeyDown)
                {
                    hero.Move("right");
                }

                if (dashTicksRemaining == 0)
                {
                    hero.speed = normalSpeed; // Reset speed after dashing
                }
            }

            // Handle player horizontal movement
            if (dKeyDown && hero.y == groundLevel)
            {
                hero.Move("right");
                UpdateAnimation(Form1.rrun);
            }
            else if (aKeyDown && hero.y == groundLevel)
            {
                hero.Move("left");
                UpdateAnimation(Form1.run);
            }
            else if (!spaceKeyDown && hero.y >= groundLevel)
            {
                hero.SetIdle();
            }

            // Handle jumping logic
            if (spaceKeyDown && force > 0)
            {
                hero.y -= jumpSpeed;
                force--;
                if (dKeyDown)
                {
                    hero.Move("right");
                    UpdateAnimation(Form1.rjump);
                }
                else if (aKeyDown)
                {
                    hero.Move("left");
                    UpdateAnimation(Form1.jump);
                }
                else
                {
                    UpdateAnimation(Form1.jump);
                }
            }
            else if (hero.y < groundLevel)
            {
                hero.y += jumpSpeed;
                if (dKeyDown)
                {
                    hero.Move("right");
                    UpdateAnimation(Form1.rjump);
                }
                else if (aKeyDown)
                {
                    hero.Move("left");
                    UpdateAnimation(Form1.jump);
                }
                else
                {
                    UpdateAnimation(Form1.jump);
                }

                if (hero.y >= groundLevel)
                {
                    hero.y = groundLevel;
                    force = 15;
                    hero.SetIdle();
                }
            }

            Refresh();
        }
    }
}
