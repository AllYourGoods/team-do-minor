using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AllYourGoods.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    public TestController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    // [Authorize] // this one is generic
    [Authorize(Roles = "group1")]
    [ProducesResponseType(typeof(IEnumerable<ViewRestaurantDto>), 200)]
    public async Task<ActionResult<IEnumerable<ViewRestaurantDto>>> GetRestaurants()
    {
        var restaurants = await _restaurantService.GetRestaurants();
        return Ok(restaurants);
    }
}
