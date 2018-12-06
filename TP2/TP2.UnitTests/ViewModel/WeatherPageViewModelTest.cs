using System;
using System.ComponentModel;
using Moq;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using TP2.ViewModels;
using TP2.Views;
using Xunit;
using static TP2.Models.WeatherCondition;

namespace TP2.UnitTests.ViewModel
{
    public class WeatherPageViewModelTest
    {

        public Mock<IPageDialogService> _pageDialogServiceMock;
        public Mock<INavigationService> _navigationServiceMock;
        public Mock<IApiService> _apiServiceMock;
        public Mock<IFavoriteRegionListService> _mockFavoriteRegionListService;
        public Mock<IAuthentificationService> _mockAuthentificationService;
        public WeatherPageViewModel _weatherPageViewModel;

        public bool _eventRaised = false;
        public bool _isConnected = true;
        public RootObject _rootObject;

        public WeatherPageViewModelTest()
        {
            _pageDialogServiceMock = new Mock<IPageDialogService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _mockAuthentificationService = new Mock<IAuthentificationService>();
            _mockFavoriteRegionListService = new Mock<IFavoriteRegionListService>();
            _apiServiceMock = new Mock<IApiService>();
            _mockAuthentificationService.Setup(x => x.AuthenticatedUserName).Returns("test");
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object, _apiServiceMock.Object, _mockAuthentificationService.Object, _mockFavoriteRegionListService.Object);
        }

        [Fact]
        public void Region_WhenSetNewValue_ShouldRaisePropertyChangedEvent()
        {
            _weatherPageViewModel.PropertyChanged += RaiseProperty;

            _weatherPageViewModel.Region = "quebec";

            Assert.True(_eventRaised);
        }

        [Fact]
        public void IsFavoriteAdd_WhenSetNewValue_ShouldRaisePropertyChangedEvent()
        {
            _weatherPageViewModel.PropertyChanged += RaiseProperty;

            _weatherPageViewModel.IsFavoriteAdd = true;

            Assert.True(_eventRaised);
        }

        [Fact]
        public void IsFavoriteRemove_WhenSetNewValue_ShouldRaisePropertyChangedEvent()
        {
            _weatherPageViewModel.PropertyChanged += RaiseProperty;

            _weatherPageViewModel.IsFavoriteRemove = true;

            Assert.True(_eventRaised);
        }

        [Fact]
        public void Image_WhenSetNewValue_ShouldRaisePropertyChangedEvent()
        {
            _weatherPageViewModel.PropertyChanged += RaiseProperty;

            _weatherPageViewModel.Image = "quebec";

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

        [Fact]
        public void AddRegion_WhenWhenUserIsNotConnected_ShouldDisplayAlert()
        {
            _mockAuthentificationService.Setup(x => x.IsUserAuthenticated).Returns(false);
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object, _apiServiceMock.Object, _mockAuthentificationService.Object, _mockFavoriteRegionListService.Object);

            _weatherPageViewModel.FavoriteCommand.Execute();

           _pageDialogServiceMock.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.NEED_TO_BE_CONNECTED, UiText.OK));
        }

        [Fact]
        public void AddRegion_WhenUserIsConnectedAndIsNotInList_ShouldDisplayAlertAddFavorite()
        {
            _mockAuthentificationService.Setup(x => x.IsUserAuthenticated).Returns(true);
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object, _apiServiceMock.Object, _mockAuthentificationService.Object, _mockFavoriteRegionListService.Object);
            _mockFavoriteRegionListService.Setup(x => x.CheckRegionInList("test", It.IsAny<Region>())).Returns(false);

            _weatherPageViewModel.FavoriteCommand.Execute();

            _pageDialogServiceMock.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.REGION_ADDED, UiText.OK));
        }

        [Fact]
        public void RemoveRegion_WhenWhenUserIsConnectedAndIsInList_ShouldDisplayAlertRemoveFavorite()
        {
            _mockAuthentificationService.Setup(x => x.IsUserAuthenticated).Returns(true);
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object, _apiServiceMock.Object, _mockAuthentificationService.Object, _mockFavoriteRegionListService.Object);
            _mockFavoriteRegionListService.Setup(x => x.CheckRegionInList("test", It.IsAny<Region>())).Returns(true);

            _weatherPageViewModel.FavoriteCommand.Execute();

            _pageDialogServiceMock.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.REGION_REMOVED, UiText.OK));
        }

        [Fact]
        public void AddRegion_WhenUserIsConnectedAndIsNotInList_ShouldNavigateBack()
        {
            _mockAuthentificationService.Setup(x => x.IsUserAuthenticated).Returns(true);
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object, _apiServiceMock.Object, _mockAuthentificationService.Object, _mockFavoriteRegionListService.Object);
            _mockFavoriteRegionListService.Setup(x => x.CheckRegionInList("test", It.IsAny<Region>())).Returns(false);

            _weatherPageViewModel.FavoriteCommand.Execute();

            _navigationServiceMock.Verify(x => x.GoBackAsync(), Times.AtLeastOnce);
        }

        [Fact]
        public void RemoveRegion_WhenWhenUserIsConnectedAndIsInList_ShouldNavigateBack()
        {
            _mockAuthentificationService.Setup(x => x.IsUserAuthenticated).Returns(true);
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object, _apiServiceMock.Object, _mockAuthentificationService.Object, _mockFavoriteRegionListService.Object);
            _mockFavoriteRegionListService.Setup(x => x.CheckRegionInList("test", It.IsAny<Region>())).Returns(true);

            _weatherPageViewModel.FavoriteCommand.Execute();

            _navigationServiceMock.Verify(x => x.GoBackAsync(), Times.AtLeastOnce);
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
