// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
using Assignment_A2_02.Models;
using Assignment_A2_02.Services;
using System;
using System.Threading.Tasks;

namespace Assignment_A2_02
{
    class Program
    {
        static void Main(string[] args)
        {
            NewsService service = new NewsService();
            service.NewsAvailable += ReportNewsDataAvailable;
            Task<News> t = null;


            for (NewsCategory c = NewsCategory.business; c < NewsCategory.technology + 1; c++)
            {
                t = service.GetNewsAsync(c);
                Console.WriteLine($"News in {t.Result.Category}:");
                foreach (var item in t.Result.Articles)
                {
                    Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                }
                Console.WriteLine();
            }

            static void ReportNewsDataAvailable(object sender, string message)
            {
                Console.WriteLine($"Event message from weather service: {message}");
            }
        }
    }
}
