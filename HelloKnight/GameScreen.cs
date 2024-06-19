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
        private bool isAttacking = false;

        Rectangle block = new Rectangle(380, 330, 140, 60);
        Rectangle block2 = new Rectangle(200, 150, 140, 60);
        Rectangle onRec;

        List<Rectangle> blocks = new List<Rectangle>();

        public GameScreen()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            width = this.Width;
            height = this.Height;
            hero = new Player(200, 290);
            this.groundLevel = hero.y;

            blocks.Add(block);
            this.blocks.Add(block2);
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
            e.Graphics.DrawImage(hero.currentSprite, hero.x, hero.y, hero.width, hero.height);
            e.Graphics.FillRectangle(Brushes.White, block);
            e.Graphics.FillRectangle(Brushes.White, block2);
            //e.Graphics.DrawImage(Form1.platfrom, block2);
            //e.Graphics.DrawRectangle(Pens.White, hero.x, hero.y, hero.width, hero.height);
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
                case Keys.B:
                    int check = 1;
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.R:
                    Application.Restart();
                    break;
            }
        }

        private void GameScreen_MouseClick(object sender, MouseEventArgs e)
        {
            isAttacking = true;
            spriteNumber = 0; // Reset sprite animation to the start
            if (aKeyDown)
            {
                UpdateAnimation(Form1.attack);
            }
            else if (dKeyDown)
            {
                UpdateAnimation(Form1.rattack);
            }
            else
            {
                UpdateAnimation(Form1.attack);
            }
        }

        private void UpdateAnimation(List<Image> spriteList)
        {
            if (isAttacking)
            {
                // Use a specific attack animation set
                spriteList = aKeyDown ? Form1.attack : (dKeyDown ? Form1.rattack : Form1.attack);

                animationTick++;
                if (animationTick >= animationSpeed)
                {
                    animationTick = 0;
                    spriteNumber++;
                    if (spriteNumber >= spriteList.Count)
                    {
                        spriteNumber = 0;
                        isAttacking = false; // Reset attacking state once animation finishes
                    }
                    hero.currentSprite = spriteList[spriteNumber];
                }
            }
            else
            {
                // Default animation logic
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
        }

        private void Dash()
        {
            dashTicksRemaining = dashDuration;
            hero.speed = dashSpeed;
        }

        private void Jump()
        {
            if (hero.y == groundLevel || Player.onBlock)
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
            //using (Graphics g = this.CreateGraphics())
            //{
            //    //block collision can be called for any block to automatically give it collisions saving the time of coding collisions for each block
            //    hero.BlockCollision(block, g);
            //    hero.BlockCollision(block2, g);
            //}

            foreach (Rectangle rec in blocks)
            {
                if (hero.BlockCollision(rec))
                {
                    if (Player.onBlock)
                    {
                        onRec = rec;
                        break;
                    }
                }
            }



            // Handle attack animation priority
            if (isAttacking)
            {
                if (aKeyDown)
                {
                    UpdateAnimation(Form1.attack);
                }
                else if (dKeyDown)
                {
                    UpdateAnimation(Form1.rattack);
                }
                else
                {
                    UpdateAnimation(Form1.attack);
                }
                Refresh();
                return; // Exit early to prioritize attack animation
            }

            // Handle dash duration
            if (dashTicksRemaining > 0)
            {
                dashTicksRemaining--;
                if (aKeyDown)
                {
                    hero.Move("left");
                    //UpdateAnimation(Form1.dash);
                }
                else if (dKeyDown)
                {
                    hero.Move("right");
                    //UpdateAnimation(Form1.rdash);
                }

                if (dashTicksRemaining == 0)
                {
                    hero.speed = normalSpeed;
                }
            }

            // Handle player horizontal movement
            if (dKeyDown && (hero.y == groundLevel || Player.onBlock))
            {
                hero.Move("right");
                UpdateAnimation(Form1.rrun);
            }
            else if (aKeyDown && (hero.y == groundLevel || Player.onBlock))
            {
                hero.Move("left");
                UpdateAnimation(Form1.run);
            }
            else if (!spaceKeyDown && (hero.y >= groundLevel || Player.onBlock))
            {
                UpdateAnimation(Form1.player);
            }

            // Handle jumping logic
            if (spaceKeyDown && force > 0)
            {
                hero.y -= jumpSpeed;


                Player.onBlock = false;

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
            else if (hero.y < groundLevel && !Player.onBlock)
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
                    if (hero.y >= groundLevel)
                    {
                        hero.y = groundLevel;
                    }

                    force = 15;
                    canDoubleJump = false;
                    doubleJumped = false;
                    UpdateAnimation(Form1.player);
                }

            }
            if (Player.onBlock)
            {
                hero.y = onRec.Y - hero.height + 1;

                force = 15;
                canDoubleJump = true;
                doubleJumped = false;
            }

            Refresh();
        }
    }
}