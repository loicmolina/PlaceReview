using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ProjetDevMobile.Model;
using ProjetDevMobile.Services;
using Xamarin.Forms;

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

        private System.Drawing.Image _photo;
        public System.Drawing.Image Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        private List<string> _typesReview;
        public List<string> TypesReview
        {
            get { return _typesReview; }
            set { SetProperty(ref _typesReview, value); }
        }
        
        //public DelegateCommand ValiderCommand { get; private set; }
        ////public DelegateCommand<Task> PhotoCommand { get; private set; }
        //private IReviewService _reviewService { get; set; }

        //public NouvelleReviewPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        //{
        //    ValiderCommand = new DelegateCommand(Enregistrer);
        //    //PhotoCommand = new DelegateCommand<Task>(PrendrePhotoAsync);
        //    _reviewService = reviewService;
        //    TypesReview = new List<string>();
        //    TypesReview.AddRange(Enum.GetNames(typeof(ReviewTypes)));            
        //}

        //private async void PrendrePhotoAsync(Task obj)
        //{
        //    var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
        //}

        //private void Enregistrer()
        //{
        //    ReviewDisplay review = new ReviewDisplay(Titre, Description, Tag);
        //    review.Photo = Photo;
        //    review.DatePublication = DateTime.Now;
        //    _reviewService.AddReview(review.ToReview());
        //    NavigationService.NavigateAsync("NavigationPage/MainPage");
        //}
    }
}
