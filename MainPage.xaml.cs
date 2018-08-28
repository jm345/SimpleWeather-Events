using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace SimpleWeather
{
	public partial class MainPage : ContentPage
	{
        const string BASE_URL = "http://api.openweathermap.org/data/2.5/weather";
        const string API_KEY = "9a323a7c25146fd05911355bb08b74d5";

        public MainPage()
		{
			InitializeComponent();
		}

        private async void GetWeather_Clicked(object sender, EventArgs e)
        {
            string response = await DataCollecter.GetData(userCity.Text);
            //Weather report = DataCollecter.ParseJSON(response);
            //UpdateUI(report);
        }

        private void UpdateUI(Weather report)
        {
            City.Text = report.City;
            Forecast.Text = report.Forecast;
            Temperature.Text = report.Temperature;
            WindSpeed.Text = report.WindSpeed;
            Humidity.Text = report.Humidity;
            Sunrise.Text = report.Sunrise;
            Sunset.Text = report.Sunset;
        }

        private Weather FormatWeather(dynamic weatherData)
        {
            Weather report = new Weather();
            report.City = weatherData["name"];
            report.Forecast = weatherData["weather"][0]["description"].ToString();
            report.Temperature = weatherData["main"]["temp"] + " C";
            report.WindSpeed = weatherData["wind"]["speed"] + " km/h";
            report.Humidity = weatherData["main"]["humidity"] + "%";
            report.Sunrise = weatherData["sys"]["sunrise"];
            report.Sunset = weatherData["sys"]["sunset"];
            return report;
        }
    }
}
