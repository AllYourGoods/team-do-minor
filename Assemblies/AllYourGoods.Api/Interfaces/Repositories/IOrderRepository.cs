using AllYourGoods.Api.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace AllYourGoods.Api.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrders();
    Task AddOrderAsync(Order order);  
    Task SaveChangesAsync();
}