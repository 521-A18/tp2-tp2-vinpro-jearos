using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
using TP2.Views;

namespace TP2.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _region;

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        public DelegateCommand SearchRegion => new DelegateCommand(Search);
        public DelegateCommand CreateAccount => new DelegateCommand(RegisterPage);
        public DelegateCommand LoginAccount => new DelegateCommand(LoginPage);

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
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

        private async void LoginPage()
        {
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }
    }
}
