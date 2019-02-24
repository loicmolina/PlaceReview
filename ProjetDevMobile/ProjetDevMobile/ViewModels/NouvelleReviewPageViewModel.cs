using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

        private Image _photo;
        public Image Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        private byte[] _photoArray;
        public byte[] PhotoArray
        {
            get { return _photoArray; }
            set { SetProperty(ref _photoArray, value); }
        }

        private List<string> _typesReview;
        public List<string> TypesReview
        {
            get { return _typesReview; }
            set { SetProperty(ref _typesReview, value); }
        }

        public DelegateCommand ValiderCommand { get; private set; }
        public DelegateCommand<Task> PhotoCommand { get; private set; }
        private IReviewService _reviewService { get; set; }

        public NouvelleReviewPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            ValiderCommand = new DelegateCommand(Enregistrer);
            PhotoCommand = new DelegateCommand<Task>(PrendrePhotoAsync);
            _reviewService = reviewService;
            TypesReview = new List<string>();
            TypesReview.AddRange(Enum.GetNames(typeof(ReviewTypes)));
        }

        private async void PrendrePhotoAsync(Task obj)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Console.WriteLine("Camera inaccessible");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,

            });

            if (file == null)
                return;


            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                var myfile = memoryStream.ToArray();
                PhotoArray = myfile;
            }

            if (Photo == null)
            {
                Photo = new Image { Source = ImageSource.FromFile(file.Path) };
            }
            else
            {
                Photo.Source = ImageSource.FromFile(file.Path);
            }
        }

        private void Enregistrer()
        {
            ReviewDisplay review = new ReviewDisplay(Titre, Description, Tag);
            review.Photo = Photo;
            review.DatePublication = DateTime.Now;

            Review reviewSaved = review.ToReview();
            reviewSaved.Photo = PhotoArray;

            _reviewService.AddReview(reviewSaved);
            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }
    }
}
