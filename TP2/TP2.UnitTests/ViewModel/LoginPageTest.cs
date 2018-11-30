using TP2.Services;
using TP2.ViewModels;
using Moq;
using Prism.Services;
using System;
using Xunit;
using TP2.Services.Interfaces;
using Prism.Navigation;
using TP2.Externalization;
using TP2.Views;

namespace TP2.UnitTests.ViewModel
{
    public class LoginPageTest
    {
        private readonly LoginPageViewModel _loginPageViewModel;
        private Mock<INavigationService> _navigationService;
        private Mock<IAuthentificationService> _authServiceMock;
        private Mock<IPageDialogService> _pageDialogMock;

        public LoginPageTest()
        {
            _navigationService = new Mock<INavigationService>();
            _authServiceMock = new Mock<IAuthentificationService>();
            _pageDialogMock = new Mock<IPageDialogService>();
            _loginPageViewModel = new LoginPageViewModel(_navigationService.Object, _authServiceMock.Object, _pageDialogMock.Object);
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsNotValid_ShouldNotNavigate()
        {
            _authServiceMock.Setup(auth => auth.IsUserAuthenticated).Returns(false);
            _loginPageViewModel.AuthenticateCommand.Execute();

            _navigationService.Verify(x => x.NavigateAsync("/" + nameof(Views.FavoriteRegionPage)), Times.Never);
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsNotValid_ShouldDisplayAlertToUser()
        {
            _authServiceMock.Setup(auth => auth.IsUserAuthenticated).Returns(false);
            _loginPageViewModel.AuthenticateCommand.Execute();

            _pageDialogMock.Verify(dialog => dialog.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR_LOGIN, UiText.OK), Times.Once);
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsValid_ShouldNavigateToFavoriteRegionPage()
        {
            _authServiceMock.Setup(auth => auth.IsUserAuthenticated).Returns(true);
            _loginPageViewModel.AuthenticateCommand.Execute();

            _navigationService.Verify(x => x.NavigateAsync(nameof(FavoriteRegionPage)), Times.Once);
        }

        [Fact]
        public void AuthenticateCommand_OnException_ShouldDisplayAlertToUser()
        {
            _authServiceMock.Setup(auth => auth.LogIn(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            _loginPageViewModel.AuthenticateCommand.Execute();

            _pageDialogMock.Verify(dialog => dialog.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR_LOGIN, UiText.OK), Times.Once);
        }
    }
}
