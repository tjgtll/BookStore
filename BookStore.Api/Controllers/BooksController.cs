using BookStore.BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : Controller
{
    private readonly IBookService               _bookService;
    private readonly ILogger<BooksController>   _logger;

    public BooksController(ILogger<BooksController> logger,
                           IBookService bookService)
    {
        _bookService = bookService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список кинг
    /// </summary>
    /// <param name="title">Имя книги</param>
    /// <param name="releaseDate">Дата выпуска книги</param>
    /// <response code="200">Возвращает список книг</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] string? title,
                                            [FromQuery] DateTime? releaseDate)
    {
        var books = await _bookService.GetAllBooksAsync(title, releaseDate);
        return Ok(books);
    }

    /// <summary>
    /// Получить книгу по айди
    /// </summary>
    /// <param name="id">Айди книги</param>
    /// <response code="200">Возврашает книгу</response>
    /// <response code="404">Если по айди книги нет</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([Range(1, int.MaxValue)] int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null) return NotFound();
        return Ok(book);
    }
}
