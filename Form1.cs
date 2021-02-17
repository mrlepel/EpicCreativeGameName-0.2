using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpicCreativeGameName
{
    public partial class EpicCreativeGameName : Form
    {
        ///Variables 

        //defining booleans

        bool GoLeft, GoRight, Jumping, HasKey, CanJump;

        //defining vertical speed and horizontal speed for Player

        int hsp = 0;
        int vsp = 0;

        //Platforming Settings

        int JumpSpeed = 20;
        int Force = 1;
        int Acceleration = 1;
        int MaxForce = 50;
        int PlayerSpeed = 10;
        int JumpMargin = 31;

        //defining Score

        int ScorePoints = 0;

        //defining vertical speed and horizontal speed for Enemy

        int EnemyHsp = 0;
        int EnemyVsp = 0;

        //enemy settings

        int EnemyMoveSpeed = 3;
        bool EnemyLoad = false;

        public EpicCreativeGameName()
        {
            InitializeComponent();
        }

        private void EpicCreativeGameName_Load(object sender, EventArgs e)
        {
            ///Disapearing Objects

            //disapearing player masks

            PlayerFeet.Visible = false;
            PlayerVerticalMask.Visible = false;
            PlayerHorizontalMask.Visible = false;
            PlayerMiddleMask.Visible = false;

            //disapearing game triggers

            FallPoint.Visible = false;

            //disapearing enemy masks

            EnemyHorizontalMask.Visible = false;
            EnemyVerticalMask.Visible = false;


        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            ///Displaying Score

            Score.Text = "Vertical speed: " + vsp;



            ///Calculations Of Player Input

            //Gravity

            vsp += Force;
            vsp = Math.Min(vsp, MaxForce);

            //Right/Left Movement and acceleration

            if (GoLeft)
            {
                hsp = approach(hsp, -PlayerSpeed, Acceleration);
            }

            if (GoRight)
            {
                hsp = approach(hsp, PlayerSpeed, Acceleration);
            }

            if (((!GoRight) && (!GoLeft)) || ((GoRight) && (GoLeft)))
            {
                hsp = approach(hsp, 0, Acceleration);
            }

            //Movement Inbetween Scenes

            if ((Player.Left - Player.Width) < 0)
            {
                MoveGameElements("Left");
            }

            if (Player.Left > this.ClientSize.Width)
            {
                MoveGameElements("Right");
            }

            if ((Player.Top - Player.Height) < 0)
            {
                MoveGameElements("Up");
            }

            if (Player.Top > this.ClientSize.Height)
            {
                MoveGameElements("Down");
            }

            ///Game Element behavior

            EnemyBehavior();

            //Placing Player Masks

            PlayerFeet.Top = Player.Top + (Player.Width - JumpMargin);
            PlayerFeet.Left = Player.Left + 1;

            PlayerVerticalMask.Left = Player.Left + 1;
            PlayerVerticalMask.Top = Player.Top + vsp;

            PlayerHorizontalMask.Left = Player.Left + hsp;
            PlayerHorizontalMask.Top = Player.Top + 1;

            PlayerMiddleMask.Left = Player.Left + 1;
            PlayerMiddleMask.Top = Player.Top + 1;

            //Placing Game Triggers

            FallPoint.Left = Player.Left;

            ///Advanced Collision And Jumping

            foreach (Control x in this.Controls)
            {
                //wall collision

                if (x is PictureBox && (string)x.Tag == "Ground")
                {
                    if (PlayerMiddleMask.Bounds.IntersectsWith(x.Bounds))
                    {
                        while (PlayerMiddleMask.Bounds.IntersectsWith(x.Bounds))
                        {
                            Player.Top += sign(-vsp);
                            Player.Left += sign(-hsp);

                            if ((vsp == 0) && (hsp == 0))
                            {
                                vsp = 1;
                            }

                            PlayerMiddleMask.Left = Player.Left + 1;
                            PlayerMiddleMask.Top = Player.Top + 1;
                        }
                    }

                    //checking if player can jump

                    if (PlayerFeet.Bounds.IntersectsWith(x.Bounds))
                    {
                        CanJump = true;
                    }
                    else
                    {
                        CanJump = false;
                    }

                    //vertical player wall colision

                    if (PlayerVerticalMask.Bounds.IntersectsWith(x.Bounds))
                    {
                        while (!Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            Player.Top += sign(vsp);
                        }

                        vsp = 0;
                    }

                    //horizontal player wall colision

                    if (PlayerHorizontalMask.Bounds.IntersectsWith(x.Bounds))
                    {
                        while (!Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            Player.Left += sign(hsp);
                        }

                        hsp = 0;
                    }

                    //vertical enemy wall colision

                    if (EnemyVerticalMask.Bounds.IntersectsWith(x.Bounds))
                    {
                        while (!Enemy.Bounds.IntersectsWith(x.Bounds))
                        {
                            Enemy.Top += sign(EnemyVsp);
                        }

                        EnemyVsp = 0;
                    }

                    //horizontal enemt wall colision

                    if (EnemyHorizontalMask.Bounds.IntersectsWith(x.Bounds))
                    {
                        while (!Enemy.Bounds.IntersectsWith(x.Bounds))
                        {
                            Enemy.Left += sign(EnemyHsp);
                        }

                        EnemyHsp = 0;
                    }

                    //extra sorting

                    x.BringToFront();
                    Score.BringToFront();
                }

                //jumping

                if (Jumping && CanJump)
                {
                    vsp = -JumpSpeed;
                };

                //coin collision

                if (x is PictureBox && (string)x.Tag == "Coin")
                {

                    if (Player.Bounds.IntersectsWith(x.Bounds) && x.Visible)
                    {
                        x.Visible = false;
                        ScorePoints++;
                    }

                }
            }

            //blue key collision

            if (Enemy.Bounds.IntersectsWith(BlueCoin.Bounds) && BlueCoin.Visible)
            {
                HasKey = true;
            }
            else
            {
                HasKey = false;
            }

            //door collision

            if (Player.Bounds.IntersectsWith(Door.Bounds) && HasKey)
            {
                GameTimer.Stop();
                MessageBox.Show("yep");
                RestartGame();
            }

            ///game triggers events

            //fallpoint collision

            if (Player.Bounds.IntersectsWith(FallPoint.Bounds))
            {
                GameTimer.Stop();
                MessageBox.Show("nope");
                RestartGame();
            }

            ///GameElementOut

            EnemyOutput();

            ///Horizontal and Vertical output Player

            Player.Left += hsp;
            Player.Top += vsp;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            ///Release Of Player Input

            if (e.KeyCode == Keys.Left || (e.KeyCode == Keys.A))
            {
                GoLeft = false;
            }

            if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.D))
            {
                GoRight = false;
            }

            if ((e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Up) || (e.KeyCode == Keys.W))
            {
                Jumping = false;
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            ///Player Input

            if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.A))
            {
                GoLeft = true;
            }

            if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.D))
            {
                GoRight = true;
            }

            if ((e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Up) || (e.KeyCode == Keys.W))
            {
                Jumping = true;
            }

        }

        private void GameClose(object sender, FormClosedEventArgs e)
        {
            ///closing all running instances

            Application.Exit();
        }


        private void RestartGame()
        {
            ///opening a new instance

            EpicCreativeGameName newWindow = new EpicCreativeGameName();
            newWindow.Show();
            this.Hide();
        }

        private void MoveGameElements(string direction)
        {
            ///Moving To New Screen

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Ground" || x is PictureBox && (string)x.Tag == "Coin" || x is PictureBox && (string)x.Tag == "BlueCoin" || x is PictureBox && (string)x.Tag == "Door" || x is PictureBox && (string)x.Tag == "Player" || x is PictureBox && (string)x.Tag == "FallPoint" || x is PictureBox && (string)x.Tag == "Enemy")
                {
                    switch (direction)
                    {
                        case "Left":
                            x.Left += this.ClientSize.Width;
                            break;
                        case "Right":
                            x.Left -= this.ClientSize.Width;
                            break;
                        case "Up":
                            x.Top += this.ClientSize.Height;
                            break;
                        case "Down":
                            x.Top -= this.ClientSize.Height;
                            break;
                    }
                }
            }

        }


        private int sign(int var)
        {
            ///Returns -1 at a negative number 1 by a positive and 0 at a 0

            int x = 0;

            if (var < 0)
            {
                x = -1;
            }

            if (var > 0)
            {
                x = 1;
            }

            return x;
        }

        private int approach(int a, int b, int amount)
        {
            ///approaches a towards b at the given amount

            if (a < b)
            {
                a += amount;
                a = Math.Min(a, b);
            }

            if (a > b)
            {
                a -= amount;
                a = Math.Max(a, b);
            }

            return a;
        }

        private void EnemyBehavior()
        {
            EnemyHsp = -sign(Enemy.Left - Player.Left) * EnemyMoveSpeed;
            EnemyVsp = -sign(Enemy.Top - Player.Top) * EnemyMoveSpeed;

            EnemyHorizontalMask.Left = Enemy.Left + EnemyHsp;
            EnemyHorizontalMask.Top = Enemy.Top + 1;

            EnemyVerticalMask.Left = Enemy.Left + 1;
            EnemyVerticalMask.Top = Enemy.Top + EnemyVsp;

            if (((Enemy.Left + Enemy.Width) < 0) || (Enemy.Left > this.ClientSize.Width) || ((Enemy.Top + Enemy.Height) < 0) || (Enemy.Top > this.ClientSize.Height))
            {
                EnemyLoad = false;
            }
            else
            {
                EnemyLoad = true;
            }
        }

        private void EnemyOutput()
        {
            if (EnemyLoad)
            {
                Enemy.Left += EnemyHsp;
                Enemy.Top += EnemyVsp;
            };
        }
    }
}