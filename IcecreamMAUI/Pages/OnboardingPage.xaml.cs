namespace IcecreamMAUI.Pages;

public partial class OnboardingPage : ContentPage
{
	public OnboardingPage()
	{
		InitializeComponent();
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