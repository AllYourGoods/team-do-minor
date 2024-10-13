using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;

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
        var order = await _unitOfWork.Repository<Order>().GetAllAsync();

        if (order == null)
        {
            throw new KeyNotFoundException($"not Orders found");
        }

        return _mapper.Map<List<ResponseOrderDto>>(order);
    }


    public async Task<ResponseOrderDto> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId,
           o => o.Address,
           o => o.RestaurantId,
           o => o.DeliveryPersonId,
           o => o.CustomerId,
           o => o.OrderHasProduct!);
           

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with Id: {orderId} not found");
        }

        return _mapper.Map<ResponseOrderDto>(order);
    }
}

