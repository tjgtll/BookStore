using BookStore.DTO.DTOs;

namespace BookStore.BLL.Services.IServices;

public interface IOrderService
{
    public Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int? orderNumber = null,
                                                         DateTime? orderDate = null);
    public Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
}
