using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using TP2.Views;

namespace TP2.ViewModels
{
	public class FavoriteRegionPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IFavoriteRegionListService _favoriteRegionListService;
        private IAuthentificationService _authentificationService;
        private ObservableCollection<Region> _favoriteRegionList;
        public FavoriteRegionPageViewModel(INavigationService navigationService, IFavoriteRegionListService favoriteRegionListService, IAuthentificationService authentificationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _favoriteRegionListService = favoriteRegionListService;
            _authentificationService = authentificationService;
        }

        public DelegateCommand<Region> WeatherRegionPageCommand => new DelegateCommand<Region>(WeatherRegionPage);
        public DelegateCommand GoBackCommand => new DelegateCommand(GoBack);

        public ObservableCollection<Region> FavoriteRegionList
        {
            get => _favoriteRegionList;
            set
            {
                _favoriteRegionList = value;
                RaisePropertyChanged();
            }
        }

        public override void OnNavigatingTo(INavigationParameters param)
        {
            FavoriteRegionList = new ObservableCollection<Region>(_favoriteRegionListService.GetFavoriteRegionList(_authentificationService.AuthenticatedUserName));
        }

        private async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }

        private async void WeatherRegionPage(Region region)
        {
            var navigationParameter = new NavigationParameters
                    {
                        { "region", region.Name }
                    };
            await _navigationService.NavigateAsync(nameof(WeatherPage), navigationParameter);
        }
    }
}
