using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;

namespace AllYourGoods.Api.Mappings
{
    public class OrderMappingProfile: Profile
    {

        public OrderMappingProfile()
        {
            CreateMap<Order, ResponseOrderDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderHasProduct));
            CreateMap<Address, ResponseAddressDto>();


            CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<CreateAddress, Address>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
