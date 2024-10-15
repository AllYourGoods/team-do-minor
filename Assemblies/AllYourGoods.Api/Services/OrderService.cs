using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;
using System.Linq.Expressions;

namespace AllYourGoods.Api.Services;


public class OrderService : IOrderService
{
    
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseOrderDto> CreateOrderAsync(CreateOrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);

        _unitOfWork.Repository<Order>().Add(order);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<ResponseOrderDto>(order);
    }

    public async Task<List<ResponseOrderDto>> GetAllAsync()
    {
        var orders = await _unitOfWork.Repository<Order>().GetAllAsync(
            includes : new Expression<Func<Order, object>>[]
            {
            o => o.Restaurant,    
            o => o.Address,
            o => o.Restaurant.Logo
            }
        );

        if (orders == null || !orders.Any())
        {
            throw new KeyNotFoundException("No Orders found");
        }

        return _mapper.Map<List<ResponseOrderDto>>(orders);
    }


    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId,
            o => o.Address);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with Id: {orderId} not found");
        }

        return _mapper.Map<Order>(order);
    }
}

