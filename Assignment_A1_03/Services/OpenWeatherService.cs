using Assignment_A1_03.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json; //Requires nuget package System.Net.Http.Json
using System.Threading.Tasks;

namespace Assignment_A1_03.Services
{
    public class OpenWeatherService
    {
        ConcurrentDictionary<(string, string), Forecast> _Cityforecastcache = new ConcurrentDictionary<(string, string), Forecast>();
        ConcurrentDictionary<(double, double, string), Forecast> _Coordsforecastcache = new ConcurrentDictionary<(double, double, string), Forecast>();
        
        HttpClient httpClient = new HttpClient();
        readonly string apiKey = "06108d4761e05b311b258326f90ec128"; // Your API Key


        public event EventHandler<string> WeatherForecastAvailable;

        protected virtual void OnWeatherAvailable(string e)
        {
            WeatherForecastAvailable?.Invoke(this, e);
        }

        public async Task<Forecast> GetForecastAsync(string City)
        {

            Forecast forecast;

            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (!_Cityforecastcache.TryGetValue((City, date), out forecast))
            {
                //https://openweathermap.org/current
                var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                var uri = $"https://api.openweathermap.org/data/2.5/forecast?q={City}&units=metric&lang={language}&appid={apiKey}";
                forecast = await ReadWebApiAsync(uri);

                _Cityforecastcache.TryAdd((City, date), forecast);

                OnWeatherAvailable($"New weather forecast for {City} available");
            }
            else OnWeatherAvailable($"Cached weather forecast for {City} available");
            
            return forecast;

        }
        public async Task<Forecast> GetForecastAsync(double latitude, double longitude)
        {

            Forecast forecast;

            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (!_Coordsforecastcache.TryGetValue((latitude, longitude, date), out forecast))
            {
                //https://openweathermap.org/current
                var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                var uri = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&lang={language}&appid={apiKey}";


                forecast = await ReadWebApiAsync(uri);
                _Coordsforecastcache.TryAdd((latitude, longitude, date), forecast);
                OnWeatherAvailable($"New weather forecast for ({latitude}, {longitude}) available");

            }
            else OnWeatherAvailable($"Cached weather forecast for ({latitude}, {longitude}) available");


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
            //generate an event with different message if cached data

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
