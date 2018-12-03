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
        private string _image;
        private bool _isFavoriteRemove;
        private bool _isFavoriteAdd;
        private IFavoriteRegionListService _favoriteRegionListService;

        public DelegateCommand FavoriteCommand => new DelegateCommand(Favorite);

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
            if (_favoriteRegionListService.CheckRegionInList(_userName, new Region(_region)))
            {
                Image = "etoile2.png";
            }
            else
            {
                Image = "etoile1.png";
            }
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

        public string Image
        {
            get => _image;

            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFavoriteRemove
        {
            get => _isFavoriteRemove;

            set
            {
                _isFavoriteRemove = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFavoriteAdd
        {
            get => _isFavoriteAdd;

            set
            {
                _isFavoriteAdd = value;
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

        private void Favorite()
        {
            if (_favoriteRegionListService.CheckRegionInList(_userName, new Region(_region)))
            {
                RemoveFavorite();
            }
            else
            {
                AddFavorite();
            }
        }

        private async void AddFavorite()
        {
            if (_userConnected == false) await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.NEED_TO_BE_CONNECTED, UiText.OK);
            else
            {
                _favoriteRegionListService.AddRegion(_userName, new Region(_region));
                await _navigationService.GoBackAsync();
                await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.REGION_ADDED, UiText.OK);
            }
        }

        private async void RemoveFavorite()
        {
            if (_userConnected == false) await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.NEED_TO_BE_CONNECTED, UiText.OK);
            else
            {
                _favoriteRegionListService.CheckRegionInList(_userName, new Region(_region));
                _favoriteRegionListService.RemoveRegion(_userName, new Region(_region));
                await _navigationService.GoBackAsync();
                await _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.REGION_REMOVED, UiText.OK);
            }
        }
    }
}
