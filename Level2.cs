using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace EpicCreativeGameName_0._2
{
    public partial class Level2 : Form
    {
        bool GoLeft, GoRight, Jumping, CanJump, ActivateButton;

        //defining vertical speed and horizontal speed for Player

        int hsp = 0;
        int vsp = 0;

        //basic player variables

        int WallJump = 0;
        int health = 3;
        int i_frames = 0;
        bool plr_input = true;

        //Platforming Settings

        int JumpSpeed = 20;
        int Force = 1;
        int Acceleration = 1;
        int MaxForce = 30;
        int PlayerSpeed = 10;
        int JumpMargin = 31;
        int WallGlide = 2;

        int ScorePoints;

        SoundPlayer BackGroundMusicCalm = new SoundPlayer("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Sound_and_Music/Fantasy Game Loop.wav");
        public Level2()
        {
            InitializeComponent();
        }

        private void MainTickEvent(object sender, EventArgs e)
        {
            Score.Text = "Score: " + ScorePoints;

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

            //jumping

            if (Jumping && CanJump)
            {
                vsp = -JumpSpeed;
                CanJump = false;
            };

            //player animation
            if (plr_input)
            {
                switch (sign(hsp))
                {
                    case 1:
                        Player.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/PlayerRunRightSprite.png");
                        break;
                    case -1:
                        Player.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/PlayerRunLeftSprite.png");
                        break;
                    default:
                        Player.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/PlayerIdleSprite.png");
                        break;
                }
            }
            else
            {
                Player.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/PlayerIdleSprite.png");
            }

            //walljump

            if (!(WallJump == 0))
            {
                vsp = Math.Min(vsp, WallGlide);

                if (Jumping)
                {
                    hsp = WallJump * PlayerSpeed;
                    vsp = -JumpSpeed;
                }

                WallJump = 0;
            }

            //Movement Inbetween Scenes

            if ((Player.Left + Player.Width) < 0)
            {
                MoveGameElements("Left");
            }

            if (Player.Left > this.ClientSize.Width)
            {
                MoveGameElements("Right");
            }

            if ((Player.Top + Player.Height) < 0)
            {
                MoveGameElements("Up");
            }

            if (Player.Top > this.ClientSize.Height)
            {
                MoveGameElements("Down");
            }

            //Placing Player Masks

            PlayerFeet.Top = Player.Top + (Player.Width - JumpMargin);
            PlayerFeet.Left = Player.Left + 1;

            PlayerVerticalMask.Left = Player.Left + 1;
            PlayerVerticalMask.Top = Player.Top + vsp;

            PlayerHorizontalMask.Left = Player.Left + hsp;
            PlayerHorizontalMask.Top = Player.Top + 1;

            PlayerMiddleMask.Left = Player.Left + 1;
            PlayerMiddleMask.Top = Player.Top + 1;

            ///Advanced Collision And Jumping

            foreach (Control x in this.Controls)
            {
                //wall collision

                if (x is PictureBox && (string)x.Tag == "Ground")
                {
                    //checking if player can jump

                    if (PlayerFeet.Bounds.IntersectsWith(x.Bounds))
                    {
                        CanJump = true;
                    }

                    //anti wallclip

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

                        WallJump = sign(-hsp);

                        hsp = 0;
                    }
                }

                //coin collision

                if (x is PictureBox && (string)x.Tag == "Coin")
                {

                    if (Player.Bounds.IntersectsWith(x.Bounds) && x.Enabled)
                    {
                        ScorePoints++;
                        x.Enabled = false;
                    }

                    if (!x.Enabled)
                    {
                        x.Width -= 2;
                        x.Height -= 2;
                        x.Left++;
                        x.Top++;
                    }

                }
            }

            //healthbar
            switch (health)
            {
                case 3:
                    HealthBar.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/health_3-3.png");
                    break;
                case 2:
                    HealthBar.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/health_2-3.png");
                    break;
                case 1:
                    HealthBar.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/health_1-3.png");
                    break;
                case 0:
                    HealthBar.Image = null;
                    break;
            }

            //LevelEnd

            if (Player.Bounds.IntersectsWith(LevelEnd.Bounds))
            {
                EpicCreativeGameName.EpicCreativeGameName.OriginalScore = ScorePoints;

                BackGroundMusicCalm.Stop();
                MainGameTimer.Stop();
                this.Hide();
            }

            ///Horizontal and Vertical output Player
            if (plr_input)
            {
                Player.Left += hsp;
                Player.Top += vsp;
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

            if ((e.KeyCode == Keys.Z) || (e.KeyCode == Keys.Down))
            {
                ActivateButton = true;
            }

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

            if ((e.KeyCode == Keys.Z) || (e.KeyCode == Keys.Down))
            {
                ActivateButton = false;
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Ground")
                {
                    x.Visible = false;
                }

                if (x is PictureBox && (string)x.Tag == "Coin")
                {
                    x.BackgroundImage = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/Coin.png");
                }
            }

            PlayerHorizontalMask.Visible = false;
            PlayerVerticalMask.Visible = false;
            PlayerMiddleMask.Visible = false;
            PlayerFeet.Visible = false;

            SpriteDrawing();

            BackGroundMusicCalm.PlayLooping();

            ScorePoints = EpicCreativeGameName.EpicCreativeGameName.OriginalScore;
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
        private void MoveGameElements(string direction)
        {
            ///Moving To New Screen

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Ground" || x is PictureBox && (string)x.Tag == "Coin" || x is PictureBox && (string)x.Tag == "Player" || x is PictureBox && (string)x.Tag == "Enemy" || x is PictureBox && (string)x.Tag == "Trigger" || x is PictureBox && (string)x.Tag == "CustomBackground")
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
        public void SpriteDrawing()
        { 
            Player.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/PlayerIdleSprite.png");
            CustomBackground.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/FinaleBackground.png");
        }
    }
}