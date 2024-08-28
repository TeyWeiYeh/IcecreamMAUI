using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI.Models
{
    public partial class IcecreamOption : ObservableObject
    {
        public string Flavour { get; set; }
        public string Topping { get; set; }

        [ObservableProperty]
        private bool _isSelected;
    }
}
