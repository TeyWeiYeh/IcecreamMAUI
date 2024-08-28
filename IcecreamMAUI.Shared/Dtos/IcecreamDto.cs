using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI.Shared.Dtos
{
    public record struct IcecreamOptionDto(string Flavour, string Topping);

    public record IcecreamDto(int Id, string Name, string Image, double Price, IcecreamOptionDto[] Options);
}
