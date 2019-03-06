﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            var map = new Map(MapSpan.FromCenterAndRadius(new Position(49.118722, 6.175360), Distance.FromMiles(0.3)))
            {
                MapType = MapType.Hybrid,
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) =>
            {
                var zoomLevel = e.NewValue;
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            stack.Children.Add(slider);
            Content = stack;

            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = new Position(48.201684, 5.951324),
                Label = "Vittel bébé",
                Address = "Le feu"
            };

            map.Pins.Add(pin);
        }
    }
}
