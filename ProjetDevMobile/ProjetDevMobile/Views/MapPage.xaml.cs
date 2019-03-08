using ProjetDevMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProjetDevMobile.Views
{
    // Pas franchement propre mais si le GIT de Microsoft fait comme ça ¯\_(ツ)_/¯
    public partial class MapPage : ContentPage
    {
        Map map;
        public MapPage()
        {
            map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(49.118722, 6.175360), Distance.FromMiles(1)));

            Slider slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) => {
                double zoomLevel = e.NewValue;
                double latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                if (map.VisibleRegion != null)
                {
                    map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
                }
            };

            Button rues = new Button { Text = "Rues" };
            Button hybride = new Button { Text = "Hybride" };
            Button satellite = new Button { Text = "Satellite" };
            rues.Clicked += CliqueBouton;
            hybride.Clicked += CliqueBouton;
            satellite.Clicked += CliqueBouton;
            StackLayout boutons = new StackLayout
            {
                Spacing = 30,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { rues, hybride, satellite }
            };

            StackLayout stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            stack.Children.Add(slider);
            stack.Children.Add(boutons);
            Content = stack;

            MapPageViewModel.Map = map;
        }

        private void CliqueBouton(object obj, EventArgs e)
        {
            var bouton = obj as Button;
            switch (bouton.Text)
            {
                case "Rues":
                    map.MapType = MapType.Street;
                    break;
                case "Hybride":
                    map.MapType = MapType.Hybrid;
                    break;
                case "Satellite":
                    map.MapType = MapType.Satellite;
                    break;
            }
        }
    }
}