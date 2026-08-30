using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;

[Route("api/v1")]
public class LibraryController : ControllerBase
{
    private readonly AppDbContext _db;

    public LibraryController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("get_all_books")]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            var books = await _db.Books.ToListAsync();

            if (books.Count == 0)
            {
                return Ok(new { status = "no books found"});
            }

            return Ok(books);
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_book_by_id/{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        try
        {
            var book = await _db.Books.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound(new { status = "book not found"});
            }

            return Ok(book);
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_book_by_isbn/{isbn}")]
    public async Task<IActionResult> GetBookByIsbn(String isbn)
    {
        try
        {
            var book = await _db.Books.Where(b => b.Isbn == isbn).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound(new { status = "book not found"});
            }

            return Ok(book);
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_books_by_keyword")]
    public async Task<IActionResult> GetBooksByKeyword([FromQuery] string keyword)
    {
        try
        {
            var books = await _db.Books
                .Where(b => 
                    b.Title.ToLower().Contains(keyword.ToLower()) || 
                    b.Isbn == keyword ||
                    b.Author.ToLower().Contains(keyword.ToLower()))
                    .ToListAsync();

            if (books.Count == 0)
            {
                return Ok(new { status = "no books found"});
            }

            return Ok(books);
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }


}