using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Model;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ProjetDevMobile.ViewModels
{
	public class ListeReviewsPageViewModel : ViewModelBase
	{
        private Image _imageCheckedFood;
        public Image ImageCheckedFood
        {
            get { return _imageCheckedFood; }
            set { SetProperty(ref _imageCheckedFood, value); }
        }

        private Image _imageCheckedDrink;
        public Image ImageCheckedDrink
        {
            get { return _imageCheckedDrink; }
            set { SetProperty(ref _imageCheckedDrink, value); }
        }

        private Image _imageCheckedToSee;
        public Image ImageCheckedToSee
        {
            get { return _imageCheckedToSee; }
            set { SetProperty(ref _imageCheckedToSee, value); }
        }

        private ObservableCollection<Review> _reviews;
        public ObservableCollection<Review> Reviews  
        {
            get { return _reviews; }
            set { SetProperty(ref _reviews, value); }
        }

        private bool _isFoodChecked { get; set; }
        private bool _isDrinkChecked { get; set; }
        private bool _isToSeeChecked { get; set; }

        public DelegateCommand<Review> CommandReviewDetails { get; private set; }
        public DelegateCommand CommandFoodFilter { get; private set; }
        public DelegateCommand CommandDrinkFilter { get; private set; }
        public DelegateCommand CommandToSeeFilter { get; private set; }

        private IReviewService _reviewService;

        public ListeReviewsPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            _reviewService = reviewService;
            CommandReviewDetails = new DelegateCommand<Review>(DetailsReview);

            CommandFoodFilter = new DelegateCommand(ChangeFoodFilter);
            CommandDrinkFilter = new DelegateCommand(ChangeDrinkFilter);
            CommandToSeeFilter = new DelegateCommand(ChangeToSeeFilter);

            //TODO : les images ne sont pas reconnus, utilisation de Button en attendant de pouvoir mettre des ImageButton
            ImageCheckedDrink = new Image { Source = "CheckedBox.png" };
            ImageCheckedFood = new Image { Source = "CheckedBox.png" };
            ImageCheckedToSee = new Image { Source = "CheckedBox.png" };

            _isDrinkChecked = true;
            _isFoodChecked = true;
            _isToSeeChecked = true;
        }

        private void ChangeToSeeFilter()
        {
            _isToSeeChecked = !_isToSeeChecked;
            SetReviews();
        }

        private void ChangeDrinkFilter()
        {
            _isDrinkChecked = !_isDrinkChecked;
            SetReviews();
        }

        private void ChangeFoodFilter()
        {
            _isFoodChecked = !_isFoodChecked;
            SetReviews();
        }

        private void DetailsReview(Review review)
        {
            NavigationParameters navigationParam = new NavigationParameters();
            navigationParam.Add("review", review);

            NavigationService.NavigateAsync("DetailsReviewPage", navigationParam);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            SetReviews();
        }

        public void SetReviews()
        {
            Reviews = new ObservableCollection<Review>(_reviewService.GetReviews(_isFoodChecked, _isDrinkChecked, _isToSeeChecked));

        }
    }
}
