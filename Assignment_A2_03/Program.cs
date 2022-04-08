// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
using System;
using System.Threading.Tasks;

using Assignment_A2_03.Models;
using Assignment_A2_03.Services;

namespace Assignment_A2_03
{
    class Program
    {
        static void Main(string[] args)
        {
            NewsService service = new NewsService();
            service.NewsAvailable += ReportNewsDataAvailable;
            
            Task<News> t1 = null, t2 = null;

            for (NewsCategory c = NewsCategory.business; c < NewsCategory.technology + 1; c++)
            {
                t1 = service.GetNewsAsync(c);
                Console.WriteLine($"News in {t1.Result.Category}:");
                foreach (var item in t1.Result.Articles)
                {
                    Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                }
                Console.WriteLine();
            }

            for (NewsCategory c = NewsCategory.business; c < NewsCategory.technology + 1; c++)
            {
                t2 = service.GetNewsAsync(c);
                Console.WriteLine($"News in {t2.Result.Category}:");
                foreach (var item in t2.Result.Articles)
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