using AllYourGoods.Api.Data;
using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AllYourGoods.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync(); 
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
