using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IcecreamMAUI.API.Data.Entities
{
    public class IcecreamOption
    {
        public int IcecreamId { get; set; }

        [Required, MaxLength(50)]
        public string Flavour { get; set; }

        [Required, MaxLength(50)]
        public string Topping { get; set; }

        public virtual Icecream Icecream { get; set; }
    }

}
