using AllYourGoods.Api.Models;
using AutoMapper;

namespace AllYourGoods.Api.Mappings
{
    public class OrderMappingProfile: Profile
    {

        public OrderMappingProfile()
        {
             CreateMap<Order, ViewOrderDto>()
            .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.MenuItems.Select(m => m.OrderId)));
        }
    }
}
