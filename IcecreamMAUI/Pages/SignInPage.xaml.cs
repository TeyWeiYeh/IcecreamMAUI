using IcecreamMAUI.ViewModels;

namespace IcecreamMAUI.Pages;

public partial class SignInPage : ContentPage
{
	public SignInPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
        BindingContext = authViewModel;
	}

    private async void SignupLabel_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
}