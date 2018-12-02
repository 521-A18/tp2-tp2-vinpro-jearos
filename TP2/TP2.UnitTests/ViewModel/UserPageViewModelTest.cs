using Moq;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using TP2.Services.Interfaces;
using TP2.ViewModels;
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

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
