using BookStore.DAL.Entities.Base;

namespace BookStore.DAL.Entities;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public ICollection<OrderBook> OrderBooks { get; set; }
}
