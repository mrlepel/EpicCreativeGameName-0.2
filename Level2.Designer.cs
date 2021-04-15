
namespace EpicCreativeGameName_0._2
{
    partial class Level2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainGameTimer = new System.Windows.Forms.Timer(this.components);
            this.Player = new System.Windows.Forms.PictureBox();
            this.PlayerHorizontalMask = new System.Windows.Forms.PictureBox();
            this.PlayerMiddleMask = new System.Windows.Forms.PictureBox();
            this.PlayerVerticalMask = new System.Windows.Forms.PictureBox();
            this.PlayerFeet = new System.Windows.Forms.PictureBox();
            this.Score = new System.Windows.Forms.Label();
            this.HealthBar = new System.Windows.Forms.PictureBox();
            this.CustomBackground = new System.Windows.Forms.PictureBox();
            this.LevelEnd = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerHorizontalMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerMiddleMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerVerticalMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerFeet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HealthBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LevelEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // MainGameTimer
            // 
            this.MainGameTimer.Enabled = true;
            this.MainGameTimer.Interval = 1;
            this.MainGameTimer.Tick += new System.EventHandler(this.MainTickEvent);
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.Color.Black;
            this.Player.Location = new System.Drawing.Point(493, 532);
            this.Player.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(64, 64);
            this.Player.TabIndex = 71;
            this.Player.TabStop = false;
            this.Player.Tag = "Player";
            // 
            // PlayerHorizontalMask
            // 
            this.PlayerHorizontalMask.BackColor = System.Drawing.Color.Blue;
            this.PlayerHorizontalMask.Location = new System.Drawing.Point(600, 532);
            this.PlayerHorizontalMask.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayerHorizontalMask.Name = "PlayerHorizontalMask";
            this.PlayerHorizontalMask.Size = new System.Drawing.Size(64, 62);
            this.PlayerHorizontalMask.TabIndex = 72;
            this.PlayerHorizontalMask.TabStop = false;
            this.PlayerHorizontalMask.Tag = "Player";
            // 
            // PlayerMiddleMask
            // 
            this.PlayerMiddleMask.BackColor = System.Drawing.Color.Yellow;
            this.PlayerMiddleMask.Location = new System.Drawing.Point(602, 436);
            this.PlayerMiddleMask.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayerMiddleMask.Name = "PlayerMiddleMask";
            this.PlayerMiddleMask.Size = new System.Drawing.Size(62, 62);
            this.PlayerMiddleMask.TabIndex = 74;
            this.PlayerMiddleMask.TabStop = false;
            this.PlayerMiddleMask.Tag = "Player";
            // 
            // PlayerVerticalMask
            // 
            this.PlayerVerticalMask.BackColor = System.Drawing.Color.Red;
            this.PlayerVerticalMask.Location = new System.Drawing.Point(495, 436);
            this.PlayerVerticalMask.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayerVerticalMask.Name = "PlayerVerticalMask";
            this.PlayerVerticalMask.Size = new System.Drawing.Size(62, 64);
            this.PlayerVerticalMask.TabIndex = 75;
            this.PlayerVerticalMask.TabStop = false;
            this.PlayerVerticalMask.Tag = "Player";
            // 
            // PlayerFeet
            // 
            this.PlayerFeet.BackColor = System.Drawing.Color.Lime;
            this.PlayerFeet.Location = new System.Drawing.Point(764, 562);
            this.PlayerFeet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayerFeet.Name = "PlayerFeet";
            this.PlayerFeet.Size = new System.Drawing.Size(62, 32);
            this.PlayerFeet.TabIndex = 76;
            this.PlayerFeet.TabStop = false;
            this.PlayerFeet.Tag = "Player";
            // 
            // Score
            // 
            this.Score.AutoSize = true;
            this.Score.BackColor = System.Drawing.Color.Indigo;
            this.Score.Font = new System.Drawing.Font("Showcard Gothic", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Score.ForeColor = System.Drawing.Color.White;
            this.Score.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Score.Location = new System.Drawing.Point(12, 9);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(203, 53);
            this.Score.TabIndex = 106;
            this.Score.Tag = "Score";
            this.Score.Text = "Score: 0";
            // 
            // HealthBar
            // 
            this.HealthBar.BackColor = System.Drawing.Color.Black;
            this.HealthBar.Location = new System.Drawing.Point(1268, 12);
            this.HealthBar.Name = "HealthBar";
            this.HealthBar.Size = new System.Drawing.Size(80, 80);
            this.HealthBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HealthBar.TabIndex = 107;
            this.HealthBar.TabStop = false;
            this.HealthBar.Tag = "HealthBar";
            // 
            // CustomBackground
            // 
            this.CustomBackground.Location = new System.Drawing.Point(-1360, -2520);
            this.CustomBackground.Name = "CustomBackground";
            this.CustomBackground.Size = new System.Drawing.Size(8, 8);
            this.CustomBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.CustomBackground.TabIndex = 108;
            this.CustomBackground.TabStop = false;
            this.CustomBackground.Tag = "CustomBackground";
            // 
            // LevelEnd
            // 
            this.LevelEnd.BackColor = System.Drawing.Color.Purple;
            this.LevelEnd.Location = new System.Drawing.Point(587, -2200);
            this.LevelEnd.Name = "LevelEnd";
            this.LevelEnd.Size = new System.Drawing.Size(32, 32);
            this.LevelEnd.TabIndex = 109;
            this.LevelEnd.TabStop = false;
            this.LevelEnd.Tag = "Trigger";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(-1360, 624);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(4080, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 110;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "Ground";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(-1361, -2520);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(149, 3360);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 111;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "Ground";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Location = new System.Drawing.Point(2496, -2520);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(149, 3360);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 112;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Tag = "Ground";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            this.pictureBox4.Location = new System.Drawing.Point(-104, -1936);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1588, 2036);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 113;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Tag = "Ground";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Black;
            this.pictureBox5.Location = new System.Drawing.Point(-1360, -2520);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(4080, 20);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 114;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Tag = "Ground";
            // 
            // Level2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 840);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.LevelEnd);
            this.Controls.Add(this.CustomBackground);
            this.Controls.Add(this.HealthBar);
            this.Controls.Add(this.PlayerFeet);
            this.Controls.Add(this.PlayerVerticalMask);
            this.Controls.Add(this.PlayerMiddleMask);
            this.Controls.Add(this.PlayerHorizontalMask);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1360, 840);
            this.MinimumSize = new System.Drawing.Size(1360, 840);
            this.Name = "Level2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Level2";
            this.Load += new System.EventHandler(this.OnLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerHorizontalMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerMiddleMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerVerticalMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerFeet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HealthBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LevelEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer MainGameTimer;
        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.PictureBox PlayerHorizontalMask;
        private System.Windows.Forms.PictureBox PlayerMiddleMask;
        private System.Windows.Forms.PictureBox PlayerVerticalMask;
        private System.Windows.Forms.PictureBox PlayerFeet;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.PictureBox HealthBar;
        private System.Windows.Forms.PictureBox CustomBackground;
        private System.Windows.Forms.PictureBox LevelEnd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}