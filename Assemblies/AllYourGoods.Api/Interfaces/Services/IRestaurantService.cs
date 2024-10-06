using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using System.Linq.Expressions;

namespace AllYourGoods.Api.Interfaces.Services;

public interface IRestaurantService
{
    Task<ResponseRestaurantDto> CreateRestaurantAsync(CreateRestaurantDto restaurantDto);
    Task<ResponseRestaurantDto> GetRestaurantByIdAsync(Guid restaurantId);

    Task<PaginatedList<ResponseRestaurantDto>> GetPaginatedRestaurantsAsync(
        int pageNumber, int pageSize, Expression<Func<Restaurant, bool>>? filter = null);

    Task DeleteRestaurantAsync(Guid id);
    Task<ResponseRestaurantDto> UpdateRestaurantAsync(Guid id, UpdateRestaurantDto updateRestaurantDto);
}
