using IcecreamMAUI.Services;

namespace IcecreamMAUI
{
    //the first page that gets loaded when we open the app
    public partial class App : Application
    {
        public App(AuthService authService)
        {
            InitializeComponent();

            authService.Initialize();

            MainPage = new AppShell(authService);
        }
    }
}
