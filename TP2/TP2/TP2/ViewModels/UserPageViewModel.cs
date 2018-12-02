using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TP2.Services.Interfaces;
using TP2.Views;

namespace TP2.ViewModels
{
	public class UserPageViewModel : ViewModelBase
	{
        private IAuthentificationService _authentificationService;
        private INavigationService _navigationService;
        private string _userName;
        public UserPageViewModel(INavigationService navigationService, IAuthentificationService authentificationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _authentificationService = authentificationService;
            UserName = _authentificationService.AuthenticatedUserName;
        }

        public DelegateCommand LogoutCommand => new DelegateCommand(Logout);
        public DelegateCommand FavoriteRegionCommand => new DelegateCommand(FavoriteRegion);

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
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
    }
}
