using BookStore.BLL.Services;
using BookStore.BLL.Services.IServices;
using BookStore.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : Controller
{
    private readonly IOrderService              _orderService;
    private readonly ILogger<OrdersController>  _logger;

    public OrdersController(ILogger<OrdersController> logger,
                            IOrderService orderService)
    {
        _orderService   = orderService;
        _logger         = logger;
    }

    /// <summary>
    /// Retrieves a list of orders with optional filtering
    /// </summary>
    /// <param name="orderNumber">Filter by order ID (positive integer)</param>
    /// <param name="orderDate">Filter by exact order date</param>
    /// <returns>List of order DTOs</returns>
    /// <response code="200">Returns the list of orders</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery][Range(1, int.MaxValue)] int? orderNumber,
                                         [FromQuery] DateTime? orderDate)
    {
        var orders = await _orderService.GetAllOrdersAsync(orderNumber,orderDate);
        return Ok(orders);
    }

    /// <summary>
    /// Creates a new order
    /// </summary>
    /// <param name="orderDto">Order creation data</param>
    /// <returns>Newly created order</returns>
    /// <response code="201">Returns the created order</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto)
    {
        var order = await _orderService.CreateOrderAsync(orderDto);
        return Ok(order);
    }
}
