using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Model;
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

        public DetailsReviewPageViewModel(INavigationService navigationService): base(navigationService)
        {

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

    }
}
