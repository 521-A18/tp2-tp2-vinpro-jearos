using Prism.Mvvm;
using Prism.Navigation;
using TP2.Validations;

namespace TP2.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _passwordConfirm;

        public RegisterPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            _navigationService = navigationService;
        }

        public ValidatableObject<string> Email
        {
            get; set;
            //get => _email;
            //set 
            //{
            //    _email = value;
            //    RaisePropertyChanged();
            //}
        }

        public ValidatableObject<string> Password
        {
            get; set;
            //get => _password;
            //set
            //{
            //    _password = value;
            //    RaisePropertyChanged();
            //}
        }

        public ValidatableObject<string> PasswordConfirm
        {
            get; set;
            //get => _passwordConfirm;
            //set
            //{
            //    _passwordConfirm = value;
            //    RaisePropertyChanged();
            //}
        }
    }
}
