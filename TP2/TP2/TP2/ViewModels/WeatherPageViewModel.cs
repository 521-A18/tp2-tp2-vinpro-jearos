using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TP2.Models;
using static TP2.Models.WeatherCondition;

namespace TP2.ViewModels
{
    public class WeatherPageViewModel : ViewModelBase
    {
        private string _url = "http://api.apixu.com/v1/";
        private string _key = "current.json?key=2536facad072420089773603181210";
        private string _region;
        private string _language = "&lang=fr";
        private string _weatherConditionString;
        private RootObject _weatherCondition;
        private HttpClient _client = new HttpClient();

        public WeatherPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
        }

        public override async void OnNavigatingTo(INavigationParameters param)
        {
            var region = param.GetValue<string>("region");
            Region = region;
            GetResponse();
            var icon = WeatherCondition.current.cloud;
        }

        public string Region
        {
            get => _region;

            set
            {
                _region = value;
                RaisePropertyChanged();
            }
        }

        public RootObject WeatherCondition
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
            Run();
            string region = "&q=" + _region + "";
            _weatherConditionString = await GetLocationAsync(region, _language);
            SetWeatherCondition();
           
        }

        private void Run()
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
            var weatherConditionObject = JsonConvert.DeserializeObject<RootObject>(_weatherConditionString);
            WeatherCondition = weatherConditionObject;
        }

        
    }
}
