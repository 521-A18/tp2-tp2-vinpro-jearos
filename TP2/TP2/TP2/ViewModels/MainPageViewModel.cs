using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
using TP2.Services.Interfaces;
using TP2.Views;

namespace TP2.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _region;

        private INavigationService _navigationService;
        private readonly IAuthentificationService _authentificationService;
        private readonly IPageDialogService _pageDialogService;
        private string _userName;
        private string _userPassword;

        public DelegateCommand SearchRegion => new DelegateCommand(Search);
        public DelegateCommand CreateAccount => new DelegateCommand(RegisterPage);

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IAuthentificationService authentificationService)
            : base(navigationService)
        {
            _authentificationService = authentificationService;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
        }

        public string Region
        {
            get => _region;
            set
            {
                _region = value;
                RaisePropertyChanged();
            }
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
                    await _navigationService.NavigateAsync(nameof(UserPage));
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR_LOGIN, UiText.OK);
                }
            }
            catch
            {
                await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR_LOGIN, UiText.OK);
            }
        }

        private async void Search()
        {
            try
            {
                if (Region == null || Region == "") { await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.WRONG_REGION, UiText.OK); }
                else
                {
                    var navigationParameter = new NavigationParameters
                    {
                        { "region", Region }
                    };
                    await _navigationService.NavigateAsync(nameof(WeatherPage), navigationParameter);
                }
            }
            catch
            {
                await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.ALERT_ERROR, UiText.OK);
            }
        }
        
        private async void RegisterPage()
        {
            await _navigationService.NavigateAsync(nameof(RegisterPage));
        }
    }
}
