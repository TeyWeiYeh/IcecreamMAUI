using IcecreamMAUI.Services;
using IcecreamMAUI.ViewModels;

namespace IcecreamMAUI
{
    //the first page that gets loaded when we open the app
    public partial class App : Application
    {
        private readonly CartViewModel _cartViewModel;
        public App(AuthService authService, CartViewModel cartViewModel)
        {
            InitializeComponent();

            authService.Initialize();

            MainPage = new AppShell(authService);
            _cartViewModel = cartViewModel;
        }

        protected override async void OnStart()
        {
            await _cartViewModel.InitializedCartAsync();
        }
    }
}
