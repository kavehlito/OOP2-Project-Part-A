// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
using Assignment_A2_01.Services;
using System;
using System.Threading.Tasks;

namespace Assignment_A2_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = new NewsService().GetNewsAsync();

            //Your Code

            Console.WriteLine($"Top Headlines:");
            foreach (var item in t1.Result.Articles)
            {
                    Console.WriteLine($"   - {item.Title}");
            }
        }
    }
}