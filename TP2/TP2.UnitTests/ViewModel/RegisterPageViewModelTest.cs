using System;
using System.ComponentModel;
using System.Linq;
using Moq;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Services.Interfaces;
using TP2.UnitTests.Constante;
using TP2.Validations;
using TP2.ViewModels;
using TP2.Views;
using Xunit;

namespace TP2.UnitTests.ViewModel
{
    public class RegisterPageViewModelTest
    {
        private readonly RegisterPageViewModel _registerPageViewModels;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private Mock<IRepository<User>> _mockRepository;
        private ICryptoService _cryptoService;
        private Mock<ISecureStorageService> _mockSecureStorageService;
        private Mock<IRegisterService> _mockRegisterService;

        private bool _eventRaised = false;

        public RegisterPageViewModelTest()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockPageDialogService = new Mock<IPageDialogService>();
            _cryptoService = new CryptoService();
            _mockRepository = new Mock<IRepository<User>>();
            _mockSecureStorageService = new Mock<ISecureStorageService>();
            _mockRegisterService = new Mock<IRegisterService>();

            _registerPageViewModels = new RegisterPageViewModel(_mockNavigationService.Object, _mockPageDialogService.Object, _cryptoService, _mockRepository.Object, _mockSecureStorageService.Object, _mockRegisterService.Object);
        }

        [Fact]
        public void Email_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _registerPageViewModels.PropertyChanged += RaiseProperty;

            _registerPageViewModels.Email = new ValidatableObject<string>();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void Password_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _registerPageViewModels.PropertyChanged += RaiseProperty;

            _registerPageViewModels.Password = new ValidatableObject<string>();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void PasswordConfirm_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _registerPageViewModels.PropertyChanged += RaiseProperty;

            _registerPageViewModels.PasswordConfirm = new ValidatableObject<string>();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void ValidateEmailCommand_WithBadEmail_ShouldValidateEmailValidatableObject()
        {

            _registerPageViewModels.Email.Value = ConstanteTest.BAD_EMAIL;

            _registerPageViewModels.ExecuteUserPage.Execute();

            Assert.False(_registerPageViewModels.Email.IsValid);
        }

        [Fact]
        public void ValidatePasswordCommand_WithBadPassword_ShouldValidatePasswordValidatableObject()
        {

            _registerPageViewModels.Password.Value = ConstanteTest.BAD_PASSWORD;

            _registerPageViewModels.ExecuteUserPage.Execute();

            Assert.False(_registerPageViewModels.Password.IsValid);
        }

        [Fact]
        public void ValidatePasswordConfirmCommand_WithBadPasswordConfirm_ShouldValidatePasswordConfirmValidatableObject()
        {
            _registerPageViewModels.Password.Value = ConstanteTest.GOOD_PASSWORD;
            _registerPageViewModels.PasswordConfirm.Value = ConstanteTest.BAD_PASSWORD;

            _registerPageViewModels.ExecuteUserPage.Execute();

            Assert.False(_registerPageViewModels.PasswordConfirm.IsValid);
        }

        [Fact]
        public void ValidateEmailCommand_WithBadEmail_ShouldAddErrorToEmailValidatableObject()
        {
            _registerPageViewModels.Email.Value = ConstanteTest.BAD_EMAIL;

            _registerPageViewModels.ExecuteUserPage.Execute();

            var firstErrorMessage = _registerPageViewModels.Email.Errors.ElementAt(0);

            Assert.Equal(UiText.EMAIL_IS_INCORRECT, firstErrorMessage);
        }

        [Fact]
        public void ValidatePassWordCommand_WithBadPasswordNoLowerCase_ShouldAddErrorToPasswordValidatableObject()
        {
            _registerPageViewModels.Password.Value = ConstanteTest.BAD_PASSWORD;

            _registerPageViewModels.ExecuteUserPage.Execute();

            var firstErrorMessage = _registerPageViewModels.Password.Errors.ElementAt(0);
            Assert.Equal(UiText.PASSWORD_NEED_ONE_NUMBER, firstErrorMessage);
        }

        [Fact]
        public void UserPageNavigation_WhenEmailIsInvalidAndPasswordAndConfirmAreValid_ShouldNotNavigate()
        {
            _registerPageViewModels.Email.Value = ConstanteTest.BAD_EMAIL;
            _registerPageViewModels.Password.Value = ConstanteTest.GOOD_PASSWORD;
            _registerPageViewModels.PasswordConfirm.Value = ConstanteTest.GOOD_PASSWORD_CONFIRM;

            _registerPageViewModels.ExecuteUserPage.Execute();

            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void UserPageNavigation_WhenEmailAndPasswordAreValidAndConfirmIsInvalid_ShouldDisplayAlert()
        {
            _registerPageViewModels.Email.Value = ConstanteTest.GOOD_EMAIL;
            _registerPageViewModels.Password.Value = ConstanteTest.GOOD_PASSWORD;
            _registerPageViewModels.PasswordConfirm.Value = ConstanteTest.BAD_PASSWORD_CONFIRM;

            _registerPageViewModels.ExecuteUserPage.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.PASSWORD_AND_CONFIRM_ARE_DIFFERENT, UiText.OK), Times.AtLeastOnce());
        }

        [Fact]
        public void UserPageNavigation_WhenEmailIsInvalid_ShouldDisplayAlert()
        {
            _registerPageViewModels.Email.Value = ConstanteTest.BAD_EMAIL;
            _registerPageViewModels.Password.Value = ConstanteTest.GOOD_PASSWORD;
            _registerPageViewModels.PasswordConfirm.Value = ConstanteTest.GOOD_PASSWORD;

            _registerPageViewModels.ExecuteUserPage.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.ELEMENT_ARE_INVALIDE, UiText.OK), Times.AtLeastOnce());
        }

        [Fact]
        public void UserPageNavigation_WhenEmailIsValidAndPasswordIsInvalid_ShouldNotNavigate()
        {
            _registerPageViewModels.Email.Value = ConstanteTest.GOOD_EMAIL;
            _registerPageViewModels.Password.Value = ConstanteTest.BAD_PASSWORD;
            _registerPageViewModels.PasswordConfirm.Value = ConstanteTest.BAD_PASSWORD;

            _registerPageViewModels.ExecuteUserPage.Execute();

            _mockNavigationService.VerifyNoOtherCalls();
        }

        [Fact]
        public void UserPageNavigation_WhenEmailAndPasswordAndConfirmAreValidButSomethingHappen_ShouldDisplayAlert()
        {
            _registerPageViewModels.Email.Value = ConstanteTest.GOOD_EMAIL;
            _registerPageViewModels.Password.Value = ConstanteTest.GOOD_PASSWORD;
            _registerPageViewModels.PasswordConfirm.Value = ConstanteTest.GOOD_PASSWORD_CONFIRM;

            _mockNavigationService.Setup(x => x.NavigateAsync(It.Is<string>(s => s.Contains(nameof(MainPage))))).Throws(new Exception());

            _registerPageViewModels.ExecuteUserPage.Execute();

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR, UiText.OK), Times.AtLeastOnce());
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
