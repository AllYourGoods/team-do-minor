using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using System.Linq.Expressions;

public interface IProductService
{
    Task<ResponseProductDto> CreateProductAsync(CreateProductDto model);
    Task<PaginatedList<ResponseProductDto>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
    Task<ResponseProductDto> GetProductByIdAsync(Guid id);
    Task DeleteProductAsync(Guid id);
    Task<ResponseProductDto> UpdateProductAsync(Guid id, UpdateProductDto model);
}
