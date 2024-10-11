using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using Microsoft.AspNetCore.Mvc;

namespace AllYourGoods.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto restaurantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseRestaurantDto = await _restaurantService.CreateRestaurantAsync(restaurantDto);

            return CreatedAtAction(nameof(GetRestaurant), new { id = responseRestaurantDto.Id }, responseRestaurantDto);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedList<ResponseRestaurantDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRestaurants([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var paginatedRestaurants = await _restaurantService.GetPaginatedRestaurantsAsync(pageNumber, pageSize);

            return Ok(paginatedRestaurants);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseRestaurantDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseRestaurantDto>> GetRestaurant(Guid id)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

                return Ok(restaurant);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Restaurant with ID = {id} not found.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            try
            {
                await _restaurantService.DeleteRestaurantAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Restaurant with ID = {id} not found.");
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ResponseRestaurantDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateRestaurant(Guid id, [FromBody] UpdateRestaurantDto updateRestaurantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(id, updateRestaurantDto);

                return Ok(updatedRestaurant);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Restaurant with ID = {id} not found.");
            }
        }
    }
}
