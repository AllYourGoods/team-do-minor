using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;

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

    [HttpGet("paginated")]
    // [Authorize] // this one is generic
    [Authorize(Roles = "group1")]
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
}
