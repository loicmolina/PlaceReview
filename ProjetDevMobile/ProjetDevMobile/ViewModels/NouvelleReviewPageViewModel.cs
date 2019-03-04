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
        private bool _isModeAjout = true;
        public bool IsModeAjout
        {
            get { return _isModeAjout; }
            set { SetProperty(ref _isModeAjout, value); }
        }

        private ReviewDisplay _reviewD;
        public ReviewDisplay ReviewD
        {
            get { return _reviewD; }
            set { SetProperty(ref _reviewD, value); }
        }

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

        private ImageSource _imageButtonPhoto;
        public ImageSource ImageButtonPhoto
        {
            get { return _imageButtonPhoto; }
            set { SetProperty(ref _imageButtonPhoto, value); }
        }

        private ImageSource _imageButtonValider;
        public ImageSource ImageButtonValider
        {
            get { return _imageButtonValider; }
            set { SetProperty(ref _imageButtonValider, value); }
        }

        public DelegateCommand ValiderCommand { get; private set; }
        public DelegateCommand<Task> PhotoCommand { get; private set; }
        private IReviewService _reviewService { get; set; }

        public NouvelleReviewPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            ReviewD = new ReviewDisplay();
            ValiderCommand = new DelegateCommand(Enregistrer, ActiverValider).ObservesProperty(() => Titre).ObservesProperty(() => Description).ObservesProperty(() => Tag);//.ObservesProperty(() => Photo);
            PhotoCommand = new DelegateCommand<Task>(PrendrePhotoAsync);
            _reviewService = reviewService;
            TypesReview = new List<string>();
            TypesReview.AddRange(Enum.GetNames(typeof(ReviewTypes)));
            ImageButtonValider = "@drawable/save.png";
        }

        public void SetMode()
        {
            if (IsModeAjout)
            {
                Title = "Nouveau";
                Titre = "";
                Description = "";
                Tag = "";
                Photo = null;
                ImageButtonPhoto = "@drawable/appareil_photo.png";
            }
            else
            {
                Title = "Modification";
                Titre = ReviewD.Titre;
                Description = ReviewD.Description;
                Tag = ReviewD.Tag;
                Photo = ReviewD.Photo;
                ImageButtonPhoto = Photo.Source;
            }
        }

        private async void PrendrePhotoAsync(Task obj)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Console.WriteLine("Camera inaccessible");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
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
            ImageButtonPhoto = Photo.Source;
        }

        private void Enregistrer()
        {
            PopUpValider();
        }

        private bool ActiverValider()
        {
            return Photo != null                
                && Titre != null && !Titre.Equals("")
                && Description != null && !Description.Equals("")
                && Tag != null && !Tag.Equals("");
        }

        async void PopUpValider()
        {
            string _question;
            if (IsModeAjout)
            {
                _question = "Êtes-vous sûr de vouloir ajouter ce nouvel enregistrement ?";
            }
            else
            {
                _question = "Êtes-vous sûr de vouloir modifier cet enregistrement ?";
            }
            var answer = await App.Current.MainPage.DisplayAlert("Ajout", _question, "Oui", "Non");
            if (answer.Equals(true))
            {
                ReviewD.Titre = Titre;
                ReviewD.Description = Description;
                ReviewD.Tag = Tag;
                if (IsModeAjout)
                {
                    ReviewD.DatePublication = DateTime.Now;
                    Review reviewSaved = ReviewD.ToReview();
                    reviewSaved.Photo = PhotoArray;
                    _reviewService.AddReview(reviewSaved);
                }
                else
                {
                    Review reviewSaved = ReviewD.ToReview();
                    reviewSaved.Photo = _reviewService.GetReviewById(reviewSaved.Id).Photo;

                    _reviewService.UpdateReview(reviewSaved);
                }

                await NavigationService.NavigateAsync("/MenuApp/NavigationPage/MainPage");
            }
            
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            IsModeAjout = parameters.GetValue<bool>("mode");
            if (!IsModeAjout)
            {
                ReviewD = parameters.GetValue<ReviewDisplay>("review");
            }
            SetMode();
        }
    }
}
