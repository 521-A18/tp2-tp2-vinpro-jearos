using System.Threading.Tasks;
using Newtonsoft.Json;
using TP2.Services.Interfaces;
using static TP2.Models.WeatherCondition;

namespace TP2.Services
{
    public class ApiService : IApiService
    {
        private string _url = "http://api.apixu.com/v1/";
        private string _key = "current.json?key=2536facad072420089773603181210";
        private string _language = "&lang=fr";

        private IHttpClient _client;

        public ApiService(IHttpClient httpClient)
        {
            _client = httpClient;
            GetUrl();
        }

        public void GetUrl()
        {
            _client.GetUrl(_url);
        }

        public async Task<RootObject> GetLocationAsync(string region)
        {
            string location = await _client.GetLocationAsync(_key, region, _language);
            var weatherConditionObject = JsonConvert.DeserializeObject<RootObject>(location.ToString());
            return weatherConditionObject;
        }
    }
}
