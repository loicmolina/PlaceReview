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
        private string _sourceImageButtonFood;
        public string SourceImageButtonFood
        {
            get { return _sourceImageButtonFood; }
            set { SetProperty(ref _sourceImageButtonFood, value); }
        }

        private string _sourceImageButtonDrink;
        public string SourceImageButtonDrink
        {
            get { return _sourceImageButtonDrink; }
            set { SetProperty(ref _sourceImageButtonDrink, value); }
        }

        private string _sourceImageButtonToSee;
        public string SourceImageButtonToSee
        {
            get { return _sourceImageButtonToSee; }
            set { SetProperty(ref _sourceImageButtonToSee, value); }
        }

        private ObservableCollection<ReviewDisplay> _reviews;
        public ObservableCollection<ReviewDisplay> Reviews  
        {
            get { return _reviews; }
            set { SetProperty(ref _reviews, value); }
        }

        private bool _isFoodChecked { get; set; }
        private bool _isDrinkChecked { get; set; }
        private bool _isToSeeChecked { get; set; }
        private bool _isTriRecent { get; set; }

        public DelegateCommand<ReviewDisplay> CommandReviewDetails { get; private set; }
        public DelegateCommand CommandFoodFilter { get; private set; }
        public DelegateCommand CommandDrinkFilter { get; private set; }
        public DelegateCommand CommandToSeeFilter { get; private set; }
        public DelegateCommand CommandTriRecent { get; private set; }
        public DelegateCommand CommandTriAncien { get; private set; }

        private IReviewService _reviewService;

        public ListeReviewsPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            Title = "Enregistrements";

            _reviewService = reviewService;
            CommandReviewDetails = new DelegateCommand<ReviewDisplay>(DetailsReview);

            Reviews = new ObservableCollection<ReviewDisplay>();

            _isTriRecent = true;

            CommandFoodFilter = new DelegateCommand(ChangeFoodFilter);
            CommandDrinkFilter = new DelegateCommand(ChangeDrinkFilter);
            CommandToSeeFilter = new DelegateCommand(ChangeToSeeFilter);

            CommandTriRecent = new DelegateCommand(ChangeTriRecent);
            CommandTriAncien = new DelegateCommand(ChangeTriAncien);

            SourceImageButtonDrink = "https://static.thenounproject.com/png/341263-200.png";
            SourceImageButtonFood = "https://static.thenounproject.com/png/341263-200.png";
            SourceImageButtonToSee = "https://static.thenounproject.com/png/341263-200.png";

            _isDrinkChecked = true;
            _isFoodChecked = true;
            _isToSeeChecked = true;
        }

        private void ChangeTriAncien()
        {
            if (_isTriRecent)
            {
                Reviews.OrderByDescending(rev => rev.DatePublication);
            }
        }

        private void ChangeTriRecent()
        {
                    if (!_isTriRecent)
            {
                Reviews.OrderBy(rev => rev.DatePublication);
            }
        }

        private void ChangeToSeeFilter()
        {
            _isToSeeChecked = !_isToSeeChecked;
            if (_isToSeeChecked)
            {
                SourceImageButtonToSee = "https://static.thenounproject.com/png/341263-200.png";
            }
            else
            {
                SourceImageButtonToSee = "https://static.thenounproject.com/png/341262-200.png";
            }
            SetReviews();
        }

        private void ChangeDrinkFilter()
        {
            _isDrinkChecked = !_isDrinkChecked;
            if (_isDrinkChecked)
            {
                SourceImageButtonDrink = "https://static.thenounproject.com/png/341263-200.png";
            }
            else
            {
                SourceImageButtonDrink = "https://static.thenounproject.com/png/341262-200.png";
            }
            SetReviews();
        }

        private void ChangeFoodFilter()
        {
            _isFoodChecked = !_isFoodChecked;
            if (_isFoodChecked)
            {
                SourceImageButtonFood = "https://static.thenounproject.com/png/341263-200.png";
            }
            else
            {
                SourceImageButtonFood = "https://static.thenounproject.com/png/341262-200.png";
            }
            SetReviews();
        }

        private void DetailsReview(ReviewDisplay review)
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
            List<Review> reviews = _reviewService.GetReviews(_isFoodChecked, _isDrinkChecked, _isToSeeChecked);
            Reviews.Clear();
            foreach (Review rev in reviews)
            {
                Reviews.Add(rev.ToReviewDisplay());
            }
        }
    }
}
