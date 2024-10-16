using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using Microsoft.AspNetCore.Mvc;

namespace AllYourGoods.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ResponseProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseProductDto = await _productService.CreateProductAsync(productDto);

            return CreatedAtAction(nameof(GetProduct), new { id = responseProductDto.Id }, responseProductDto);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedList<ResponseProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var paginatedProducts = await _productService.GetPaginatedProductsAsync(pageNumber, pageSize);

            return Ok(paginatedProducts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseProductDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseProductDto>> GetProduct(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product with ID = {id} not found.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product with ID = {id} not found.");
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ResponseProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(id, model);

                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product with ID = {id} not found.");
            }
        }
    }
}
