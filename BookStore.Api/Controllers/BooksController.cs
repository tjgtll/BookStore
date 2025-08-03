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
    /// Gets all books with optional filtering
    /// </summary>
    /// <param name="title">Filter by book title (partial match)</param>
    /// <param name="releaseDate">Filter by exact release date</param>
    /// <returns>List of book</returns>
    /// <response code="200">Returns the list of books</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] string? title,
                                            [FromQuery] DateTime? releaseDate)
    {
        var books = await _bookService.GetAllBooksAsync(title, releaseDate);
        return Ok(books);
    }

    /// <summary>
    /// Gets a single book by its ID
    /// </summary>
    /// <param name="id">Book ID (must be positive integer)</param>
    /// <returns>Book details</returns>
    /// <response code="200">Returns the requested book</response>
    /// <response code="404">If book with specified ID doesn't exist</response>
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
