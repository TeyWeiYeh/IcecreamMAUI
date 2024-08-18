using IcecreamMAUI.Pages;

namespace IcecreamMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private readonly static Type[] _routablePageTypes =
            [
                typeof(SignInPage),
                typeof(SignupPage),
                typeof(MyOrdersPage),
                typeof(ProfilePage),
                typeof(CartPage),
            ];

        private static void RegisterRoutes() {
            foreach (var pageType in _routablePageTypes)
            {
                Routing.RegisterRoute(pageType.Name, pageType);
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await Launcher.OpenAsync("https://www.google.com");
        }

        private async void SignoutMenuItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.DisplayAlert("Alert", "Signout menu item clicked", "OK");
        }
    }
}
