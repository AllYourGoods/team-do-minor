using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos;
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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewRestaurantDto>), 200)]
        public async Task<ActionResult<IEnumerable<ViewRestaurantDto>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{RestaurantID}")]
        [ProducesResponseType(typeof(ViewRestaurantDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ViewRestaurantDto>> GetRestaurant(Guid RestaurantID)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurant(RestaurantID);
                if (restaurant == null)
                    return NotFound($"Restaurant with ID {RestaurantID} not found.");
                return Ok(restaurant);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{RestaurantID}")]
        [ProducesResponseType(204)] // No content on successful deletion
        [ProducesResponseType(404)] // Not found if the restaurant does not exist
        public async Task<IActionResult> DeleteRestaurant(Guid RestaurantID)
        {
            try
            {
                await _restaurantService.DeleteRestaurant(RestaurantID);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); 
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateRestaurant(Guid id, [FromBody] UpdateRestaurantDto updateRestaurantDto)
        {
            if (updateRestaurantDto == null)
                return BadRequest("UpdateRestaurantDto cannot be null.");

            try
            {
                await _restaurantService.UpdateRestaurant(id, updateRestaurantDto);
              

                return Ok("Restaurant updated successfully."); 
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message); 
            }
        }
        
        [HttpPost]
        [ProducesResponseType(201)] // Resource created
        [ProducesResponseType(400)] // Bad request
        [ProducesResponseType(500)] // Internal server error
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            if (createRestaurantDto == null)
                return BadRequest(new { StatusMessage = "Event is empty.", StatusCode = 400 });
            try
            {
                await _restaurantService.CreateRestaurant(createRestaurantDto);

                return StatusCode(201, new { StatusMessage = "Restaurant created successfully.", StatusCode = 201 }); 
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { StatusMessage = ex.Message, StatusCode = 500 });
            }
        }
    }
    
    
}
