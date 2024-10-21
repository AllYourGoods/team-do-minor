using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AutoMapper;

namespace AllYourGoods.Api.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, ResponseOrderDto>(MemberList.Destination);

            CreateMap<OrderHasProduct, ResponseOrderDto>(MemberList.Destination);
            CreateMap<ImageFile, ResponseLogoDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.AltText, opt => opt.MapFrom(src => src.AltText));
            CreateMap<Address, ResponseOrderDto>();

            CreateMap<Restaurant, ResponseOrderDto>();

            CreateMap<CreateOrderDto, Order>(MemberList.Source);
            CreateMap<CreateAddress, Address>(MemberList.Source);
            CreateMap<CreateOrderHasProduct, OrderHasProduct>();
        }
    }
}