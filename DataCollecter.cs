using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace SimpleWeather
{
    class DataCollecter
    {
        const string BASE_URL = "http://api.openweathermap.org/data/2.5/weather";
        const string API_KEY = "9a323a7c25146fd05911355bb08b74d5";

        public async static Task<string> GetData(string city)
        {
            string completeUrl = BASE_URL + "?q=" + city + "&appid=" + API_KEY + "&units=metric";
            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(completeUrl);
            return response;
        }

        public static Weather ParseJSON(string response)
        {
            Weather report = new Weather();
            JObject weatherData = JObject.Parse(response);
            report.City = weatherData["name"].ToString();
            report.Forecast = weatherData["weather"][0]["description"].ToString();
            report.Temperature = weatherData["main"]["temp"] + " C";
            report.WindSpeed = weatherData["wind"]["speed"] + " km/h";
            report.Humidity = weatherData["main"]["humidity"] + "%";
            report.Sunrise = weatherData["sys"]["sunrise"].ToString();
            report.Sunset = weatherData["sys"]["sunset"].ToString();
            return report;
        }

        public static string ConvertToProperCase(string str)
        {

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string formatted = textInfo.ToTitleCase(str);
            return formatted;
        }
    }
}
