using IcecreamMAUI.Pages;
using IcecreamMAUI.Services;

namespace IcecreamMAUI
{
    public partial class AppShell : Shell
    {
        private readonly AuthService _authService;
        public AppShell(AuthService authService)
        {
            InitializeComponent();
            RegisterRoutes();
            _authService = authService;
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
            //await Shell.Current.DisplayAlert("Alert", "Signout menu item clicked", "OK");
            _authService.Signout();
            await Shell.Current.GoToAsync($"//{nameof(OnboardingPage)}");
        }
    }
}
