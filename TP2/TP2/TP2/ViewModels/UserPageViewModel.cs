using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TP2.Externalization;
using TP2.Services.Interfaces;
using TP2.Views;

namespace TP2.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        private IAuthentificationService _authentificationService;
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        private string _userName;
        private string _region;

        public UserPageViewModel(INavigationService navigationService, IAuthentificationService authentificationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _authentificationService = authentificationService;
            _pageDialogService = pageDialogService;
            UserName = _authentificationService.AuthenticatedUserName;
        }

        public DelegateCommand LogoutCommand => new DelegateCommand(Logout);
        public DelegateCommand FavoriteRegionCommand => new DelegateCommand(FavoriteRegion);
        public DelegateCommand SearchRegion => new DelegateCommand(Search);

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
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

        private async void Logout()
        {
            await _navigationService.GoBackToRootAsync();
        }

        private async void FavoriteRegion()
        {
            await _navigationService.NavigateAsync(nameof(FavoriteRegionPage));
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
    }
}
