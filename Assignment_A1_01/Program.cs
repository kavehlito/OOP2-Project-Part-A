// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
// Grupp Boule: Kaveh J, Louise L, Alexandra F, Josefine S 
using Assignment_A1_01.Services;
using System;
using System.Linq;

namespace Assignment_A1_01
{
    class Program
    {
        static void Main(string[] args)
        {
            double latitude = 59.61366453278328;
            double longitude = 17.83947069452774;

            var t1 = new OpenWeatherService().GetForecastAsync(latitude, longitude);


            //Your Code
            
            Console.WriteLine($"Väderprognos i {t1.Result.City}");
            foreach (var item in t1.Result.Items.GroupBy(dt => dt.DateTime.Date.ToShortDateString()))
            {
                Console.WriteLine(item.Key);
                foreach (var thing in item)
                {
                    Console.WriteLine($"   - {thing.DateTime.ToString("HH:mm")}: {thing.Description}: Temperatur: {thing.Temperature} °C, Vind: {thing.WindSpeed} m/s");
                }
            } 

        }
        static void ReportWeatherAvailable(object sender, (double longitude, double latitude)e)
        {
            Console.WriteLine($"New weather forecast for ({e.latitude}, {e.longitude} available");
        }
    }
}
