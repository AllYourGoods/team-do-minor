using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using AutoMapper;
using System.Linq.Expressions;

namespace AllYourGoods.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ResponseProductDto>(product);
        }

        public async Task<ResponseProductDto> GetProductByIdAsync(Guid productId)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(
                productId,
                p => p.ImageFile,
                p => p.CategoryProducts,
                p => p.ProductTags
            );

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with Id: {productId} not found");
            }

            return _mapper.Map<ResponseProductDto>(product);
        }

        public async Task<PaginatedList<ResponseProductDto>> GetPaginatedProductsAsync(int pageNumber, int pageSize, Expression<Func<Product, bool>>? filter = null)
        {
            var totalCount = await _unitOfWork.Repository<Product>().CountAsync(filter);

            var items = await _unitOfWork.Repository<Product>().GetAllAsync(
                filter: filter,
                orderBy: query => query.OrderBy(p => p.Id),
                skip: (pageNumber - 1) * pageSize,
                take: pageSize,
                includes: new Expression<Func<Product, object>>[]
                {
                    p => p.ImageFile,
                    p => p.CategoryProducts,
                    p => p.ProductTags
                }
            );

            return new PaginatedList<ResponseProductDto>(_mapper.Map<List<ResponseProductDto>>(items), totalCount, pageNumber, pageSize);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(
                productId,
                p => p.ImageFile,
                p => p.CategoryProducts,
                p => p.ProductTags
            );

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with Id: {productId} not found");
            }

            // If there are any associated entities to delete, do so here
            // For example: If ImageFile should also be deleted, uncomment the next line
            // _unitOfWork.Repository<ImageFile>().Delete(product.ImageFile);
            
            // Remove product from repository
            _unitOfWork.Repository<Product>().Delete(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ResponseProductDto> UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(
                productId,
                p => p.ImageFile,
                p => p.CategoryProducts,
                p => p.ProductTags
            );

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with Id: {productId} not found");
            }

            // Update Product fields with updateProductDto
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.NotAvailable = updateProductDto.NotAvailable;
            // Any other fields to update...

            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ResponseProductDto>(product);
        }

        public Task<PaginatedList<ResponseProductDto>> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
