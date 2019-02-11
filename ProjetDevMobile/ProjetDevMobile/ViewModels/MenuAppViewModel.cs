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
        public DelegateCommand NavigateNouvelleReviewCommand { get; private set; }
        public DelegateCommand NavigateListeReviewsCommand { get; private set; }

        public MenuAppViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateAccueilCommand = new DelegateCommand(ClickedAccueil);
            NavigateNouvelleReviewCommand = new DelegateCommand(ClickedNouvelleReview);
            NavigateListeReviewsCommand = new DelegateCommand(ClickedListeReviews);

        }

        private void ClickedAccueil()
        {
            _navigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private void ClickedNouvelleReview()
        {
            _navigationService.NavigateAsync("NavigationPage/NouvelleReviewPage");
        }

        private void ClickedListeReviews()
        {
            _navigationService.NavigateAsync("NavigationPage/ListeReviewsPage");
        }
    }
}
