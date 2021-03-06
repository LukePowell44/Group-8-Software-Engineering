﻿using NewsAPI;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Group_Spotify_Architect
{
    public class NewsInterface
    {
        NewsApiClient webAPI;

        public NewsInterface()
        {
            webAPI = new NewsApiClient("fccd161d07d041cc9a9a8f8d011c30be");
        }

        public async Task<List<Article>> GetHeadlinesAsync(NewsAPI.Constants.Countries country, NewsAPI.Constants.Categories cat, int size = 25)
        {
            var output = new List<Article>();

            var tmp = new NewsAPI.Models.TopHeadlinesRequest { Country = country, Category = cat, Language = NewsAPI.Constants.Languages.EN, PageSize = size };

            var response = await webAPI.GetTopHeadlinesAsync(tmp);

            foreach (var article in response.Articles)
            {
                output.Add(new Article(article.Title, article.Description, article.Url));
            }

            return output;
        }
    }
}
