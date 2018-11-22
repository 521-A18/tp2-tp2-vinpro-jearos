using Prism.Services;
using Moq;
using Prism.Navigation;
using TP2.ViewModels;
using Xunit;
using System;
using System.ComponentModel;
using TP2.Externalization;

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

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
