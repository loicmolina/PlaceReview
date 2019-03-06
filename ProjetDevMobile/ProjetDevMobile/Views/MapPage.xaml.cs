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
            MyMap.MapType = MapType.Hybrid;
            MyMap.IsShowingUser = true;

            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = new Position(48.201684, 5.951324),
                Label = "Vittel c'est de l'eau",
                Address = "Le feu"
            };

            MyMap.Pins.Add(pin);
        }

        void ZoomSurLaCarte(object sender, ValueChangedEventArgs args)
        {
            var zoomLevel = args.NewValue;
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            if (MyMap != null)
            {
                MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            }
        }
    }
}
