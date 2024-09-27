using AllYourGoods.Api.Models.Dtos;

namespace AllYourGoods.Api.Interfaces.Services;

public interface IRestaurantService
{
    Task<IEnumerable<ViewRestaurantDto>> GetRestaurants();
}
