using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IcecreamMAUI.Models;
using IcecreamMAUI.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI.ViewModels
{
    //detailspage?Icecream=VALUE
    //query id parameters: name of the query, name of the query in the shell navigation
    [QueryProperty(nameof(Icecream), nameof(Icecream))]
    public partial class DetailsViewModel : BaseViewModel
    {
        public DetailsViewModel(CartViewModel cartViewModel)
        {
            _cartViewModel = cartViewModel;
        }
        //pass icecream info from homepage to our details page
        [ObservableProperty]
        private IcecreamDto? _icecream;

        [ObservableProperty]
        private int quantity;

        [ObservableProperty]
        private IcecreamOption[] _options = [];
        private readonly CartViewModel _cartViewModel;

        partial void OnIcecreamChanged(IcecreamDto? value)
        {
            Options = [];
            if (value is null)
                return;

            Options = value.Options.Select(o => new IcecreamOption
            {
                Flavour = o.Flavour,
                Topping = o.Topping,
                IsSelected = false
            }).ToArray();

            Quantity = _cartViewModel.GetItemCartCount(value.Id);
        }

        [RelayCommand]
        private void IncreaseQuantity() => Quantity++;

        [RelayCommand]
        private void DecreaseQuantity()
        {
            Quantity--;
        }

        [RelayCommand] //GoBackCommand
        private async Task GoBackAsync() => await GoToAsync("..", animate:true);

        //whenever we select a new option, we shld deselect all the others options first 
        [RelayCommand]
        private void SelectOption(IcecreamOption newOption)
        {
            var newIsSelected = !newOption.IsSelected;
            //deselect all options
            Options = [..Options.Select(o => { o.IsSelected = false;return o; })];
            //set it to the new selected item
            newOption.IsSelected = newIsSelected;
        }

        [RelayCommand]
        private void AddToCart()
        {
            var selectedOption = Options.FirstOrDefault(o => o.IsSelected) ?? Options[0];
            _cartViewModel.AddItemToCart(Icecream!, Quantity, selectedOption.Flavour, selectedOption.Topping);
        }
    }
}
