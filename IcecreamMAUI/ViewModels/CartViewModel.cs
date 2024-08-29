using IcecreamMAUI.Models;
using IcecreamMAUI.Services;
using IcecreamMAUI.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        public CartViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public ObservableCollection<CartItem> CartItems { get; set; } = [];

        public static int TotalCartCount { get; set; }
        public static event EventHandler<int>? TotalCartCountChanged;

        public async void AddItemToCart(IcecreamDto icecream, int quantity, string flavour, string topping)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.IcecreamId == icecream.Id);
            if (existingItem is not null)
            {
                var dbCartItem = await _databaseService.GetCartItemAsync(existingItem.Id);
                if (quantity <= 0)
                {
                    
                    CartItems.Remove(existingItem);
                    await _databaseService.DeleteCartItem(dbCartItem);
                    //User wants to remove the item from cart
                    await ShowToastAsync("Icecream removed from cart");
                }
                else
                {
                    dbCartItem.Quantity = quantity;
                    await _databaseService.UpdateCartItem(dbCartItem);

                    existingItem.Quantity = quantity;
                    await ShowToastAsync("Quantity updated in cart");
                }
            }
            else
            {
                var CartItem = new CartItem
                {
                    FlavourName = flavour,
                    IcecreamId = icecream.Id,
                    Quantity = quantity,
                    Name = icecream.Name,
                    Price = icecream.Price,
                    ToppingName = topping
                };

                var entity = new Data.CartItemEntity(CartItem);
                await _databaseService.AddCartItem(entity);

                CartItem.Id = entity.Id;

                CartItems.Add(CartItem);

                //await _databaseService.AddCartItem(new Data.CartItemEntity(CartItem));

                await ShowToastAsync("Icecream added to cart");
            }

            NotifyCartCountChange();
        }

        private void NotifyCartCountChange()
        {
            TotalCartCount = CartItems.Sum(i => i.Quantity);
            TotalCartCountChanged?.Invoke(null, TotalCartCount);
        }

        public int GetItemCartCount(int icecreamId)
        {
            var existingItem = CartItems.FirstOrDefault(i=>i.IcecreamId == icecreamId);
            return existingItem?.Quantity ?? 0;
        }

        public async Task InitializedCartAsync()
        {
            var dbItems = await _databaseService.GetAllCartItemsAsync();
            foreach (var dbItem in dbItems)
            {
                CartItems.Add(dbItem.ToCartItemModel());
            }
            NotifyCartCountChange();
        }
    }
}
