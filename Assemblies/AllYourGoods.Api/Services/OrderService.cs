using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;
using System.Linq.Expressions;

namespace AllYourGoods.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ResponseOrderDto>> GetAllAsync()
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

            if (orders == null || !orders.Any())
            {
                throw new KeyNotFoundException("No Orders found");
            }

            var responseOrders = _mapper.Map<List<ResponseOrderDto>>(orders);
            return responseOrders ?? new List<ResponseOrderDto>();
        }

        public async Task<List<ResponseOrderDto>> GetUserAllAsync(Guid DPersonId)
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync(o => o.DeliveryPersonId == DPersonId);

            if (orders == null || !orders.Any())
            {
                throw new KeyNotFoundException("No Orders found");
            }

            var responseOrders = _mapper.Map<List<ResponseOrderDto>>(orders);
            return responseOrders ?? new List<ResponseOrderDto>();
        }

        public async Task<ResponseOrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            if (order == null)
            {
                throw new InvalidOperationException("Mapping to Order failed, returned null.");
            }

            _unitOfWork.Repository<Order>().Add(order);
            await _unitOfWork.SaveAsync();
            var responseOrder = _mapper.Map<ResponseOrderDto>(order);

            return responseOrder ?? throw new InvalidOperationException("Mapping to ResponseOrderDto failed, returned null.");
        }

        public async Task<ResponseOrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId, o => o.Address);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with Id: {orderId} not found");
            }

            var responseOrder = _mapper.Map<ResponseOrderDto>(order);
            
            return responseOrder; 
        }
    }
}
