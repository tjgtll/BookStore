using BookStore.DTO.DTOs;

namespace BookStore.BLL.Services.IServices;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync(string? title, DateTime? releaseDate);

    Task<BookDto> GetBookByIdAsync(int id);
}
