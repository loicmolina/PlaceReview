using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Navigation;
using ProjetDevMobile.Model;
using ProjetDevMobile.Services;

namespace ProjetDevMobile.ViewModels
{
    public class NouvelleReviewPageViewModel : ViewModelBase
	{
        private string _titre;
        public string Titre
        {
            get { return _titre; }
            set { SetProperty(ref _titre, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        private List<string> _typesReview;
        public List<string> TypesReview
        {
            get { return _typesReview; }
            set { SetProperty(ref _typesReview, value); }
        }

        public DelegateCommand ValiderCommand { get; private set; }
        private IReviewService _reviewService { get; set; }

        public NouvelleReviewPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            ValiderCommand = new DelegateCommand(Enregistrer);
            _reviewService = reviewService;
            TypesReview = new List<string>();
            TypesReview.AddRange(Enum.GetNames(typeof(ReviewTypes)));
            
        }

        private void Enregistrer()
        {
            _reviewService.AddReview(new Review(Titre, Description, Tag));
            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }
    }
}
