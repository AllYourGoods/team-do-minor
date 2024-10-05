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
    
    public async Task<IEnumerable<Restaurant>> GetRestaurants(FilterRestaurantDto filter = null)
    {
        var query = _context.Restaurants.Include(r => r.Tags).AsQueryable();

        if (filter != null)
        {
            query = ApplyFilters(query, filter);

            query = ApplySorting(query, filter); 

            query = query.Skip((filter.CurrentPage - 1) * filter.PageSize)
                .Take(filter.PageSize); 
        }

        return await query.ToListAsync(); 
    }

    public async Task<Restaurant> GetRestaurant(Guid id)
    {
        var query = _context.Restaurants.Include(r => r.Tags).AsQueryable();
        
        return await query.FirstOrDefaultAsync(r => r.Id == id);
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
    
    public async Task<Restaurant> CreateRestaurant(CreateRestaurantDto restaurantDto)
    {
        var restaurant = new Restaurant
        {
            Id = Guid.NewGuid(),
            Name = restaurantDto.Name,
            OpeningTime = restaurantDto.OpeningTime,
            ClosingTime = restaurantDto.ClosingTime,
            StreetName = restaurantDto.StreetName,
            HouseNumber = restaurantDto.HouseNumber,
            Description = restaurantDto.Description,
            Radius = restaurantDto.Radius,
            ImageLink = restaurantDto.ImageLink,
        };

        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return restaurant;
    }
    
    private IQueryable<Restaurant> ApplyFilters(IQueryable<Restaurant> query, FilterRestaurantDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(r => r.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.Radius.HasValue)
        {
            query = query.Where(r => r.Radius <= filter.Radius.Value);
        }

        if (filter.Tags != null && filter.Tags.Any())
        {
            query = query.Where(r => r.Tags.Any(t => filter.Tags.Contains(t.Name)));
        }

        if (filter.OpeningTime.HasValue && filter.ClosingTime.HasValue)
        {
            query = query.Where(r => r.OpeningTime <= filter.OpeningTime.Value && r.ClosingTime >= filter.ClosingTime.Value);
        }
        else
        {
            if (filter.OpeningTime.HasValue)
            {
                query = query.Where(r => r.OpeningTime <= filter.OpeningTime.Value);
            }

            if (filter.ClosingTime.HasValue)
            {
                query = query.Where(r => r.ClosingTime >= filter.ClosingTime.Value);
            }
        }

        return query;
    }
    
    private IQueryable<Restaurant> ApplySorting(IQueryable<Restaurant> query, FilterRestaurantDto filter)
    {
        switch (filter.SortBy.ToLower())
        {
            case "radius":
                query = filter.SortDirection.ToLower() == "asc" ? query.OrderBy(r => r.Radius) : query.OrderByDescending(r => r.Radius);
                break;
            
            default:
                query = filter.SortDirection.ToLower() == "asc" ? query.OrderBy(r => r.Name) : query.OrderByDescending(r => r.Name);
                break;
        }

        return query;
    }
}

