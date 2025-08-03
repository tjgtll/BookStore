using AutoMapper;
using BookStore.BLL.Services.IServices;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.IRepositories;
using BookStore.DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BookStore.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly ILogger<BookService> _logger;
    private readonly IMapper _mapper;

    public OrderService(IGenericRepository<Order> orderRepository,
                        IGenericRepository<Book> bookRepository,
                        ILogger<BookService> logger,
                        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        if (createOrderDto == null || createOrderDto.Items == null || !createOrderDto.Items.Any())
        {
            throw new ArgumentException("Order must contain at least one item");
        }

        var bookIds = createOrderDto.Items.Select(i => i.BookId).Distinct().ToList();
        var existingBooks = await _bookRepository.GetListAsync(b => bookIds.Contains(b.Id));

        if (existingBooks.Count() != bookIds.Count)
        {
            var missingIds = bookIds.Except(existingBooks.Select(b => b.Id));
            throw new KeyNotFoundException($"Books not found: {string.Join(", ", missingIds)}");
        }

        var order = new Order
        {
            OrderDate   = DateTime.UtcNow,
            OrderBooks  = createOrderDto.Items.Select(item => new OrderBook
            {
                BookId      = item.BookId,
                Quantity    = item.Quantity,
            }).ToList()
        };

        await _orderRepository.AddAsync(order);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int? orderNumber = null, DateTime? orderDate = null)
    {
        Expression<Func<Order, bool>> filter = order => (!orderNumber.HasValue || order.Id == orderNumber.Value) && 
                                                        (!orderDate.HasValue || order.OrderDate.Date == orderDate.Value.Date);
        var orderToReturn = await _orderRepository.GetListAsync(filter,
                                                                new List<Func<IQueryable<Order>, IQueryable<Order>>>
                                                                {
                                                                    q => q.Include(o => o.OrderBooks),
                                                                    q => q.Include(o => o.OrderBooks)
                                                                          .ThenInclude(ob => ob.Book)
                                                                });

        _logger.LogInformation("List of {Count} users has been returned", orderToReturn.Count());

        return _mapper.Map<List<OrderDto>>(orderToReturn);
    }
}
