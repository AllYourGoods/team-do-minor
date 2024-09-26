using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;
using AutoMapper;

namespace AllYourGoods.Api.Mappings;

public class RestaurantMappingProfile : Profile
{
    public RestaurantMappingProfile()
    {
        CreateMap<Restaurant, ViewRestaurantDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
    }
}