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
    public partial class HomeViewModel(IIcecreamsAPI icecreamsAPI, AuthService authService) : BaseViewModel
    {
        private readonly IIcecreamsAPI icecreamsAPI = icecreamsAPI;
        private readonly AuthService _authService = authService;

        [ObservableProperty]
        private string _Username = string.Empty;

        [ObservableProperty]
        private IcecreamDto[] _icecreams = [];

        private bool _IsInitialized;
        
        //this method only works for authorized users
        public async Task InitializeAsync()
        {
            Username = _authService.User!.Name;

            if(_IsInitialized)
            {
                return;
            }
            IsBusy = true;
            try
            {
                _IsInitialized = true;
                //Make API call to fetch icecreams
                Icecreams = await icecreamsAPI.GetIcecreamsAsync();
            }
            catch(Exception ex)
            {
                _IsInitialized = false;
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        //create a method for us called GotoDetailsPage command
        [RelayCommand]
        private async Task GoToDetailsPageAsync(IcecreamDto icecream)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Icecream)] = icecream,
            };
            await GoToAsync(nameof(DetailsPage),animate:true, parameter);
        }
           
    }
}
