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
        private ObservableCollection<Region> _favoriteRegionList;
        public FavoriteRegionPageViewModel(INavigationService navigationService, IFavoriteRegionListService favoriteRegionListService, IAuthentificationService authentificationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            FavoriteRegionList = new ObservableCollection<Region>(favoriteRegionListService.GetFavoriteRegionList(authentificationService.AuthenticatedUserName));
        }

        public DelegateCommand<Region> WeatherRegionPageCommand => new DelegateCommand<Region>(WeatherRegionPage);

        public ObservableCollection<Region> FavoriteRegionList
        {
            get => _favoriteRegionList;
            set
            {
                _favoriteRegionList = value;
                RaisePropertyChanged();
            }
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
