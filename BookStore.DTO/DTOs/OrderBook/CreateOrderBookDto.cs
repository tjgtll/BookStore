using System.ComponentModel.DataAnnotations;

namespace BookStore.DTO.DTOs;

public class CreateOrderBookDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int BookId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
