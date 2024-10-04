using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;

namespace AllYourGoods.Api.Interfaces.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurants();
    Task<Restaurant> GetRestaurant(Guid id);
    Task<bool> DeleteRestaurant(Guid id);
    Task<bool> UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto);
}
