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
        private ReviewDisplay _reviewD;
        public ReviewDisplay ReviewD
        {
            get { return _reviewD; }
            set { SetProperty(ref _reviewD, value); }
        }

        private ImageSource _imageButtonSupprimer;
        public ImageSource ImageButtonSupprimer
        {
            get { return _imageButtonSupprimer; }
            set { SetProperty(ref _imageButtonSupprimer, value); }
        }

        public DelegateCommand SupprimerCommand { get; private set; }

        private ImageSource _imageButtonModifier;
        public ImageSource ImageButtonModifier
        {
            get { return _imageButtonModifier; }
            set { SetProperty(ref _imageButtonModifier, value); }
        }

        public DelegateCommand ModifierCommand { get; private set; }

        private IReviewService _reviewService { get; set; }

        public DetailsReviewPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            _reviewService = reviewService;
            SupprimerCommand = new DelegateCommand(SupprimerReview);
            ModifierCommand = new DelegateCommand(ModifierReview);

            ImageButtonSupprimer = "@drawable/supprimer.png";
            ImageButtonModifier = "@drawable/modifier.png";
        }

        private void ModifierReview()
        {
            PopUpValiderModification();
        }

        private void SupprimerReview()
        {
            PopUpValiderSuppression();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            ReviewD =  parameters.GetValue<ReviewDisplay>("review");
        }

        async void PopUpValiderModification()
        {
            var answer = await App.Current.MainPage.DisplayAlert("Modification", "Êtes-vous sûr de vouloir modifier l'enregistrement ?", "Oui", "Non");
            Console.WriteLine(answer);
            if (answer.Equals(true))
            {
                NavigationParameters navigationParam = new NavigationParameters();
                navigationParam.Add("mode", false);
                navigationParam.Add("review", ReviewD);

                await NavigationService.NavigateAsync("EditeurReviewPage", navigationParam);
            }
        }

        async void PopUpValiderSuppression()
        {            
            var answer = await App.Current.MainPage.DisplayAlert("Suppression", "Êtes-vous sûr de vouloir supprimer l'enregistrement ? Cette action n'est pas reversible", "Oui", "Non");
            Console.WriteLine(answer);
            if (answer.Equals(true))
            {
                _reviewService.DeleteReview(ReviewD.Id);
                await NavigationService.NavigateAsync("/MenuApp/NavigationPage/ListeReviewsPage");
            }
        }

    }
}
