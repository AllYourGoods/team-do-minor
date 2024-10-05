using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos;
using AutoMapper;

namespace AllYourGoods.Api.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ViewRestaurantDto>> GetRestaurants()
    {
        var restaurants = await _restaurantRepository.GetRestaurants();
        return _mapper.Map<List<ViewRestaurantDto>>(restaurants);
    }

    public async Task<ViewRestaurantDto> GetRestaurant(Guid id)
    {
        var restaurant = await _restaurantRepository.GetRestaurant(id);

        if (restaurant == null)
            throw new Exception("Restaurant not found");

        return _mapper.Map<ViewRestaurantDto>(restaurant);
    }

    public async Task DeleteRestaurant(Guid id)
    {
        var restaurant = await _restaurantRepository.GetRestaurant(id);

        if (restaurant == null)
            throw new Exception("Restaurant not found");

        await _restaurantRepository.DeleteRestaurant(id);
    }

    public async Task UpdateRestaurant(Guid id, UpdateRestaurantDto updateRestaurantDto)
    {
        var restaurant = await _restaurantRepository.GetRestaurant(id);

        if (restaurant == null)
            throw new Exception("Restaurant not found");

        await _restaurantRepository.UpdateRestaurant(id, updateRestaurantDto);
    }
}

