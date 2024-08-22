using IcecreamMAUI.API.Data;
using IcecreamMAUI.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IcecreamMAUI.API.Services
{
    public class IcecreamService(DataContext dataContext)
    {
        private readonly DataContext _context = dataContext;

        public async Task<IcecreamDto[]> GetIcecreamsAsync() =>
            await _context.Icecreams.AsNoTracking()
            .Select(i =>
                    new IcecreamDto(
                            i.Id,
                            i.Name,
                            i.Image,
                            i.Price,
                            i.Options
                                .Select(o => new IcecreamOptionDto(o.Flavour, o.Topping)).ToArray()
                        )
            )
            .ToArrayAsync();
    }
}
