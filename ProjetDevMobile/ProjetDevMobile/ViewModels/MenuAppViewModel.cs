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
        public DelegateCommand NavigateMapCommand { get; private set; }
        public DelegateCommand NavigateNouvelleReviewCommand { get; private set; }
        public DelegateCommand NavigateListeReviewsCommand { get; private set; }

        public MenuAppViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateAccueilCommand = new DelegateCommand(ClickedAccueil);
            NavigateMapCommand = new DelegateCommand(ClickedMap);
            NavigateNouvelleReviewCommand = new DelegateCommand(ClickedNouvelleReview);
            NavigateListeReviewsCommand = new DelegateCommand(ClickedListeReviews);
        }

        private void ClickedAccueil()
        {
            _navigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private void ClickedMap()
        {
            _navigationService.NavigateAsync("NavigationPage/MapPage");
        }

        private void ClickedNouvelleReview()
        {
            NavigationParameters navigationParam = new NavigationParameters();
            navigationParam.Add("mode", true);
            
            _navigationService.NavigateAsync("NavigationPage/EditeurReviewPage", navigationParam);
        }

        private void ClickedListeReviews()
        {
            _navigationService.NavigateAsync("NavigationPage/ListeReviewsPage");
        }
    }
}
