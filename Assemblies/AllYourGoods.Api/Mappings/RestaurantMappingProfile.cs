using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;

namespace AllYourGoods.Api.Mappings;

public class RestaurantMappingProfile : Profile
{
    public RestaurantMappingProfile()
    {
        CreateMap<Restaurant, ResponseRestaurantDto>();
        CreateMap<Address, ResponseAddressDto>();
        CreateMap<ImageFile, ResponseBannerDto>();
        CreateMap<ImageFile, ResponseLogoDto>();

        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<CreateAddress, Address>();
        CreateMap<CreateBanner, ImageFile>();
        CreateMap<CreateLogo, ImageFile>();
    }
}