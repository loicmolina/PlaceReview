using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMobile.Model;
using ProjetDevMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection
        {
            get { return _pinCollection; }
            set { SetProperty(ref _pinCollection, value); }
        }
        
        public static Map Map { get; set; }

        private IReviewService _reviewService { get; set; }

        public MapPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            _reviewService = reviewService;
            
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Map.Pins.Clear();
            foreach(Review rev in _reviewService.GetReviews())
            {
                StringBuilder sb = new StringBuilder();
                foreach (string str in rev.Tags)
                {
                    sb.Append("#" + str + " ");
                }
                var pin = new Pin
                {
                    Type = PinType.Generic,
                    Position = new Position(rev.Latitude, rev.Longitude),
                    Label = "\"" + rev.Titre + "\"",
                    Address = sb.ToString()
                };
                pin.Clicked += (sender, e) => {
                    NavigationParameters np = new NavigationParameters();
                    np.Add("review", rev.ToReviewDisplay());
                    NavigationService.NavigateAsync("DetailsReviewPage", np);
                };
                Map.Pins.Add(pin);
            }
        }
    }
}
