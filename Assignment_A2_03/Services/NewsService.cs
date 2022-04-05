

using Assignment_A2_03.Models;
using Assignment_A2_03.ModelsSampleData;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace Assignment_A2_03.Services
{
    public class NewsService
    {
        ConcurrentDictionary<(NewsCategory, string), News> _NewsCache = new ConcurrentDictionary<(NewsCategory, string), News>();

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
            News news1 = null;
            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            if (!_NewsCache.TryGetValue((category, date), out news1))
            {

                _NewsCache.TryAdd((category, date), news1);

                OnNewsAvailable($"News in category is available: {category}");
            }
            else OnNewsAvailable($"Cached in category is available: {category}");

#endif

            return news;
        }
    }
}
