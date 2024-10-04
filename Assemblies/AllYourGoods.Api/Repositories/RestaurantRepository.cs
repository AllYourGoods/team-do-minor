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
        return await _context.Restaurants
            .Include(r => r.Tags)
            .ToListAsync();
    }

    public async Task<Restaurant> GetRestaurant(Guid id)
    {
        var restaurant = await _context.Restaurants
            .Include(r => r.Tags)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (restaurant == null)
            throw new KeyNotFoundException($"Restaurant with ID {id} not found.");

        return restaurant;
    }

    public async Task<bool> DeleteRestaurant(Guid id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            return false; // Restaurant not found
        }

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();

        return true; // Deletion successful
    }

    public async Task<bool> UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

        if (restaurant == null)
        {
            return false;
        }

        // Map the updated properties
        restaurant.Name = updateRestaurantDto.Name;
        //restaurant.PhoneNumber = updateRestaurantDto.PhoneNumber;
        //restaurant.AboutUs = updateRestaurantDto.AboutUs;
        restaurant.Radius = updateRestaurantDto.Radius;

        // Update Address
        //restaurant.Address.Longitude = updateRestaurantDto.Address.Longitude;
        //restaurant.Address.Latitude = updateRestaurantDto.Address.Latitude;
        //restaurant.Address.HouseNumber = updateRestaurantDto.Address.HouseNumber;
        //restaurant.Address.ZipCode = updateRestaurantDto.Address.ZipCode;
        //restaurant.Address.City = updateRestaurantDto.Address.City;
        //restaurant.Address.StreetName = updateRestaurantDto.Address.StreetName;

        // Update Banner
        //restaurant.Banner.URL = updateRestaurantDto.Banner.URL;
        //restaurant.Banner.AltText = updateRestaurantDto.Banner.AltText;
        //restaurant.Banner.MimeType = updateRestaurantDto.Banner.MimeType;
        //restaurant.Banner.FileSize = updateRestaurantDto.Banner.FileSize;

        //// Update Logo
        //restaurant.Logo.URL = updateRestaurantDto.Logo.URL;
        //restaurant.Logo.AltText = updateRestaurantDto.Logo.AltText;
        //restaurant.Logo.MimeType = updateRestaurantDto.Logo.MimeType;
        //restaurant.Logo.FileSize = updateRestaurantDto.Logo.FileSize;

        //// Update owner info
        //restaurant.OwnerID = updateRestaurantDto.OwnerID;

        // Save changes to database
        await _context.SaveChangesAsync();

        return true;
    }

}
