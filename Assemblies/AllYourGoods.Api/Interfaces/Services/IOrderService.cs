using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;

namespace AllYourGoods.Api.Interfaces.Services;

    public interface IOrderService
    {
    Task<List<ResponseOrderDto>> GetAllAsync();
    Task<ResponseOrderDto> CreateOrderAsync(CreateOrderDto orderDto);
    Task<ResponseOrderDto> GetOrderByIdAsync(Guid OrderId);
}

