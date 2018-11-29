using Prism.Mvvm;
using Prism.Navigation;
using TP2.Validations;
using Prism.Commands;
using System;

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

        private static void UserPageNavigation()
        {
            throw new NotImplementedException();
        }
    }
}
