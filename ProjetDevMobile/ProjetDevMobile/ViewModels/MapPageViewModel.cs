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

        private EventHandler<ValueChangedEventArgs> _valueChanged;
        public EventHandler<ValueChangedEventArgs> ValueChanged
        {
            get { return _valueChanged; }
            set { SetProperty(ref _valueChanged, value); }
        }

        private IReviewService _reviewService { get; set; }

        public MapPageViewModel(INavigationService navigationService, IReviewService reviewService) : base(navigationService)
        {
            _reviewService = reviewService;
            Map = new Map(MapSpan.FromCenterAndRadius(new Position(49.118722, 6.175360), Distance.FromMiles(0.3)));
            ValueChanged += (sender, e) =>
            {
                var zoomLevel = e.NewValue;
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                Map.MoveToRegion(new MapSpan(Map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Map.Pins.Clear();
            foreach(Review rev in _reviewService.GetReviews())
            {
                Map.Pins.Add(new Pin
                {
                    Type = PinType.Generic,
                    Position = new Position(rev.Longitude, rev.Latitude),
                    Label = "\"" + rev.Titre + "\"\n#" + rev.Tag
                });
            }
        }
    }
}
