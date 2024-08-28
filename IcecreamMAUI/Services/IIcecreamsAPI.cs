using IcecreamMAUI.Shared.Dtos;
using Refit;

namespace IcecreamMAUI.Services
{
    public interface IIcecreamsAPI
    {
        [Get("/api/icecreams")]
        Task<IcecreamDto[]> GetIcecreamsAsync();
    }
}
