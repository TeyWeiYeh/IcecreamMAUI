﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        protected async Task GoToAsync(string url, bool animate = false) =>
            await Shell.Current.GoToAsync(url, animate);

        protected async Task GoToAsync(string url, bool animate, IDictionary<string, object> parameters) =>
            await Shell.Current.GoToAsync(url, animate, parameters);

        protected async Task ShowErrorAlertAsync(string errorMessage) =>
            await Shell.Current.DisplayAlert("Error",errorMessage,"Ok");

        protected async Task ShowAlertAsync(string Message) =>
            await Shell.Current.DisplayAlert("Alert", Message, "Ok");

        protected async Task ShowToastAsync(string message) => await Toast.Make(message).Show();
    }
}
