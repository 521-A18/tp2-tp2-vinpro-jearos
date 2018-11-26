using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static TP2.Models.WeatherCondition;

namespace TP2.Services.Interfaces
{
    public interface IApiService
    {
        void GetUrl();
        Task<RootObject> GetLocationAsync(string region);
    }
}
