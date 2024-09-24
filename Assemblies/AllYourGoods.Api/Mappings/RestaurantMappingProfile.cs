using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;
using AutoMapper;

namespace AllYourGoods.Api.Mappings;

public class RestaurantMappingProfile : Profile
{
    public RestaurantMappingProfile()
    {
        CreateMap<Restaurant, ViewRestaurantDto>();
    }
}