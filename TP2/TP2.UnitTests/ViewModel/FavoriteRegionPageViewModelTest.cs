using Moq;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using TP2.ViewModels;
using Xunit;

namespace TP2.UnitTests.ViewModel
{
    public class FavoriteRegionPageViewModelTest
    {
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IAuthentificationService> _authServiceMock;
        private Mock<IFavoriteRegionListService> _favoriteRegionListServiceMock;
        private FavoriteRegionPageViewModel _favoriteRegionPageViewModel;

        private bool _eventRaised = false;

        public FavoriteRegionPageViewModelTest()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _authServiceMock = new Mock<IAuthentificationService>();
            _favoriteRegionListServiceMock = new Mock<IFavoriteRegionListService>();
            _authServiceMock.Setup(x => x.AuthenticatedUserName).Returns("test");
            _favoriteRegionListServiceMock.Setup(x => x.GetFavoriteRegionList("test")).Returns(new Collection<Region>());
            _favoriteRegionPageViewModel = new FavoriteRegionPageViewModel(_mockNavigationService.Object, _favoriteRegionListServiceMock.Object, _authServiceMock.Object);
        }

        [Fact]
        public void FavoriteRegionList_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _favoriteRegionPageViewModel.PropertyChanged += RaiseProperty;

            _favoriteRegionPageViewModel.FavoriteRegionList = new ObservableCollection<Region>(new Collection<Region>());

            Assert.True(_eventRaised);
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
