using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;

namespace AllYourGoods.Api.Interfaces.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurants();
    Task<Restaurant> GetRestaurant(Guid id);
    Task DeleteRestaurant(Guid id);
    Task UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto);
}
