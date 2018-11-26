using Prism.Services;
using Moq;
using Prism.Navigation;
using TP2.ViewModels;
using Xunit;
using System;
using System.ComponentModel;
using TP2.Externalization;
using TP2.Views;

namespace TP2.UnitTests.ViewModel
{
    public class MainPageViewModelTest
    {
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<INavigationService> _mockNavigationService;
        private MainPageViewModel _mainPageViewModel;

        private bool _eventRaised = false;


        public MainPageViewModelTest()
        {
            _mockPageDialogService = new Mock<IPageDialogService>();
            _mockNavigationService = new Mock<INavigationService>();
            _mainPageViewModel = new MainPageViewModel(_mockNavigationService.Object, _mockPageDialogService.Object);
        }

        [Fact]
        public void Region_WhenSetNewValue_ShouldRaisePropertyChangedEvent()
        {
            _mainPageViewModel.PropertyChanged += RaiseProperty;

            _mainPageViewModel.Region = "quebec";

            Assert.True(_eventRaised);
        }

        [Fact]
        public void Search_whenRegionIsCorrect_ShouldNavigateToPage()
        {
            _mainPageViewModel.Region = "quebec";
            var navigationParameter = new NavigationParameters
            {
                { "region", "quebec" }
            };

            _mainPageViewModel.SearchRegion.Execute();

            _mockNavigationService.Verify(x => x.NavigateAsync(It.Is<string>(s => s.Contains(nameof(WeatherPage))), navigationParameter), Times.AtLeastOnce());
        }

        [Fact]
        public void Search_whenRegionIsIncorrect_ShouldNotNavigateToPage()
        {
            _mainPageViewModel.Region = null;
            var navigationParameter = new NavigationParameters
            {
                { "region", null }
            };

            _mainPageViewModel.SearchRegion.Execute();

            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void Search_whenRegionIsIncorrect_ShouldDisplayAlert()
        {
            _mainPageViewModel.Region = null;
            var navigationParameter = new NavigationParameters
            {
                { "region", null }
            };

            _mainPageViewModel.SearchRegion.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.WRONG_REGION, UiText.OK), Times.AtLeastOnce());
        }

        [Fact]
        public void Search_whenErrorHappens_ShouldDisplayAlert()
        {
            _mainPageViewModel.Region = "quebec";
            var navigationParameter = new NavigationParameters
            {
                { "region", "quebec" }
            };
            _mockNavigationService.Setup(x => x.NavigateAsync(It.Is<string>(s => s.Contains(nameof(WeatherPage))), navigationParameter)).Throws(new Exception());
            _mainPageViewModel.SearchRegion.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR, UiText.OK), Times.AtLeastOnce());
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
