using BookStore.DAL.Entities;
using BookStore.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.DataContext;

public class BookStoreDbContext : DbContext
{
    public DbSet<OrderBook> OrderBooks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Book> Books { get; set; }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.Price)
                  .HasColumnType("decimal(18, 2)"); 
        });

        modelBuilder.Entity<OrderBook>()
                    .HasKey(oi => new { oi.OrderId, oi.BookId });

        modelBuilder.Entity<OrderBook>()
                    .HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderBooks)
                    .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderBook>()
                    .HasOne(oi => oi.Book)
                    .WithMany(b => b.OrderBooks)
                    .HasForeignKey(oi => oi.BookId);

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "CLR via C#",
                Author = "Рихтер Джеффри",
                ReleaseDate = new DateTime(2019, 1, 1),
                Price = 118.14m,
                CreateAt = DateTime.UtcNow
            },
            new Book
            {
                Id = 2,
                Title = "Microsoft SQL Server 2022",
                Author = "Бондарь Александр",
                ReleaseDate = new DateTime(2024, 1, 1),
                Price = 45.08m,
                CreateAt = DateTime.UtcNow
            }
        );
    }
}
