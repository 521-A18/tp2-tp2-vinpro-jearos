using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TP2.Views;

namespace TP2.ViewModels
{
	public class FavoriteRegionPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public FavoriteRegionPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand LogoutCommand => new DelegateCommand(Logout);

        private async void Logout()
        {
            await _navigationService.GoBackToRootAsync();
        }
    }
}
