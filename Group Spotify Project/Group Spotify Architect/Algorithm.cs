﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group_Spotify_Architect
{
    class Algorithm
    {
        private SpotifyInterface spotify;
        private NewsInterface news;
        public Algorithm(ref SpotifyInterface Spotify, ref NewsInterface News)
        {
            spotify = Spotify;
            news = News;
        }

        public async Task<Song> SearchSong(NewsAPI.Constants.Countries country, NewsAPI.Constants.Categories catagory, int size = 25)
        {
            var results = await news.GetHeadlines(country, catagory, size);

            List<string> headlineText = new List<string>();

            foreach (var item in results)
            {
                headlineText.Add(item.Title);
            }

            Random r = new Random();

            foreach (var x in headlineText)
            {
                Console.WriteLine(x);
            }
            var result = headlineText[r.Next(0, headlineText.Count)];
            result = result.Split(' ').FirstOrDefault().Trim();
            Console.WriteLine(result);
            var songOutput = await spotify.GetSongs(result, 1);

            return songOutput.FirstOrDefault();

        }

        public async Task<List<Song>> SearchPlaylist(NewsAPI.Constants.Countries country, NewsAPI.Constants.Categories catagory, int size = 25)
        {
            int trackAmount = 0;
            var results = await news.GetHeadlines(country, catagory, size);



            List<string> headlineText = new List<string>();

            foreach (var item in results)
            {
                headlineText.Add(item.Title);
            }

            List<Song> playlist = new List<Song>();


            for (int x = 0; x < headlineText.Count; x++)
            {
                var result = headlineText[x];
                result = result.Split(' ').FirstOrDefault().Trim();

                var songOutput = await spotify.GetSongs(result, 1);

                try
                {
                    playlist.Add(songOutput[0]);
                    trackAmount++;
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }



            }

            MessageBox.Show("Unfortunately we could only find " + trackAmount + " songs related to this news!", "Title", MessageBoxButtons.OK, MessageBoxIcon.Error);


            return playlist;

        }

    }
}
