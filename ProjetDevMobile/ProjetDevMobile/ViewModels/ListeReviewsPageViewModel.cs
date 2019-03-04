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

        private string _sourceImageButtonTriRecent;
        public string SourceImageButtonTriRecent
        {
            get { return _sourceImageButtonTriRecent; }
            set { SetProperty(ref _sourceImageButtonTriRecent, value); }
        }

        private string _sourceImageButtonTriAncien;
        public string SourceImageButtonTriAncien
        {
            get { return _sourceImageButtonTriAncien; }
            set { SetProperty(ref _sourceImageButtonTriAncien, value); }
        }

        private ObservableCollection<ReviewDisplay> _reviews;
        public ObservableCollection<ReviewDisplay> ReviewsD  
        {
            get { return _reviews; }
            set { SetProperty(ref _reviews, value); }
        }

        private bool _isFoodChecked { get; set; }
        private bool _isDrinkChecked { get; set; }
        private bool _isToSeeChecked { get; set; }
        private bool _isTriRecent { get; set; }

        private string _checkedbox { get; set; }
        private string _uncheckedbox { get; set; }

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

            ReviewsD = new ObservableCollection<ReviewDisplay>();

            _isTriRecent = true;

            CommandFoodFilter = new DelegateCommand(ChangeFoodFilter);
            CommandDrinkFilter = new DelegateCommand(ChangeDrinkFilter);
            CommandToSeeFilter = new DelegateCommand(ChangeToSeeFilter);

            CommandTriRecent = new DelegateCommand(ChangeTriRecent);
            CommandTriAncien = new DelegateCommand(ChangeTriAncien);

            _checkedbox = "@drawable/checkedbox.png";
            _uncheckedbox = "@drawable/uncheckedbox.png";

            SourceImageButtonDrink = _checkedbox;
            SourceImageButtonFood = _checkedbox;
            SourceImageButtonToSee = _checkedbox;

            SourceImageButtonTriRecent = "@drawable/arrow_up.png";
            SourceImageButtonTriAncien = "@drawable/arrow_down_gray.png";

            _isDrinkChecked = true;
            _isFoodChecked = true;
            _isToSeeChecked = true;
        }

        private void ChangeTriAncien()
        {
            if (_isTriRecent)
            {
                ReviewsD = new ObservableCollection<ReviewDisplay>(ReviewsD.OrderBy(rev => rev.DatePublication));
                _isTriRecent = !_isTriRecent;
                SourceImageButtonTriRecent = "@drawable/arrow_up_gray.png";
                SourceImageButtonTriAncien = "@drawable/arrow_down.png";
            }
        }

        private void ChangeTriRecent()
        {
            if (!_isTriRecent)
            {
                ReviewsD = new ObservableCollection<ReviewDisplay>(ReviewsD.OrderByDescending(rev => rev.DatePublication));
                _isTriRecent = !_isTriRecent;
                SourceImageButtonTriRecent = "@drawable/arrow_up.png";
                SourceImageButtonTriAncien = "@drawable/arrow_down_gray.png";
            }
        }

        private void ChangeToSeeFilter()
        {
            _isToSeeChecked = !_isToSeeChecked;
            SourceImageButtonToSee = ChangeFilter(_isToSeeChecked, SourceImageButtonToSee);
        }

        private void ChangeDrinkFilter()
        {
            _isDrinkChecked = !_isDrinkChecked;
            SourceImageButtonDrink = ChangeFilter(_isDrinkChecked, SourceImageButtonDrink);
        }

        private void ChangeFoodFilter()
        {
            _isFoodChecked = !_isFoodChecked;
            SourceImageButtonFood = ChangeFilter(_isFoodChecked, SourceImageButtonFood);
        }

        private string ChangeFilter(bool _isChecked, string SourceImageButton)
        { 
            if (_isChecked)
            {
                SourceImageButton = _checkedbox;
            }
            else
            {
                SourceImageButton = _uncheckedbox;
            }
            SetReviews();
            return SourceImageButton;
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
            ReviewsD.Clear();
            foreach (Review rev in reviews)
            {
                ReviewsD.Add(rev.ToReviewDisplay());
            }
        }
    }
}
