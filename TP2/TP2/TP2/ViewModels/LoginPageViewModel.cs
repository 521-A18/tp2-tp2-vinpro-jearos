using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
using TP2.Services.Interfaces;
using TP2.Views;

namespace TP2.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthentificationService _authentificationService;
        private readonly IPageDialogService _dialogService;
        private string _userName;
        private string _userPassword;

        public LoginPageViewModel(INavigationService navigationService, IAuthentificationService authentificationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Login Page";
            _navigationService = navigationService;
            _authentificationService = authentificationService;
            _dialogService = dialogService;
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        public string UserPassword
        {
            get => _userPassword;
            set
            {
                _userPassword = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand AuthenticateCommand => new DelegateCommand(AuthenticateCommandViewModel);

        public async void AuthenticateCommandViewModel()
        {
            try
            {
                _authentificationService.LogIn(UserName, UserPassword);
                if (_authentificationService.IsUserAuthenticated == true)
                {
                    await _navigationService.NavigateAsync(nameof(FavoriteRegionPage));
                }
                else
                {
                    await _dialogService.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR_LOGIN, UiText.OK);
                }
            }
            catch
            {
                await _dialogService.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR_LOGIN, UiText.OK);
            }
        }
    }
}
