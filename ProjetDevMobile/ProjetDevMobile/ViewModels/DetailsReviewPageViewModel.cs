using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetDevMobile.ViewModels
{
	public class DetailsReviewPageViewModel : ViewModelBase
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

        public DetailsReviewPageViewModel(INavigationService navigationService): base(navigationService)
        {

        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            Review review = parameters.GetValue<Review>("review");

            Titre = review.Titre;
            Description = review.Description;
            Tag = review.Tag;
        }

    }
}
