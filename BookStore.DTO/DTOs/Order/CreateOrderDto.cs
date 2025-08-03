using System.ComponentModel.DataAnnotations;

namespace BookStore.DTO.DTOs;

public class CreateOrderDto
{
    [Required]
    [MinLength(1)]
    public List<CreateOrderBookDto> Items { get; set; }
}
