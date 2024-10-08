using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AllYourGoods.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ViewOrderDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<ViewOrderDto>>> GetOrders()
    {
        var orders = await _orderService.GetOrders();

        if (orders == null || !orders.Any())
            return NotFound("No orders found.");

        var viewOrderDtos = _mapper.Map<IEnumerable<ViewOrderDto>>(orders);

        return Ok(viewOrderDtos);
    }

    [HttpPost]
    [ProducesResponseType(201)] 
    [ProducesResponseType(400)] 
    public async Task<IActionResult> CreateOrder([FromBody] ViewOrderDto viewOrderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        await _orderService.CreateOrderAsync(viewOrderDto);

        return CreatedAtAction(nameof(CreateOrder), new { id = viewOrderDto.RestaurantId }, viewOrderDto);
    }
}

