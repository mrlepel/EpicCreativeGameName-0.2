using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using EpicCreativeGameName;
using System.Diagnostics;

namespace EpicCreativeGameName_0._2
{
    public partial class mainMenu : Form
    {
        string[] menuitems = { "Start", "help", "quit" };
        int selected;

        string CurrentlySelected;
        bool Trigger = true;

        SoundPlayer MenuMusic = new SoundPlayer("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Sound_and_Music/Patakas World.wav");
        SoundPlayer helpSong = new SoundPlayer("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Sound_and_Music/help.wav");

        public mainMenu()
        {
            InitializeComponent();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (Trigger)
            {
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.W))
                {
                    Trigger = false;
                    selected--;
                    if (selected < 0)
                    {
                        selected = 2;
                    }
                    UpdateMenu();
                }

                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.S))
                {
                    Trigger = false;
                    selected++;
                    if (selected > 2)
                    {
                        selected = 0;
                    }
                    UpdateMenu();
                }
            }
            if ((e.KeyCode == Keys.Z) || (e.KeyCode == Keys.Space))
            {
                switch (CurrentlySelected)
                {
                    case "Start":
                        EpicCreativeGameName.EpicCreativeGameName.menu = true;
                        EpicCreativeGameName.EpicCreativeGameName lv0 = new EpicCreativeGameName.EpicCreativeGameName();
                        lv0.Show();
                        this.Hide();
                        break;
                    case "help":
                        pictureBox1.Image = Image.FromFile("C:\\Users/Riemer/Documents/EpicCreativeGameName-0.2/Resources/Epic_Creative_Game_Name_Sprites/rick-astley.png");
                        helpSong.Play();
                        break;
                    case "quit":
                        Application.Exit();
                        break;
                }
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.W))
            {
                Trigger = true;
            }

            if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.S))
            {
                Trigger = true;
            }
        }

        private void MenuLoad(object sender, EventArgs e)
        {
            MenuMusic.PlayLooping();
            UpdateMenu();
        }

        private void UpdateMenu()
        {
            CurrentlySelected = menuitems[selected];
            foreach (Control x in this.Controls)
            {
                if (x is Label && (string)x.Name == CurrentlySelected)
                {
                    x.ForeColor = Color.Yellow;
                }
                else
                {
                    x.ForeColor = Color.Black;
                }
            }
        }

    }
}
