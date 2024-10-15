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
            .ForMember(dest => dest.StatusMessage, opt => opt.Ignore())
            .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Address.StreetName)) 
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.Address.HouseNumber))
            .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Restaurant.Logo));


            CreateMap<OrderHasProduct, ResponseOrderDto>(MemberList.Destination);
            CreateMap<ImageFile, ResponseLogoDto>(MemberList.Destination)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.AltText, opt => opt.MapFrom(src => src.AltText));
            CreateMap<Address, ResponseOrderDto>();
            
            CreateMap<Restaurant, ResponseOrderDto>()
            .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo));



            CreateMap<CreateOrderDto, Order>(MemberList.Source);
            

            CreateMap<CreateAddress, Address>(MemberList.Source);
            CreateMap<CreateOrderHasProduct, OrderHasProduct>();

        }
        
    }
    
}
