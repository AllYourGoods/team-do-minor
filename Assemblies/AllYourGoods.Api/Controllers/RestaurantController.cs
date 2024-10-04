using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AllYourGoods.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
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
        var restaurant = await _restaurantService.GetRestaurant(RestaurantID);

        if (restaurant == null)
            return NotFound($"Restaurant with ID {RestaurantID} not found.");

        return Ok(restaurant);
    }

    [HttpDelete("{RestaurantID}")]
    [ProducesResponseType(204)] // No content on successful deletion
    [ProducesResponseType(404)] // Not found if the restaurant does not exist
    public async Task<IActionResult> DeleteRestaurant(Guid RestaurantID)
    {
        var restaurant = await _restaurantService.GetRestaurant(RestaurantID);

        if (restaurant == null)
        
            return NotFound($"Restaurant with ID {RestaurantID} not found.");

        await _restaurantService.DeleteRestaurant(RestaurantID);

        return NoContent(); // Return 204 No Content after successful deletion
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateRestaurant(Guid id, [FromBody] UpdateRestaurantDto updateRestaurantDto)
    {
        var updated = await _restaurantService.UpdateRestaurant(id, updateRestaurantDto);

        if (!updated)
        {
            return NotFound("Restaurant not found.");
        }

        return Ok(new
        {
            message = "Restaurant updated successfully.",
            status = "Success"
        });
    }
}
