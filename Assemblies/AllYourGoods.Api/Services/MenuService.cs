using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Responses;
using AutoMapper;

namespace AllYourGoods.Api.Services;

public class MenuService : IMenuService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseMenuDto> GetMenuByRestaurantIdAsync(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.Repository<Menu>()
            .GetByIdAsync(restaurantId);

        if (restaurant == null) throw new KeyNotFoundException($"Menu with Id: {restaurantId} not found");

        return _mapper.Map<ResponseMenuDto>(restaurant);
    }
}