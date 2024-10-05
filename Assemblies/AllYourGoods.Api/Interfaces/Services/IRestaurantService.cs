using AllYourGoods.Api.Models.Dtos;

namespace AllYourGoods.Api.Interfaces.Services;

public interface IRestaurantService
{
    Task<IEnumerable<ViewRestaurantDto>> GetRestaurants(FilterRestaurantDto filter = null);
    Task<ViewRestaurantDto> GetRestaurant(Guid id);
    Task DeleteRestaurant(Guid id);
    Task UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto);
    Task CreateRestaurant(CreateRestaurantDto restaurantDto);
}
