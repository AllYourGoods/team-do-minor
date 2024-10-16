using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;

namespace AllYourGoods.Api.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Mapping for Product entity to ResponseProductDto
            CreateMap<Product, ResponseProductDto>(MemberList.Destination)
                .ForMember(dest => dest.StatusCode, opt => opt.Ignore())
                .ForMember(dest => dest.StatusMessage, opt => opt.Ignore());
            
            // Map for CreateProductDto to Product entity
            CreateMap<CreateProductDto, Product>(MemberList.Source)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            
            // Map for any other DTOs related to Product as needed
            // Assuming you may have categories and tags to map
            CreateMap<Category, ResponseCategoryDto>(MemberList.Destination);
            CreateMap<Tag, ResponseTagDto>(MemberList.Destination);

            // If you have create DTOs for categories or tags
            CreateMap<CreateCategoryD, Category>(MemberList.Source)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<CreateTag, Tag>(MemberList.Source)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
