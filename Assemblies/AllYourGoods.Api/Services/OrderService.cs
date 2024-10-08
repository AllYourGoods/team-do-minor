using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AutoMapper;


namespace AllYourGoods.Api.Services;


public class OrderService : IOrderService
    {
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository , IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _orderRepository.GetOrders();  
    }

    public async Task CreateOrderAsync(ViewOrderDto viewOrderDto)
    {

        var order = _mapper.Map<Order>(viewOrderDto);

        
        await _orderRepository.AddOrderAsync(order);

        await _orderRepository.SaveChangesAsync();
    }


}

