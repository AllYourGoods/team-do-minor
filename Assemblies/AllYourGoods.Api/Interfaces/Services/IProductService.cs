using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using System.Linq.Expressions;

public interface IProductService
{
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto model);
    Task<PaginatedList<ProductResponseDto>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
    Task<RepsonseProductDto> GetProductByIdAsync(Guid id);
    Task DeleteProductAsync(Guid id);
    Task<ProductResponseDto> UpdateProductAsync(Guid id, UpdateProductDto model);
}
