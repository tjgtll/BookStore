using System.ComponentModel.DataAnnotations;

namespace BookStore.DTO.DTOs;

public class OrderBookDto
{
    public int BookId { get; set; }
    public string Title { get; set; }  
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
