using IcecreamMAUI.Services;

namespace IcecreamMAUI.Pages;

public partial class OnboardingPage : ContentPage
{
    private readonly AuthService _authService;
	public OnboardingPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
	}

    protected async override void OnAppearing()
    {
        if(_authService.User is not null
            && _authService.User.id != default(Guid)
            && !string.IsNullOrWhiteSpace(_authService.Token))
        {
            //user is logged in
            // Navigate user to Home Page
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }

	//goes to the home page on click
    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }

    //redirects to the sign in page 
    private async void SignIn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignInPage));
    }

    private async void SignUp_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
}