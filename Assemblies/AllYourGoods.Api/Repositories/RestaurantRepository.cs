using AllYourGoods.Api.Data;
using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AllYourGoods.Api.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationContext _context;
    public RestaurantRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurants()
    {
        return await _context.Restaurants
            .Include(r => r.Tags)
            .ToListAsync();
    }
}
