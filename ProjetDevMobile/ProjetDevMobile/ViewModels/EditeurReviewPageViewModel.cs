using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using ProjetDevMobile.Model;
using ProjetDevMobile.Services;
using Xamarin.Forms;

namespace ProjetDevMobile.ViewModels
{
    public class EditeurReviewPageViewModel : ViewModelBase
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

        private ObservableCollection<string> _listeTags = new ObservableCollection<string>();
        public ObservableCollection<string> ListeTags
        {
            get { return _listeTags; }
            set { SetProperty(ref _listeTags, value); }
        }

        private string _tag = "";
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); AjouterTag(_tag); }
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

        private String _adresse = "Inconnu";
        public String Adresse
        {
            get { return _adresse; }
            set { SetProperty(ref _adresse, value); }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }
        public DelegateCommand<string> CommandRemoveTag { get; private set; }

        public DelegateCommand ValiderCommand { get; private set; }
        public DelegateCommand<Task> PhotoCommand { get; private set; }
        private IReviewService _reviewService { get; set; }

        public EditeurReviewPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            ReviewD = new ReviewDisplay();
            ValiderCommand = new DelegateCommand(Enregistrer, ActiverValider).ObservesProperty(() => Titre).ObservesProperty(() => Description).ObservesProperty(() => Tag).ObservesProperty(() => Photo).ObservesProperty(() => ListeTags);
            PhotoCommand = new DelegateCommand<Task>(ChoisirImageAsync);
            CommandRemoveTag = new DelegateCommand<string>(SupprimerTag);
            _reviewService = reviewService;
            TypesReview = new List<string>();
            TypesReview.AddRange(Enum.GetNames(typeof(ReviewTypes)));
            ImageButtonValider = "@drawable/save.png";
        }

        private void AjouterTag(string tag)
        {
            if (tag!= null && !tag.Equals("") && ListeTags != null && !ListeTags.Contains(tag))
            {
                ListeTags.Add(tag);
            }
        }

        private void SupprimerTag(string tag)
        {
            if (IsModeAjout)
            {
                ListeTags.Remove(tag);
            }
        }

        public void SetMode()
        {
            if (IsModeAjout)
            {
                Title = "Nouveau";
                Titre = "";
                Description = "";
                Tag = "";
                ListeTags = new ObservableCollection<string>();
                Photo = null;
                ImageButtonPhoto = "@drawable/appareil_photo.png";
                RecupererPosition();
            }
            else
            {
                Title = "Modification";
                Titre = ReviewD.Titre;
                Description = ReviewD.Description;
                Tag = "";
                ListeTags = new ObservableCollection<string>(ReviewD.Tags);
                Photo = ReviewD.Photo;
                ImageButtonPhoto = Photo.Source;
                Latitude = ReviewD.Latitude;
                Longitude = ReviewD.Longitude;
                Adresse = ReviewD.Adresse;
            }
        }

        private async void ChoisirImageAsync(Task obj)
        {
            var answer = await App.Current.MainPage.DisplayAlert("Mode de Selection","Comment souhaitez-vous renseigner l'image de la review ?", "Caméra", "Galerie");
            MediaFile file;
            if (answer.Equals(true))
            {
                file = await PrendrePhotoAsync(obj);
            }
            else
            {
                file = await SelectionDansGalerie(obj);
            }
            if (file == null)
            {
                return;
            }

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

        private async Task<MediaFile> SelectionDansGalerie(Task obj)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Non supporté", "Votre appareil ne permet pas de selectionner une image dans la galerie", "Ok");
                return null;
            }
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            });

            if (selectedImageFile == null)
            {
                await App.Current.MainPage.DisplayAlert("Erreur", "Erreur dans le charement de l'image, veuillez réessayer.", "Ok");
            }
            return selectedImageFile;
        }

        private async Task<MediaFile> PrendrePhotoAsync(Task obj)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Console.WriteLine("Camera inaccessible");
                return null;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,

            });
            return file;
        }

        private void Enregistrer()
        {
            PopUpValider();
        }

        public async void RecupererPosition()
        {
            Position position = null;
            try
            {
                IGeolocator localisation = CrossGeolocator.Current;
                localisation.DesiredAccuracy = 100;
                position = await localisation.GetLastKnownLocationAsync();
                if (position != null)
                {
                    Latitude = position.Latitude;
                    Longitude = position.Longitude;
                    IEnumerable<Address> addresses = await localisation.GetAddressesForPositionAsync(position, null);
                    Address address = addresses.FirstOrDefault();
                    Adresse = address.Thoroughfare + ", " + address.PostalCode + " " + address.Locality;
                }
                else
                {
                    if (!localisation.IsGeolocationAvailable || !localisation.IsGeolocationEnabled)
                    {
                        Console.WriteLine("Erreur !");
                        return;
                    }
                    else
                    {
                        position = await localisation.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                        if (position != null)
                        {
                            Latitude = position.Latitude;
                            Longitude = position.Longitude;
                            IEnumerable<Address> addresses = await localisation.GetAddressesForPositionAsync(position, null);
                            Address address = addresses.FirstOrDefault();
                            Adresse = address.Thoroughfare + ", " + address.PostalCode + " " + address.Locality;
                        }
                        else
                        {
                            Latitude = 0;
                            Longitude = 0;
                            Adresse = "Inconnu";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Impossible de récupérer la location: " + ex);
            }
        }

        private bool ActiverValider()
        {
            return Photo != null                
                && Titre != null && !Titre.Equals("")
                && Description != null && !Description.Equals("")
                && ((Tag != null && !Tag.Equals("")) || (ListeTags != null && ListeTags.Count > 0));
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
                ReviewD.Tags = ListeTags.ToList();
                if (IsModeAjout)
                {
                    ReviewD.DatePublication = DateTime.Now;
                    ReviewD.Latitude = Latitude;
                    ReviewD.Longitude = Longitude;
                    ReviewD.Adresse = Adresse;
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
