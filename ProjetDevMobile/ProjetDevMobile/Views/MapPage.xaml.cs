using ProjetDevMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(49.118722, 6.175360), Distance.FromMiles(1)));
            MapPageViewModel.Map = MyMap;
        }

        void ZoomSurLaCarte(object sender, ValueChangedEventArgs args)
        {
            var zoomLevel = args.NewValue;
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            if (MyMap != null && MyMap.VisibleRegion != null)
            {
                MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            }
        }
    }
}
