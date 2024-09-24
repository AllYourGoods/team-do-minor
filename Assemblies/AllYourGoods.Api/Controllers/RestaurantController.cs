using AllYourGoods.Api.Interfaces.Services;
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
}
