using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;

namespace AllYourGoods.Api.Interfaces.Services;

public interface IOrderService
{
    Task<List<ResponseOrderDto>> GetAllAsync();
    Task<ResponseOrderDto> CreateOrderAsync(CreateOrderDto OrderDto);
    Task<ResponseOrderDto> GetOrderByIdAsync(Guid orderId);
}
