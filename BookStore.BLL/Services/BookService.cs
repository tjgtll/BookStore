using AutoMapper;
using BookStore.BLL.Services.IServices;
using BookStore.DAL.Entities;
using BookStore.DAL.Repositories.IRepositories;
using BookStore.DTO.DTOs;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Threading;

namespace BookStore.BLL.Services;

public class BookService : IBookService
{
    private readonly IGenericRepository<Book>   _bookRepository;
    private readonly ILogger<BookService>       _logger;
    private readonly IMapper                    _mapper;

    public BookService(ILogger<BookService> logger,
                       IMapper mapper,
                       IGenericRepository<Book> bookRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync(string? title, DateTime? releaseDate)
    {
        Expression<Func<Book, bool>> filter = book => (string.IsNullOrEmpty(title) || 
                                                      book.Title.Contains(title)) &&
                                                      (!releaseDate.HasValue || 
                                                       book.ReleaseDate.Date == releaseDate.Value.Date);
        var booksToReturn = await _bookRepository.GetListAsync(filter);
        _logger.LogInformation("List of {Count} users has been returned", booksToReturn.Count());

        return _mapper.Map<List<BookDto>>(booksToReturn);
    }

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        _logger.LogInformation("User with userId = {UserId} was requested", id);
        var userToReturn = await _bookRepository.GetAsync(x => x.Id == id);

        if (userToReturn is null)
        {
            _logger.LogError("User with userId = {UserId} was not found", id);
            return null;
        }

        return _mapper.Map<BookDto>(userToReturn);
    }
}
