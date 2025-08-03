using BookStore.DAL.Entities.Base;

namespace BookStore.DAL.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public ICollection<OrderBook> OrderBooks { get; set; }
}
