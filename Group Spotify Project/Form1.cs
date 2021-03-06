﻿using System;
using System.Windows.Forms;

namespace Group_Spotify_Architect
{
    public partial class Form1 : Form
    {
        private SpotifyInterface spotify;
        private NewsInterface news;
        public Form1()
        {
            InitializeComponent();
            spotify = new SpotifyInterface();
            news = new NewsInterface();
            try
            {
                spotify.Connect();
                lb_connected.Text = "Connected";
            }
            catch (Exception err)
            {
                lb_connected.Text = $"Error: {err.Message}";
            }

            cb_catagory.DataSource = Enum.GetValues(typeof(NewsAPI.Constants.Categories));
            cb_country.DataSource = Enum.GetValues(typeof(NewsAPI.Constants.Countries));
            cb_country.SelectedItem = NewsAPI.Constants.Countries.GB;
        }


        private async void Bt_searchPlaylist_Click(object sender, EventArgs e)
        {
            Algorithm algorithm = new Algorithm(ref spotify, ref news);
            NewsAPI.Constants.Countries cont;
            Enum.TryParse<NewsAPI.Constants.Countries>(cb_country.SelectedValue.ToString(), out cont);
            NewsAPI.Constants.Categories cat;
            Enum.TryParse<NewsAPI.Constants.Categories>(cb_country.SelectedValue.ToString(), out cat);
            var search = await algorithm.SearchPlaylist(cont, cat, Convert.ToInt32(numericUpDown1.Value));
            lb_spotify.Items.Clear();
            for (int x = 0; x < search.Count; x++)
            {
                lb_spotify.Items.Add(search[x]);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (numericUpDown1.Value > tB_SongAmount.Maximum)
                {
                    numericUpDown1.Value = tB_SongAmount.Maximum;
                }

                if (numericUpDown1.Value < tB_SongAmount.Minimum)
                {
                    numericUpDown1.Value = tB_SongAmount.Minimum;
                }
            }
            catch
            {

                throw;
            }
        }

        private void tB_SongAmount_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = tB_SongAmount.Value;
        }

        private void lb_spotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lb_spotify.SelectedItem != null)
            {
                try
                {
                    Song tmp = (Song)lb_spotify.SelectedItem;
                    System.Diagnostics.Process.Start(tmp.Link);
                }
                catch {}
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
