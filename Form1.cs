using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using EpicCreativeGameName_0._2;

namespace EpicCreativeGameName
{
    public partial class EpicCreativeGameName : Form
    {
        ///Variables 

        //defining booleans

        bool GoLeft, GoRight, Jumping, HasKey, CanJump, EnemyLoad, HellDeath, HellFenceOpen, ActivateButton, EnemyCutsceneTimer;

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

        //defining Score

        int ScorePoints;

        //defining vertical speed and horizontal speed for Enemy

        int EnemyVsp = 0;
        int EnemyHsp = 0;

        //enemy settings

        int EnemyMoveSpeed = 9;
        int EnemyAcceleration = 1;
        int EnemyCutscene = 120;

        //basic variables

        int TrueCoordsX, TrueCoordsY;

        SoundPlayer BackGroundMusicCalm = new SoundPlayer("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Sound_and_Music/Superboy.wav");
        SoundPlayer BackGroundMusicIntens = new SoundPlayer("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Sound_and_Music/Fiberitron Loop.wav");
        SoundPlayer GameOverEffect = new SoundPlayer("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Sound_and_Music/gameover.wav");

        public static int OriginalScore;
        public static bool menu;

        public EpicCreativeGameName()
        {
            InitializeComponent();
        }

        private void EpicCreativeGameName_Load(object sender, EventArgs e)
        {
            ScorePoints = OriginalScore;


            ///Disapearing Objects

            //disapearing player masks

            PlayerFeet.Visible = false;
            PlayerVerticalMask.Visible = false;
            PlayerHorizontalMask.Visible = false;
            PlayerMiddleMask.Visible = false;

            //disapearing game triggers

            HellTrigger.Visible = false;
            HelpText.Visible = false;
            FallTrigger.Visible = false;
            BlueSwichHelpText.Visible = false;

            //disapearing enemy masks

            EnemyHorizontalMask.Visible = false;
            EnemyVerticalMask.Visible = false;
            EnemyMiddelMask.Visible = false;

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

            //Music and SFX

            BackGroundMusicCalm.PlayLooping();

            ///drawing

            SpriteDrawing();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            if (!menu)
            {
                GameTimer.Stop();
                mainMenu mm = new mainMenu();
                mm.Show();
                this.Hide();
            }
            ///Displaying Score

            Score.Text = "score: " + ScorePoints;

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
                TrueCoordsX -= this.ClientSize.Width;
            }

            if (Player.Left > this.ClientSize.Width)
            {
                MoveGameElements("Right");
                TrueCoordsX += this.ClientSize.Width;
            }

            if ((Player.Top + Player.Height) < 0)
            {
                MoveGameElements("Up");
                TrueCoordsY -= this.ClientSize.Height;
            }

            if (Player.Top > this.ClientSize.Height)
            {
                if (HellDeath)
                {
                    Die();
                }

                MoveGameElements("Down");
                TrueCoordsY += this.ClientSize.Height;
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

            FallTrigger.Left = Player.Left;
            HelpText.Left = HellFenceSwitch.Left;

            ///Advanced Collision And Jumping

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Enemy")
                { 
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (i_frames <= 0 && EnemyLoad)
                        {
                            health -= 1;
                            i_frames = 50;
                            health = Math.Max(health,0);

                            hsp = sign(EnemyHsp) * 30;
                            vsp = -10;
                        }
                    }
                }

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
                    

                    if (EnemyMiddelMask.Bounds.IntersectsWith(x.Bounds))
                    {
                        while (EnemyMiddelMask.Bounds.IntersectsWith(x.Bounds))
                        {
                            Enemy.Top += sign(-EnemyVsp);
                            Enemy.Left += sign(-EnemyHsp);

                            if ((EnemyVsp == 0) && (EnemyHsp == 0))
                            {
                                EnemyVsp = 1;
                            }

                            EnemyMiddelMask.Left = Enemy.Left + 1;
                            EnemyMiddelMask.Top = Enemy.Top + 1;
                        }
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
                }

                //coin collision

