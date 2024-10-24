using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AllYourGoods.Api.Controllers;

[ApiController]
[Route("api/")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }


    [HttpGet("restaurant/{id}/menu")]
    [ProducesResponseType(typeof(ResponseMenuDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseMenuDto>> GetMenuMenu(Guid id)
    {
        try
        {
            var menu = await _menuService.GetMenuByRestaurantIdAsync(id);

            return Ok(menu);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Menu with RestaurantID = {id} not found.");
        }
    }
}