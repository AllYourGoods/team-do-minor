using AllYourGoods.Api.Models;

namespace AllYourGoods.Api.Interfaces.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurants();
}
