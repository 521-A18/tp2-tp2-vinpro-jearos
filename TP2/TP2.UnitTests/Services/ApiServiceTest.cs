using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TP2.Services.Interfaces;
using Xunit;

namespace TP2.UnitTests.Services
{
    public class ApiServiceTest
    {
        private Mock<IHttpClient> _IHttpClientMock;
        private IApiService _apiService;

        public ApiServiceTest()
        {
            _IHttpClientMock = new Mock<IHttpClient>();
        }

        [Fact]
        public void GetLocationAsync_WhenCalled_ShouldReturnRootObject()
        {
            
        }
    }
}
