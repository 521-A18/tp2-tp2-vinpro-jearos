using Prism.Mvvm;
using Prism.Navigation;
using TP2.Validations;
using Prism.Commands;
using System;
using TP2.Validations.Rules;
using TP2.Externalization;

namespace TP2.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _passwordConfirm;

        public DelegateCommand ExecuteUserPage = new DelegateCommand(UserPageNavigation);

        

        public RegisterPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            _navigationService = navigationService;
        }

        public ValidatableObject<string> Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public ValidatableObject<string> PasswordConfirm
        {
            get => _passwordConfirm;
            set
            {
                _passwordConfirm = value;
                RaisePropertyChanged();
            }
        }

        private void AddValidations()
        {
            var IsValidEmail = new IsValidEmail<string>
            {
                ErrorMessage = UiText.EMAIL_IS_INCORRECT
            };
            _email.AddValidationRule(IsValidEmail);

            var HasAtleastOneCapCharacter = new HasAtleastOneCapCharacter<string>
            {
                ErrorMessage = UiText.PASSWORD_NEED_CAP_LETTER
            };
            _password.AddValidationRule(HasAtleastOneCapCharacter);
            _passwordConfirm.AddValidationRule(HasAtleastOneCapCharacter);

            var HasMoreThanTenCharacters = new HasMoreThanTenCharacters<string>
            {
                ErrorMessage = UiText.PASSWORD_NEED_TEN_CHARACTER
            };
            _password.AddValidationRule(HasMoreThanTenCharacters);
            _passwordConfirm.AddValidationRule(HasMoreThanTenCharacters);

            var HasAtleastOneLowercaseCharacter = new HasAtleastOneLowercaseCharacter<string>
            {
                ErrorMessage = UiText.PASSWORD_NEED_LOWERCASE_LETTER
            };
            _password.AddValidationRule(HasAtleastOneLowercaseCharacter);
            _passwordConfirm.AddValidationRule(HasAtleastOneLowercaseCharacter);

            var HasAtleastOneNumber = new HasAtleastOneNumber<string>
            {
                ErrorMessage = UiText.PASSWORD_NEED_ONE_NUMBER
            };
            _password.AddValidationRule(HasAtleastOneNumber);
            _passwordConfirm.AddValidationRule(HasAtleastOneNumber);
        }

        private static void UserPageNavigation()
        {
            throw new NotImplementedException();
        }
    }
}
