using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;

namespace AllYourGoods.Api.Interfaces.Services;

    public interface IOrderService
    {
    Task<IEnumerable<Order>> GetOrders();
    Task CreateOrderAsync(ViewOrderDto viewOrderDto);

}

