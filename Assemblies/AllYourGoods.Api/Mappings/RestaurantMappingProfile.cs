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
        //TODO currently not doing anything with status messages we need to fix that and remove these ignores
        CreateMap<Restaurant, ResponseRestaurantDto>(MemberList.Destination)
            .ForMember(dest => dest.StatusCode, opt => opt.Ignore())
            .ForMember(dest => dest.StatusMessage, opt => opt.Ignore());
        CreateMap<Address, ResponseAddressDto>(MemberList.Destination);
        CreateMap<ImageFile, ResponseBannerDto>(MemberList.Destination);
        CreateMap<ImageFile, ResponseLogoDto>(MemberList.Destination);

        CreateMap<CreateRestaurantDto, Restaurant>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<CreateAddress, Address>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<CreateBanner, ImageFile>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<CreateLogo, ImageFile>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

    }
}