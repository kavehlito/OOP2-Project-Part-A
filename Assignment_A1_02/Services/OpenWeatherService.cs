using Assignment_A1_02.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http.Json;

namespace Assignment_A1_02.Services
{
    public class OpenWeatherService
    {
        HttpClient httpClient = new HttpClient();
        readonly string apiKey = "06108d4761e05b311b258326f90ec128"; // Your API Key

        public event EventHandler<string> WeatherForecastAvailable;
        
        protected virtual void OnWeatherAvailable(string e)
        {
            WeatherForecastAvailable?.Invoke(this, e);
        }
        
        //part of your event code here
        public async Task<Forecast> GetForecastAsync(string City)
        {
            //https://openweathermap.org/current
            var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var uri = $"https://api.openweathermap.org/data/2.5/forecast?q={City}&units=metric&lang={language}&appid={apiKey}";

            Forecast forecast = await ReadWebApiAsync(uri);

            //part of your event code here
            OnWeatherAvailable($"New weather forecast for {City} available");
            return forecast;

        }
        public async Task<Forecast> GetForecastAsync(double latitude, double longitude)
        {
            //https://openweathermap.org/current
            var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var uri = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&lang={language}&appid={apiKey}";

            Forecast forecast = await ReadWebApiAsync(uri);

            //part of your event code here
            OnWeatherAvailable($"New weather forecast for ({latitude}, {longitude})");
            return forecast;
        }
        private async Task<Forecast> ReadWebApiAsync(string uri)
        {
            // part of your read web api code here
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            WeatherApiData wd = await response.Content.ReadFromJsonAsync<WeatherApiData>();

            // part of your data transformation to Forecast here
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
