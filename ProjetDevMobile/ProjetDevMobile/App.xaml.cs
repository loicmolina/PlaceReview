using Prism;
using Prism.Ioc;
using ProjetDevMobile.ViewModels;
using ProjetDevMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjetDevMobile.Client;
using ProjetDevMobile.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProjetDevMobile
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/MenuApp/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILiteDBClient, LiteDBClient>();
            containerRegistry.RegisterSingleton<IReviewService, ReviewService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuApp, MenuAppViewModel>();
            containerRegistry.RegisterForNavigation<EditeurReviewPage, EditeurReviewPageViewModel>();
            containerRegistry.RegisterForNavigation<ListeReviewsPage, ListeReviewsPageViewModel>();
            containerRegistry.RegisterForNavigation<DetailsReviewPage, DetailsReviewPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<BonusPage, BonusPageViewModel>();
        }
    }
}
