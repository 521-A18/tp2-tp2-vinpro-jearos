using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using TP2.Externalization;
using TP2.Services.Interfaces;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests.ViewModel
{
    public class UserPageViewModelTest
    {

        private Mock<INavigationService> _mockNavigationService;
        private Mock<IAuthentificationService> _mockAuthentificationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private UserPageViewModel _userPageViewModel;

        private bool _eventRaised = false;

        public UserPageViewModelTest()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockAuthentificationService = new Mock<IAuthentificationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _userPageViewModel = new UserPageViewModel(_mockNavigationService.Object, _mockAuthentificationService.Object, _mockPageDialogService.Object);
        }

        [Fact]
        public void UserName_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _userPageViewModel.PropertyChanged += RaiseProperty;

            _userPageViewModel.UserName = "test";

            Assert.True(_eventRaised);
        }

        [Fact]
        public void Search_whenRegionIsCorrect_ShouldNavigateToPage()
        {
            _userPageViewModel.Region = "quebec";
            var navigationParameter = new NavigationParameters
            {
                { "region", "quebec" }
            };

            _userPageViewModel.SearchRegion.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync(It.Is<string>(s => s.Contains(nameof(WeatherPage))), navigationParameter), Times.AtLeastOnce());
        }

        [Fact]
        public void Search_whenRegionIsIncorrect_ShouldNotNavigateToPage()
        {
            _userPageViewModel.Region = null;
            var navigationParameter = new NavigationParameters
            {
                { "region", null }
            };

            _userPageViewModel.SearchRegion.Execute();

            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void Search_whenRegionIsIncorrect_ShouldDisplayAlert()
        {
            _userPageViewModel.Region = null;
            var navigationParameter = new NavigationParameters
            {
                { "region", null }
            };

            _userPageViewModel.SearchRegion.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.WRONG_REGION, UiText.OK), Times.AtLeastOnce());
        }

        [Fact]
        public void Search_whenErrorHappens_ShouldDisplayAlert()
        {
            _userPageViewModel.Region = "quebec";
            var navigationParameter = new NavigationParameters
            {
                { "region", "quebec" }
            };
            _mockNavigationService.Setup(x => x.NavigateAsync(It.Is<string>(s => s.Contains(nameof(WeatherPage))), navigationParameter)).Throws(new Exception());
            _userPageViewModel.SearchRegion.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR, UiText.OK), Times.AtLeastOnce());
        }

        [Fact]
        public void LogOut_whenClicked_ShouldNavigateToMainPage()
        {
            _userPageViewModel.LogoutCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("/NavigationPage/MainPage"), Times.AtLeastOnce());
        }

        [Fact]
        public void FavoriteRegion_whenClicked_ShouldNavigateToFavoriteRegionPage()
        {
            _userPageViewModel.FavoriteRegionCommand.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync("FavoriteRegionPage"), Times.AtLeastOnce());
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
