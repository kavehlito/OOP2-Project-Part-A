

using Assignment_A2_02.Models;
using Assignment_A2_02.ModelsSampleData;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Net.Http.Json;

namespace Assignment_A2_02.Services
{
    public class NewsService
    {
        
        HttpClient httpClient = new HttpClient();
        readonly string apiKey = "a389335d7a5044a49c01cb03ab5a9267";

        public event EventHandler<string> NewsAvailable;

        protected virtual void OnNewsAvailable(string e)
        {
            NewsAvailable?.Invoke(this, e);
        }
        public async Task<News> GetNewsAsync(NewsCategory category)
        {
#if UseNewsApiSample      
            NewsApiData nd = await NewsApiSampleData.GetNewsApiSampleAsync(category);
            

#else
            //https://newsapi.org/docs/endpoints/top-headlines
            var uri = $"https://newsapi.org/v2/top-headlines?country=se&category={category}&apiKey={apiKey}";

           // Your code here to get live data
           HttpResponseMessage response = await httpClient.GetAsync(uri);
           response.EnsureSuccessStatusCode();
           NewsApiData nd = await response.Content.ReadFromJsonAsync<NewsApiData>();
            News news = new News();
            try
            {
                news.Category = category;
                news.Articles = nd.Articles.Select(item => new NewsItem
                {
                    DateTime = item.PublishedAt,
                    Title = item.Title,
                    Description = item.Description,
                    Url = item.Url,
                    UrlToImage = item.UrlToImage
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            OnNewsAvailable($"News in category is available: {category}");
#endif

            return news;
        }
    }
}
