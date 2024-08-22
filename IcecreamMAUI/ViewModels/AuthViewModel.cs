using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IcecreamMAUI.Pages;
using IcecreamMAUI.Services;
using IcecreamMAUI.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI.ViewModels
{
    public partial class AuthViewModel(IAuthApi authAPI, AuthService authService) : BaseViewModel
    {
        private readonly IAuthApi _authAPI = authAPI;
        private readonly AuthService _authService = authService;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _name;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _email;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _password;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _address;

        public bool CanSignin => !string.IsNullOrEmpty(Email)
            && !string.IsNullOrEmpty(Password);

        public bool CanSignup => CanSignin
            && !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(Address);

        //validates that all the four properties have valid values
        [RelayCommand]
        private async Task SignupAsync()
        {
            IsBusy = true;
            try
            {
                var signUpDto = new SignupRequestDto(Name, Email, Password, Address);
                //Make api call
                var result = await _authAPI.SignupAsync(signUpDto);

                if(result.IsSuccess)
                {
                    _authService.Signin(result.data);
                    //Navigate to HomePage
                    await GoToAsync($"//{nameof(HomePage)}", animate: true);
                }
                else
                {
                    //Display Error Message
                    await ShowErrorAlertAsync(result.ErrorMessage ?? "Unknown error occurred when signing up");
                }
            }
            catch (Exception ex) 
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally 
            { 
                IsBusy = false; 
            }

        }

        [RelayCommand]
        private async Task SigninAsync()
        {
            IsBusy = true;
            try
            {
                var signinDto = new SigninRequestDto(Email, Password);
                //Make api call
                var result = await _authAPI.SigninAsync(signinDto);

                if (result.IsSuccess)
                {
                    _authService.Signin(result.data);
                    //Navigate to HomePage
                    await GoToAsync($"//{nameof(HomePage)}", animate: true);
                }
                else
                {
                    //Display Error Message
                    await ShowErrorAlertAsync(result.ErrorMessage ?? "Unknown error occurred when signing in");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
