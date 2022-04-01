#define UseNewsApiSample  // Remove or undefine to use your own code to read live data

using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json; //Requires nuget package System.Net.Http.Json
using System.Collections.Concurrent;
using System.Threading.Tasks;

using Assignment_A2_04.Models;
using Assignment_A2_04.ModelsSampleData;

namespace Assignment_A2_04.Services
{
    public class NewsService
    {
        HttpClient httpClient = new HttpClient();
        readonly string apiKey = "d318329c40734776a014f9d9513e14ae";

        public event EventHandler<string> NewsAvailable;

        protected virtual void OnNewsAvailable(string e)
        {
            NewsAvailable?.Invoke(this, e);
        }
        public async Task<News> GetNewsAsync(NewsCategory category)
        {

#if UseNewsApiSample      
            NewsApiData nd = await NewsApiSampleData.GetNewsApiSampleAsync(category);
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
            var date = DateTime.UtcNow;


            NewsCacheKey cache = new NewsCacheKey(category, date);
            if (!cache.CacheExist)
            {
                News.Serialize(news, "test.xml");
                NewsCacheKey.Serialize(news, cache.FileName);
                OnNewsAvailable($"News in category is available: {category}");
            }
            else
            {
                NewsCacheKey.Deserialize(cache.FileName);
                OnNewsAvailable($"XML Cached news in category is available: {category}");
            }
            
            


#else
            //https://newsapi.org/docs/endpoints/top-headlines
            var uri = $"https://newsapi.org/v2/top-headlines?country=se&category={category}&apiKey={apiKey}";

            // your code to get live data
#endif
            return news;
        }
    }
}
