using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;
using System.Linq.Expressions;

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

    public async Task<ResponseRestaurantDto> CreateRestaurantAsync(CreateRestaurantDto restaurantDto)
    {
        var restaurant = _mapper.Map<Restaurant>(restaurantDto);

        _unitOfWork.Repository<Restaurant>().Add(restaurant);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<ResponseRestaurantDto>(restaurant);
    }

    public async Task<ResponseRestaurantDto> GetRestaurantByIdAsync(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(
            restaurantId,
            r => r.Logo,
            r => r.Address,
            r => r.Banner,
            r => r.Owner!,
            r => r.OpeningsTimes!);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with Id: {restaurantId} not found");
        }

        return _mapper.Map<ResponseRestaurantDto>(restaurant);
    }

    public async Task<PaginatedList<ResponseRestaurantDto>> GetPaginatedRestaurantsAsync(int pageNumber, int pageSize, Expression<Func<Restaurant, bool>>? filter = null)
    {
        var totalCount = await _unitOfWork.Repository<Restaurant>().CountAsync(filter);

        var items = await _unitOfWork.Repository<Restaurant>().GetAllAsync(
            filter: filter,
            orderBy: query => query.OrderBy(r => r.Id),
            skip: (pageNumber - 1) * pageSize,
            take: pageSize,
            includes: new Expression<Func<Restaurant, object>>[]
            {
                r => r.Logo,
                r => r.Address,
                r => r.Banner,
                r => r.Owner!,
                r => r.OpeningsTimes!
            }
        );

        return new PaginatedList<ResponseRestaurantDto>(_mapper.Map<List<ResponseRestaurantDto>>(items), totalCount, pageNumber, pageSize);
    }


    public async Task DeleteRestaurantAsync(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(
            restaurantId,
            r => r.Logo,
            r => r.Address,
            r => r.Banner,
            r => r.Owner!,
            r => r.OpeningsTimes!);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with Id: {restaurantId} not found");
        }

        //TODO check what entities need to be deleted if a restaurant is deleted
        _unitOfWork.Repository<Address>().Delete(restaurant.Address);
        _unitOfWork.Repository<ImageFile>().Delete(restaurant.Logo);
        _unitOfWork.Repository<ImageFile>().Delete(restaurant.Banner);
        if (restaurant.OpeningsTimes != null)
        {
            _unitOfWork.Repository<OpeningsTime>().DeleteRange(restaurant.OpeningsTimes);
        }

        if (restaurant.Owner != null)
        {
            _unitOfWork.Repository<User>().Delete(restaurant.Owner);
        }

        _unitOfWork.Repository<Restaurant>().Delete(restaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task<ResponseRestaurantDto> UpdateRestaurantAsync(Guid restaurantId, UpdateRestaurantDto updateRestaurantDto)
    {
        var restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(
            restaurantId,
            r => r.Logo,
            r => r.Address,
            r => r.Banner,
            r => r.Owner!,
            r => r.OpeningsTimes!);

        if (restaurant == null)
        {
            throw new KeyNotFoundException($"Restaurant with Id: {restaurantId} not found");
        }

        // update Restaurant here with updateRestaurantDto
           restaurant.Name = updateRestaurantDto.Name;
           restaurant.PhoneNumber = updateRestaurantDto.PhoneNumber;
           restaurant.AboutUs = updateRestaurantDto.AboutUs;
           restaurant.Radius = updateRestaurantDto.Radius;
        // etc etc

        _unitOfWork.Repository<Restaurant>().Update(restaurant);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ResponseRestaurantDto>(restaurant);
    }
}

