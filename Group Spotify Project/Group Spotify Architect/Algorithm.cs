﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace Group_Spotify_Architect
{
    public class Algorithm
    {
        private SpotifyInterface spotify;
        private NewsInterface news;
        public Algorithm(SpotifyInterface Spotify, NewsInterface News)
        {
            spotify = Spotify;
            news = News;
        }

        public bool TooFewTracks { get; set; }
        public int TrackAmount { get; set; }

        public async Task<List<Song>> SearchPlaylistAsync(NewsAPI.Constants.Countries country, NewsAPI.Constants.Categories catagory, int size = 25)
        {
            int trackAmount = 0;
            var results = await news.GetHeadlinesAsync(country, catagory, 50);
                       
            List<string> headlineText = new List<string>();

            foreach (var item in results)
            {
                headlineText.Add(item.Title);
            }

            List<Song> playlist = new List<Song>();
            
            for (int x = 0; x < headlineText.Count; x++)
            {
                var result = headlineText[x];
                var splitResult = result.Split(' ');
                result = result.Split(' ').FirstOrDefault().Trim();
                for (int y = 0; y < splitResult.Length; y++)
                {
                    if (splitResult[y].Length > 3)
                    {
                        result = splitResult[y];
                        break;
                    }
                }
                
                var songOutput = await spotify.GetSongsAsync(result, 1);

                try
                {
                    playlist.Add(songOutput[0]);
                    trackAmount++;
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }

                if (trackAmount == size)
                {
                    break;
                }                
            }

            TooFewTracks = (trackAmount < size);
            TrackAmount = trackAmount;

            return playlist;
        }
    }
}
