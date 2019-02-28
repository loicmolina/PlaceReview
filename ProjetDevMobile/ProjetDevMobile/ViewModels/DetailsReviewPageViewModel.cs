using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Model;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProjetDevMobile.ViewModels
{
	public class DetailsReviewPageViewModel : ViewModelBase
	{
        private ReviewDisplay _review;
        public ReviewDisplay Review
        {
            get { return _review; }
            set { SetProperty(ref _review, value); }
        }

        private ImageSource _imageButtonSupprimer;
        public ImageSource ImageButtonSupprimer
        {
            get { return _imageButtonSupprimer; }
            set { SetProperty(ref _imageButtonSupprimer, value); }
        }

        public DelegateCommand SupprimerCommand { get; private set; }
        private IReviewService _reviewService { get; set; }

        public DetailsReviewPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            _reviewService = reviewService;
            SupprimerCommand = new DelegateCommand(SupprimerReview);
        }

        private void SupprimerReview()
        {
            PopUpValiderSuppression();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            ReviewDisplay review = parameters.GetValue<ReviewDisplay>("review");
            Review = new ReviewDisplay(review.Titre, review.Description, review.Tag)
            {
                Photo = review.Photo
            };
        }

        async void PopUpValiderSuppression()
        {            
            var answer = await App.Current.MainPage.DisplayAlert("Suppression", "Êtes-vous sûr de vouloir supprimer l'enregistrement ? Cette action n'est pas reversible", "Oui", "Non");

            if (answer.Equals("Yes"))
            {
                //TODO : supprimer review
            }
        }

    }
}
