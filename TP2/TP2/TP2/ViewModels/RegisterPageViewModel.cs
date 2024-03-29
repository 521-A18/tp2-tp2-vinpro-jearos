﻿using Prism.Navigation;
using TP2.Validations;
using Prism.Commands;
using TP2.Validations.Rules;
using TP2.Externalization;
using Prism.Services;
using TP2.Views;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using System.Linq;

namespace TP2.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IRegisterService _registerService;
        private readonly IAuthentificationService _authentificationService;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _passwordConfirm;

        public DelegateCommand ExecuteUserPage => new DelegateCommand(UserPageNavigation);
        public DelegateCommand ExecuteValidateEmail => new DelegateCommand(ValidateEmail);
        public DelegateCommand ExecuteValidatePassword => new DelegateCommand(ValidatePassword);
        public DelegateCommand ExecuteValidatePasswordConfirm => new DelegateCommand(ValidatePasswordConfirm);

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IRegisterService registerService, IAuthentificationService authentificationService)
            :base(navigationService)
        {
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _passwordConfirm = new ValidatableObject<string>();

            AddValidations();

            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _registerService = registerService;
            _authentificationService = authentificationService;
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

        public void UserPageNavigation()
        {
            try
            {
                _email.Validate();
                _password.Validate();
                _passwordConfirm.Validate();
                if (Password.Value != PasswordConfirm.Value) _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.PASSWORD_AND_CONFIRM_ARE_DIFFERENT, UiText.OK);
                else if (_registerService.CheckUser(Email.Value)) _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.EMAIL_ALREADY_EXIST, UiText.OK);
                else if (Email.IsValid && Password.IsValid && PasswordConfirm.IsValid)
                {
                    _registerService.RegisterUser(Email.Value, Password.Value);
                    _authentificationService.LogIn(Email.Value, Password.Value);
                    _navigationService.NavigateAsync("/NavigationPage/UserPage");
                }
                else _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.ELEMENT_ARE_INVALIDE, UiText.OK);
                
            }
            catch
            {
                _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR, UiText.OK);
            }
            
        }

        public void ValidateEmail()
        {
            _email.Validate();
        }

        public void ValidatePassword()
        {
            _password.Validate();
        }

        public void ValidatePasswordConfirm()
        {
            _passwordConfirm.Validate();
        }
    }
}