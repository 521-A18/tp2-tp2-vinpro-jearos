using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Moq;
using Prism.Navigation;
using Prism.Services;
using TP2.Models;
using TP2.ViewModels;
using Xunit;
using static TP2.Models.WeatherCondition;

namespace TP2.UnitTests.ViewModel
{
    public class WeatherPageViewModelTest
    {

        public Mock<IPageDialogService> _pageDialogServiceMock;
        public Mock<INavigationService> _navigationServiceMock;
        public WeatherPageViewModel _weatherPageViewModel;

        public bool _eventRaised = false;
        public RootObject _rootObject;

        public WeatherPageViewModelTest()
        {
            _pageDialogServiceMock = new Mock<IPageDialogService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _weatherPageViewModel = new WeatherPageViewModel(_navigationServiceMock.Object, _pageDialogServiceMock.Object);
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

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
