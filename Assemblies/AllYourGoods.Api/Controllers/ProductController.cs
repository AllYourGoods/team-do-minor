using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllYourGoods.Api.Data;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;

namespace AllYourGoods.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ProductsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products
                                         .Include(p => p.ImageFile)
                                         .Include(p => p.CategoryProducts)
                                             .ThenInclude(cp => cp.Category)
                                         .Include(p => p.ProductTags)
                                             .ThenInclude(pt => pt.Tag)
                                         .ToListAsync();

            var productDtos = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                NotAvailable = p.NotAvailable,
                ImageFileId = p.ImageFileId,
                CategoryNames = p.CategoryProducts.Select(cp => cp.Category.Name).ToList(),
                TagNames = p.ProductTags.Select(pt => pt.Tag.Name).ToList()
            });

            return Ok(productDtos);
        }

        // GET: api/products/{id}
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _context.Products
                                        .Include(p => p.ImageFile)
                                        .Include(p => p.CategoryProducts)
                                            .ThenInclude(cp => cp.Category)
                                        .Include(p => p.ProductTags)
                                            .ThenInclude(pt => pt.Tag)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            var productDto = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                NotAvailable = product.NotAvailable,
                ImageFileId = product.ImageFileId,
                CategoryNames = product.CategoryProducts.Select(cp => cp.Category.Name).ToList(),
                TagNames = product.ProductTags.Select(pt => pt.Tag.Name).ToList()
            };

            return Ok(productDto);
        }

        // POST: api/products
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                NotAvailable = model.NotAvailable,
                ImageFileId = model.ImageFileId
            };

            // Add related entities (Categories and Tags) if provided
            foreach (var categoryId in model.CategoryIds)
            {
                product.CategoryProducts.Add(new CategoryHasProduct
                {
                    CategoryId = categoryId,
                    ProductId = product.Id
                });
            }

            foreach (var tagId in model.TagIds)
            {
                product.ProductTags.Add(new ProductHasTag
                {
                    TagId = tagId,
                    ProductId = product.Id
                });
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Product created successfully!", ProductId = product.Id });
        }

        // PUT: api/products/{id}
        [HttpPut("(products/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateProduct model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products
                                        .Include(p => p.CategoryProducts)
                                        .Include(p => p.ProductTags)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.NotAvailable = model.NotAvailable;
            product.ImageFileId = model.ImageFileId;

            // Update categories
            product.CategoryProducts.Clear();
            foreach (var categoryId in model.CategoryIds)
            {
                product.CategoryProducts.Add(new CategoryHasProduct
                {
                    CategoryId = categoryId,
                    ProductId = product.Id
                });
            }

            // Update tags
            product.ProductTags.Clear();
            foreach (var tagId in model.TagIds)
            {
                product.ProductTags.Add(new ProductHasTag
                {
                    TagId = tagId,
                    ProductId = product.Id
                });
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Product updated successfully!", ProductId = product.Id });
        }

        // DELETE: api/products/{id}
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Product deleted successfully!" });
        }
    }
}
