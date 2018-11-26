using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
