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

            /*
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
            */

            Task<News> t1 = null, t2 = null, t3 = null, t4 = null, t5 = null, t6 = null, t7 = null;
            Exception exception = null;

            try
            {
                t1 = service.GetNewsAsync(NewsCategory.business);
                t1.Wait();
                t2 = service.GetNewsAsync(NewsCategory.entertainment);
                t2.Wait();
                t3 = service.GetNewsAsync(NewsCategory.general);
                t3.Wait();
                t4 = service.GetNewsAsync(NewsCategory.health);
                t4.Wait();
                t5 = service.GetNewsAsync(NewsCategory.science);
                t5.Wait();
                t6 = service.GetNewsAsync(NewsCategory.sports);    
                t6.Wait();
                t7 = service.GetNewsAsync(NewsCategory.technology);
                t7.Wait();

                t1 = service.GetNewsAsync(NewsCategory.business);
                t1.Wait();
                t2 = service.GetNewsAsync(NewsCategory.entertainment);
                t2.Wait();
                t3 = service.GetNewsAsync(NewsCategory.general);
                t3.Wait();
                t4 = service.GetNewsAsync(NewsCategory.health);
                t4.Wait();
                t5 = service.GetNewsAsync(NewsCategory.science);
                t5.Wait();
                t6 = service.GetNewsAsync(NewsCategory.sports);
                t6.Wait();
                t7 = service.GetNewsAsync(NewsCategory.technology);
                t7.Wait();

            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Console.WriteLine("-----------------");
            if (t1?.Status == TaskStatus.RanToCompletion)
            {
                News news = t1.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }

            Console.WriteLine("-----------------");
            if (t2?.Status == TaskStatus.RanToCompletion)
            {
                News news = t2.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t3?.Status == TaskStatus.RanToCompletion)
            {
                News news = t3.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t4?.Status == TaskStatus.RanToCompletion)
            {
                News news = t4.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t5?.Status == TaskStatus.RanToCompletion)
            {
                News news = t5.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t6?.Status == TaskStatus.RanToCompletion)
            {
                News news = t6.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t7?.Status == TaskStatus.RanToCompletion)
            {
                News news = t7.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }

            Console.WriteLine("-----------------");
            if (t1?.Status == TaskStatus.RanToCompletion)
            {
                News news = t1.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }

            Console.WriteLine("-----------------");
            if (t2?.Status == TaskStatus.RanToCompletion)
            {
                News news = t2.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t3?.Status == TaskStatus.RanToCompletion)
            {
                News news = t3.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t4?.Status == TaskStatus.RanToCompletion)
            {
                News news = t4.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t5?.Status == TaskStatus.RanToCompletion)
            {
                News news = t5.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t6?.Status == TaskStatus.RanToCompletion)
            {
                News news = t6.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine("-----------------");
            if (t7?.Status == TaskStatus.RanToCompletion)
            {
                News news = t7.Result;
                Console.WriteLine($"Weather forecast for {news.Category}");
                var GroupedList = news.Articles.GroupBy(item => item.DateTime.Date);
                foreach (var group in GroupedList)
                {
                    Console.WriteLine(group.Key.Date.ToShortDateString());
                    foreach (var item in group)
                    {
                        Console.WriteLine($"   - {item.DateTime}: {item.Title}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geolocation weather service error.");
                Console.WriteLine($"Error: {exception.Message}");
            }

            static void ReportNewsDataAvailable(object sender, string message)
            {
                Console.WriteLine($"Event message from weather service: {message}");
            }


        }
    }
}
