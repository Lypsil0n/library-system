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

    [HttpPost("create_library_card")]
    public async Task<IActionResult> CreateLibraryCard([FromBody]LibraryCard libraryCard)
    {
        try
        {
            _db.LibraryCards.Add(libraryCard);
            await _db.SaveChangesAsync();

            return Ok( new {status = "success"});
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_all_library_cards")]
    public async Task<IActionResult> GetAllLibraryCards()
    {
        try
        {
            var libraryCards = await _db.LibraryCards.ToListAsync();
            return Ok(libraryCards);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_library_card_by_ssn/{ssn}")]
    public async Task<IActionResult> GetLibraryCardBySsn(string ssn)
    {
        try
        {
            var libraryCard = await _db.LibraryCards.Where(l => l.Ssn == ssn).FirstOrDefaultAsync();

            if (libraryCard == null)
            {
                return NotFound(new { status = "library card not found"});
            }

            return Ok(libraryCard);
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_all_books")]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            var books = await _db.Books.ToListAsync();

            if (books.Count == 0)
            {
                return NotFound(new { status = "no books found"});
            }

            return Ok(books);
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_book_by_title")]
    public async Task<IActionResult> GetBookByTitle([FromQuery] string title)
    {
        try
        {
            var book = await _db.Books.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToListAsync();

            return Ok(book);
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpPost("create_book")]
    public async Task<IActionResult> CreateBook([FromBody]Book book)
    {
        try
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            return Ok( new {status = "success"});
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }
}