using Assignment_A1_01.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json; //Requires nuget package System.Net.Http.Json
using System.Threading.Tasks;


namespace Assignment_A1_01.Services
{
    public class OpenWeatherService
    {
        HttpClient httpClient = new HttpClient();
        readonly string apiKey = "06108d4761e05b311b258326f90ec128"; // Your API Key
        public async Task<Forecast> GetForecastAsync(double latitude, double longitude)
        {
            //https://openweathermap.org/current
            var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var uri = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&lang={language}&appid={apiKey}";

            //Read the response from the WebApi
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            WeatherApiData wd = await response.Content.ReadFromJsonAsync<WeatherApiData>();

            //Your Code to convert WeatherApiData to Forecast using Linq.

            Forecast forecast = new Forecast();

            try
            {
                forecast.City = wd.city.name;
                forecast.Items = wd.list.Select(item => new ForecastItem
                {
                    DateTime = UnixTimeStampToDateTime(item.dt),
                    Temperature = item.main.temp,
                    WindSpeed = item.wind.speed,
                    Description = item.weather.Select(desc => desc.description).FirstOrDefault().ToString(),
                    Icon = item.weather.Select(item => item.icon).ToString()
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }


            return forecast;
        }
        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
