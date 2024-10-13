using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using Microsoft.AspNetCore.Mvc;

namespace AllYourGoods.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
   

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
        
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(ResponseOrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var responseOrderDto = await _orderService.CreateOrderAsync(orderDto);

        return CreatedAtAction(" ", new { id = responseOrderDto.Id }, responseOrderDto);
    }



    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ResponseOrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseOrderDto>> GetOrderAllAsync()
    {
        try { 
        
            var order = await _orderService.GetAllAsync();

            return Ok(order);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"There is no Order available right now.");
        }
    }


}

