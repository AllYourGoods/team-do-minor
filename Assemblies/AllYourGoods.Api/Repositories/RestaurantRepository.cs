using AllYourGoods.Api.Data;
using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;
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
        return await _context.Restaurants.Include(r => r.Tags).ToListAsync();
    }

    public async Task<Restaurant> GetRestaurant(Guid id)
    {
        return await _context.Restaurants.Include(r => r.Tags).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task DeleteRestaurant(Guid id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        // Only map the received data and update the database.
        restaurant.Name = updateRestaurantDto.Name;
        restaurant.Radius = updateRestaurantDto.Radius;
        // Map other properties...

        await _context.SaveChangesAsync();
    }
}

