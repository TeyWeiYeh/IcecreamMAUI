using IcecreamMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI.Data
{
    public class CartItemEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IcecreamId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string FlavourName { get; set; }
        public string ToppingName { get; set; }
        public int Quantity { get; set; }

        public CartItemEntity(CartItem cartItemModel)
        {
            IcecreamId = cartItemModel.IcecreamId;
            Name = cartItemModel.Name;
            Price = cartItemModel.Price;
            FlavourName = cartItemModel.FlavourName;
            ToppingName = cartItemModel.ToppingName;
            Quantity = cartItemModel.Quantity;  
        }

        public CartItemEntity()
        {
        }

        public CartItem ToCartItemModel() =>
            new ()
            {
                Id = Id,
                Name = Name,
                Price = Price,
                FlavourName = FlavourName,
                ToppingName = ToppingName,
                Quantity = Quantity,
                IcecreamId = IcecreamId,
            };

    }
}
