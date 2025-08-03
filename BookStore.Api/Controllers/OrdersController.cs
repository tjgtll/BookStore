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
    /// Получить список заказов
    /// </summary>
    /// <param name="orderNumber">Айди заказа</param>
    /// <param name="orderDate">кто это будет читать?</param>
    /// <response code="200">Возвращает список заказов</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery][Range(1, int.MaxValue)] int? orderNumber,
                                         [FromQuery] DateTime? orderDate)
    {
        var orders = await _orderService.GetAllOrdersAsync(orderNumber,orderDate);
        return Ok(orders);
    }

    /// <summary>
    /// Создать новый заказ
    /// </summary>
    /// <param name="orderDto">Данные заказа</param>
    /// <response code="201">Возвращает созданный заказ</response>
    /// <response code="400">Если данные невалидны</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto)
    {
        var order = await _orderService.CreateOrderAsync(orderDto);
        return Ok(order);
    }
}
