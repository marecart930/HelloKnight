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

        private int jumpSpeed = 12;
        private int force = 15;
        private int groundLevel = 290;

        int spriteNumber = 0;
        int animationTick = 0;
        int animationSpeed = 5;
        int dashSpeed = 10;
        int normalSpeed = 5;
        int dashDuration = 15;
        int dashTicksRemaining = 0;

        private bool canDoubleJump = false;
        private bool doubleJumped = false;

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
            this.groundLevel = hero.y;
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
                    if (hero.y == groundLevel || canDoubleJump)
                    {
                        Jump();
                    }
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
            hero.speed = dashSpeed;
        }

        private void Jump()
        {
            if (hero.y == groundLevel)
            {
                force = 15;
                canDoubleJump = true;
                doubleJumped = false;
            }
            else if (canDoubleJump && !doubleJumped)
            {
                force = 15;
                doubleJumped = true;
                canDoubleJump = false;
            }
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
                    hero.speed = normalSpeed;
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
                    canDoubleJump = false;
                    doubleJumped = false;
                    hero.SetIdle();
                }
            }

            Refresh();
        }
    }
}