using Assignment_A2_04.Models;
using Assignment_A2_04.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_A2_04
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
