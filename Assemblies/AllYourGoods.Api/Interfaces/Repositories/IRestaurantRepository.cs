using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;

namespace AllYourGoods.Api.Interfaces.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurants(FilterRestaurantDto filter = null);
    Task<Restaurant> GetRestaurant(Guid id, FilterRestaurantDto filter = null);
    Task DeleteRestaurant(Guid id);
    Task UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto);
    
    Task<Restaurant> CreateRestaurant(CreateRestaurantDto restaurantDto);
}
