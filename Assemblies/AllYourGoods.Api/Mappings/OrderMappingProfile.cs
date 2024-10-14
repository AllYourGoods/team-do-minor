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

            CreateMap<Order, ResponseOrderDto>(MemberList.Destination)
            .ForMember(dest => dest.StatusCode, opt => opt.Ignore())
            .ForMember(dest => dest.StatusMessage, opt => opt.Ignore());
            CreateMap<OrderHasProduct, ResponseOrderDto>(MemberList.Destination);
            CreateMap<ImageFile, ResponseOrderDto>(MemberList.Destination);
            CreateMap<ImageFile, ResponseLogoDto>(MemberList.Destination);
            CreateMap<Address, ResponseOrderDto>(MemberList.Destination);
            CreateMap<Restaurant, ResponseOrderDto>()
            .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Name));

            CreateMap<CreateOrderDto, Order>(MemberList.Source);
            CreateMap<CreateAddress, Address>(MemberList.Source);
            CreateMap<CreateOrderHasProduct, OrderHasProduct>();
            
        }
    }
    
}
