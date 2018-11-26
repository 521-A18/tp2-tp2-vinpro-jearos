using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TP2.Services.Interfaces;

namespace TP2.Services
{
    public class HttpClientService : IHttpClient
    {
        private HttpClient _httpClient = new HttpClient();

        public void GetUrl(string url)
        {
            // Update port # in the following line.
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetLocationAsync(string key, string region, string language)
        {
            string weatherCondition = null;
            HttpResponseMessage response = await _httpClient.GetAsync(key + region + language);
            if (response.IsSuccessStatusCode)
            {
                weatherCondition = await response.Content.ReadAsStringAsync();
            }
            return weatherCondition;
        }

    }
}
