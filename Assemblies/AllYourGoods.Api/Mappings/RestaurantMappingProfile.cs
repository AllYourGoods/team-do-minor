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
        CreateMap<Restaurant, ResponseRestaurantDto>(MemberList.Destination);
        CreateMap<Address, ResponseAddressDto>(MemberList.Destination);
        CreateMap<ImageFile, ResponseBannerDto>(MemberList.Destination);
        CreateMap<ImageFile, ResponseLogoDto>(MemberList.Destination);

        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<CreateAddress, Address>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<CreateBanner, ImageFile>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<CreateLogo, ImageFile>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

    }
}