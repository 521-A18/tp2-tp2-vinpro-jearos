using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TP2.Services;
using TP2.Services.Interfaces;
using Xunit;
using static TP2.Models.WeatherCondition;

namespace TP2.UnitTests.Services
{
    public class ApiServiceTest
    {
        private Mock<IHttpClient> _IHttpClientMock;
        private IApiService _apiService;

        public ApiServiceTest()
        {
            _IHttpClientMock = new Mock<IHttpClient>();
            _apiService = new ApiService(_IHttpClientMock.Object);
        }

        [Fact]
        public async void GetLocationAsync_WhenCalled_ShouldReturnRootObjectAsync()
        {
            _IHttpClientMock.Setup(x => x.GetLocationAsync("current.json?key=2536facad072420089773603181210", "quebec", "&lang=fr")).ReturnsAsync("{\"location\":{\"name\":\"Quebec\",\"region\":\"Quebec\",\"country\":\"Canada\",\"lat\":46.8,\"lon\":-71.25,\"tz_id\":\"America/Toronto\",\"localtime_epoch\":1543253958,\"localtime\":\"2018-11-26 12:39\"},\"current\":{\"last_updated_epoch\":1543252510,\"last_updated\":\"2018-11-26 12:15\",\"temp_c\":0.6,\"temp_f\":33.1,\"is_day\":1,\"condition\":{\"text\":\"Ensoleillé\",\"icon\":\"//cdn.apixu.com/weather/64x64/day/113.png\",\"code\":1000},\"wind_mph\":11.9,\"wind_kph\":19.1,\"wind_degree\":50,\"wind_dir\":\"NE\",\"pressure_mb\":1015.0,\"pressure_in\":30.4,\"precip_mm\":0.1,\"precip_in\":0.0,\"humidity\":85,\"cloud\":0,\"feelslike_c\":-4.4,\"feelslike_f\":24.2,\"vis_km\":15.3,\"vis_miles\":9.0,\"uv\":2.0}}");

            RootObject response = await _apiService.GetLocationAsync("quebec");

            Assert.Equal("Quebec", response.location.name);
        }
    }
}
