using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TP2.Models;

namespace TP2.ViewModels
{
    public class WeatherPageViewModel : BindableBase
    {
        private string _url = "http://api.apixu.com/v1/";
        private string _key = "current.json?key=2536facad072420089773603181210";
        private string _region = "&q=quebec";
        private string _language = "&lang=fr";
        private string _weatherConditionString;
        private WeatherCondition _weatherCondition;
        private HttpClient _client = new HttpClient();

        public WeatherPageViewModel()
        {
            GetResponse();
        }

        public WeatherCondition WeatherCondition
        {
            get => _weatherCondition;

            set
            {
                _weatherCondition = value;
                RaisePropertyChanged();
            }
        }

        public async void GetResponse()
        {
            await RunAsync();
            _weatherConditionString = await GetLocationAsync(_region, _language);
            SetWeatherCondition();
           
        }

        private async Task RunAsync()
        {
            // Update port # in the following line.
            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<string> GetLocationAsync(string region, string language)
        {
            string weatherCondition = null;
            HttpResponseMessage response = await _client.GetAsync(_key + region + language);
            if (response.IsSuccessStatusCode)
            {
                weatherCondition = await response.Content.ReadAsStringAsync();
            }
            return weatherCondition;
        }

        private void SetWeatherCondition()
        {
            WeatherCondition = JsonConvert.DeserializeObject<WeatherCondition>(_weatherConditionString);
            
        }
    }
}
