using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

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
	}
}
