using System.ComponentModel;
using System.Linq;
using Moq;
using Prism.Navigation;
using TP2.Externalization;
using TP2.UnitTests.Constante;
using TP2.Validations;
using TP2.ViewModels;
using Xunit;

namespace TP2.UnitTests.ViewModel
{
    public class RegisterPageViewModelTest
    {
        private readonly RegisterPageViewModel _registerPageViewModels;
        private Mock<INavigationService> _mockNavigationService;
        private bool _eventRaised = false;

        public RegisterPageViewModelTest()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _registerPageViewModels = new RegisterPageViewModel(_mockNavigationService.Object);
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

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
