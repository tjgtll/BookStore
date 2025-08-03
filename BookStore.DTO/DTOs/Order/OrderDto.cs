namespace BookStore.DTO.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderBookDto> Books { get; set; }
    public decimal TotalAmount => Books?.Sum(i => i.Quantity * i.Price) ?? 0;
}
