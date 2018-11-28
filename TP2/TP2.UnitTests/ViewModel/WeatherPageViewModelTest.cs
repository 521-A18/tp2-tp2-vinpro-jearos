using System;
using System.ComponentModel;
using Moq;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
using TP2.Services.Interfaces;
using TP2.ViewModels;
using Xunit;
using static TP2.Models.WeatherCondition;

namespace TP2.UnitTests.ViewModel
{
    public class WeatherPageViewModelTest
    {

        public Mock<IPageDialogService> _pageDialogServiceMock;
        public Mock<INavigationService> _navigationServiceMock;
        public Mock<IApiService> _apiServiceMock;
        public WeatherPageViewModel _weatherPageViewModel;

        public bool _eventRaised = false;
        public RootObject _rootObject;

        public WeatherPageViewModelTest()
        {
            _pageDialogServiceMock = new Mock<IPageDialogService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _apiServiceMock = new Mock<IApiService>();
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object, _apiServiceMock.Object);
        }

        [Fact]
        public void Region_WhenSetNewValue_ShouldRaisePropertyChangedEvent()
        {
            _weatherPageViewModel.PropertyChanged += RaiseProperty;

            _weatherPageViewModel.Region = "quebec";

            Assert.True(_eventRaised);
        }

        [Fact]
        public void WeatherCondition_WhenSetNewValue_ShouldRaisePropertyChangedEvent()
        {
            _weatherPageViewModel.PropertyChanged += RaiseProperty;

            _weatherPageViewModel.WeatherCondition = new RootObject();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void SetWeatherCondition_WhenApiCallIsCorrect_ShouldDisplayInformation()
        {
            RootObject rootObject = new RootObject();
            rootObject.location = new Location();
            rootObject.location.name = "quebec";
            rootObject.current = new Current();
            rootObject.current.condition = new Condition();
            rootObject.current.condition.icon = "test";

            _weatherPageViewModel.Region = "quebec";
            _apiServiceMock.Setup(x => x.GetLocationAsync("&q=quebec")).ReturnsAsync(rootObject);

            _weatherPageViewModel.GetResponse();

            Assert.Equal(rootObject.location.name, _weatherPageViewModel.WeatherCondition.location.name);
        }

        [Fact]
        public void SetWeatherCondition_WhenApiCallIsIncorrect_ShouldDisplayAlert()
        {
            _apiServiceMock.Setup(x => x.GetLocationAsync("")).Throws(new Exception());

            _weatherPageViewModel.GetResponse();

            _pageDialogServiceMock.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.WRONG_REGION, UiText.OK));
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