                if (x is PictureBox && (string)x.Tag == "Coin")
                {

                    if (Player.Bounds.IntersectsWith(x.Bounds) && x.Enabled)
                    {
                        x.Enabled = false;
                        ScorePoints++;
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

            //blue key collision

            if (Enemy.Bounds.IntersectsWith(DemonCageTrigger.Bounds) && !HasKey)
            {
                EnemyLoad = false;
                Enemy.Visible = false;
                DemonCage.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/CageClosingSprite.png");
                HasKey = true;
                BackGroundMusicIntens.Stop();
                BackGroundMusicCalm.PlayLooping();
            }

            if (Player.Bounds.IntersectsWith(DemonCageTrigger.Bounds))
            {
                switch (HasKey)
                {
                    case true:
                        BlueSwichHelpText.Text = "you've caught a demon";
                        break;
                    case false:
                        BlueSwichHelpText.Text = "Demon cage is empty";
                        break;
                }
                BlueSwichHelpText.Visible = true;
            }
            else
            {
                BlueSwichHelpText.Visible = false;
            }

            ///game triggers events
            
            if (!(((HellTrigger.Left + HellTrigger.Width) < 0) || (HellTrigger.Left > this.ClientSize.Width) || ((HellTrigger.Top + HellTrigger.Height) < 0) || (HellTrigger.Top > this.ClientSize.Height)))
            {
                HellDeath = true;
            }
            else
            {
                HellDeath = false;
            }

            if (FallTrigger.Top < Player.Top)
            {
                HellDeath = true;
            }

            if ((Player.Bounds.IntersectsWith(HellFenceSwitch.Bounds)) && !(HellFenceOpen))
            {
                HelpText.Visible = true;
                if (ActivateButton)
                {
                    HellFenceSwitch.Top += 20;
                    HellFenceSwitch.Height -= 20;
                    HellFenceOpen = true;
                    plr_input = false;
                    BackGroundMusicCalm.Stop();
                }
            }
            else
            {
                HelpText.Visible = false;
            }

            if (HellFenceOpen && !EnemyLoad)
            {
                HellFence.Height = approach(HellFence.Height, 0, 1);
                HellFence.Top += 1;
                if ((HellFence.Height == 0) && (!EnemyCutsceneTimer))
                {
                    Enemy.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/DemonRunLeftSprite.png");
                    EnemyCutsceneTimer = true;
                }
            }

            if (EnemyCutsceneTimer)
            {
                EnemyCutscene--;
                if (EnemyCutscene == 0)
                {
                    EnemyCutsceneTimer = false;
                    plr_input = true;
                    EnemyLoad = true;
                    BackGroundMusicIntens.PlayLooping();
                }
            }

            if (HasKey)
            {
                TreeGate.Image = null;
                TreeGate.Width = 0;
                TreeGate.Left = 0;
            }

            if (Player.Bounds.IntersectsWith(ForestPortal.Bounds))
            {
                OriginalScore = ScorePoints;

                BackGroundMusicCalm.Stop();
                BackGroundMusicIntens.Stop();
                GameTimer.Stop();
                Form2 lv1 = new Form2();
                this.Hide();
                lv1.Show();
            }

            ///GameElementOutput

            EnemyOutput();

            ///Horizontal and Vertical output Player
            if (plr_input)
            {
                Player.Left += hsp;
                Player.Top += vsp;
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
                if (x is PictureBox && (string)x.Tag == "Ground" || x is PictureBox && (string)x.Tag == "Coin" ||  x is PictureBox && (string)x.Tag == "Player" || x is PictureBox && (string)x.Tag == "Enemy" || x is PictureBox && (string)x.Tag == "Trigger" || x is PictureBox && (string)x.Tag == "CustomBackground")
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
            EnemyHsp = approach(EnemyHsp,(-sign(Enemy.Left - Player.Left) * EnemyMoveSpeed),EnemyAcceleration);
            EnemyVsp = approach(EnemyVsp,(-sign(Enemy.Top - Player.Top) * EnemyMoveSpeed),EnemyAcceleration);

            EnemyHorizontalMask.Left = Enemy.Left + EnemyHsp;
            EnemyHorizontalMask.Top = Enemy.Top + 1;

            EnemyVerticalMask.Left = Enemy.Left + 1;
            EnemyVerticalMask.Top = Enemy.Top + EnemyVsp;

            EnemyMiddelMask.Left = Enemy.Left + 1;
            EnemyMiddelMask.Top = Enemy.Top + 1;

            if (EnemyLoad){
                switch (sign(EnemyHsp))
                {
                    case 1:
                        Enemy.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/DemonRunRightSprite.png");
                        break;
                    case -1:
                        Enemy.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/DemonRunLeftSprite.png");
                        break;
                    default:
                        Enemy.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/DemonIdleSprite.png");
                        break;
                }
            }

            if (i_frames >= 0)
            {
                i_frames--;
            }

            if (health <= 0)
            {
                Die();
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

        public void SpriteDrawing()
        {
            TreeGate.Visible = true;
            HellFence.Visible = true;

            Enemy.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/DemonIdleSprite.png");
            Player.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/PlayerIdleSprite.png");
            DemonCage.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/CageOpenSprite.png");
            TreeGate.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/treebranche.png");
            HellFence.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/DemonGate.png");
            HellFenceSwitch.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/hellswitch.png");


            CustomBackground.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/BackgroundSprite.png");
        }

        public void Die()
        {
            Player.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/PlayerDeadSprite.png");
            GameOverEffect.Play();
            GameTimer.Stop();
            MessageBox.Show("you died");
            RestartGame();
        }
    }
}

///notes
///room width 1359, 830