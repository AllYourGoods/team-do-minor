using AllYourGoods.Api.Models.Dtos;

namespace AllYourGoods.Api.Interfaces.Services;

public interface IRestaurantService
{
    Task<IEnumerable<ViewRestaurantDto>> GetRestaurants();
    Task<ViewRestaurantDto> GetRestaurant(Guid id);
    Task<bool> DeleteRestaurant(Guid id);
    Task<bool> UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto);
}
