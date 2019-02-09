using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMobile.ViewModels
{
	public class MenuAppViewModel : BindableBase
	{
        private INavigationService _navigationService;

        public DelegateCommand NavigateAccueilCommand { get; private set; }

        public MenuAppViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateAccueilCommand = new DelegateCommand(ClickedAccueil);

        }

        private void ClickedAccueil()
        {
            _navigationService.NavigateAsync("NavigationPage/MainPage");
        }

    }
}
