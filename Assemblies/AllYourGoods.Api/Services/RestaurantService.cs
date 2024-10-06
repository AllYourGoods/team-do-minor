using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;

namespace AllYourGoods.Api.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork; 
    private readonly IMapper _mapper;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ViewRestaurantDto>> GetRestaurants()
    {
        var restaurants = await _unitOfWork.Repository<Restaurant>().GetAllAsync();
        return _mapper.Map<List<ViewRestaurantDto>>(restaurants);
    }

    public async Task<ViewRestaurantDto> GetRestaurant(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(restaurantId);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with Id: {restaurantId} not found");
        }

        return _mapper.Map<ViewRestaurantDto>(restaurant);
    }

    public async Task DeleteRestaurant(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(restaurantId);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with Id: {restaurantId} not found");
        }

        _unitOfWork.Repository<Restaurant>().Delete(restaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateRestaurant(Guid restaurantId, UpdateRestaurantDto updateRestaurantDto)
    {
        var restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(restaurantId);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with Id: {restaurantId} not found");
        }

        // update Restaurant here with updateRestaurantDto
           restaurant.Name = updateRestaurantDto.Name;
        // etc etc

        _unitOfWork.Repository<Restaurant>().Update(restaurant);
        await _unitOfWork.SaveAsync();
    }
}

