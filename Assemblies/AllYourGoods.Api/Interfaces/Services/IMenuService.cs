using AllYourGoods.Api.Models.Dtos.Responses;

namespace AllYourGoods.Api.Interfaces.Services;

public interface IMenuService
{
    public Task<ResponseMenuDto> GetMenuByRestaurantIdAsync(Guid restaurantId);
}