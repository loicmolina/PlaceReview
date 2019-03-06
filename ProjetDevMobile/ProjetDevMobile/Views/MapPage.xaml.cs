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
            MyMap.MapType = MapType.Hybrid;
            MyMap.IsShowingUser = true;

            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = new Position(48.201684, 5.951324),
                Label = "Vittel c'est de l'eau",
                Address = "Le feu"
            };
            
            MapPageViewModel.Map = MyMap;

            MyMap.Pins.Add(pin);

        }
    }
}
