using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using TP2.Views;
using static TP2.Models.WeatherCondition;

namespace TP2.ViewModels
{
    public class WeatherPageViewModel : ViewModelBase
    {
        private string _region;

        private RootObject _weatherCondition;

        private IPageDialogService _pageDialogService;
        private INavigationService _navigationService;
        private IApiService _apiService;
        private bool _userConnected;
        private string _userName;
        private IFavoriteRegionListService _favoriteRegionListService;

        public DelegateCommand putInFavorite => new DelegateCommand(AddFavorite);

        public WeatherPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService apiService, IAuthentificationService authentificationService, IFavoriteRegionListService favoriteRegionListService)
            :base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            _apiService = apiService;
            _userConnected = authentificationService.IsUserAuthenticated;
            _userName = authentificationService.AuthenticatedUserName;
            _favoriteRegionListService = favoriteRegionListService;
        }

        public override void OnNavigatingTo(INavigationParameters param)
        {
            var region = param.GetValue<string>("region");
            Region = region;
            GetResponse();
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

        public RootObject WeatherCondition
        {
            get => _weatherCondition;

            set
            {
                _weatherCondition = value;
                RaisePropertyChanged();
            }
        }

        public void GetResponse()
        {
            string region = "&q=" + _region + "";
            SetWeatherCondition(region);
        }

        private async void SetWeatherCondition(string region)
        {
            try
            {
                var weatherConditionObject = await _apiService.GetLocationAsync(region);
                WeatherCondition = weatherConditionObject;
                WeatherCondition.current.condition.icon = "http:" + WeatherCondition.current.condition.icon;
            }
            catch
            {
                await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.WRONG_REGION, UiText.OK);
                await _navigationService.GoBackAsync();
            }
            
        }

        private async void AddFavorite()
        {
            if (_userConnected == false) await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.NEED_TO_BE_CONNECTED, UiText.OK);
            else
            {
                _favoriteRegionListService.AddRegion(_userName, new Region(_region));
                await _navigationService.NavigateAsync(nameof(FavoriteRegionPage));
            }
        }
    }
}
