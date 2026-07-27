using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using server.Data;

[Route("api/v1")]
public class StaffController : ControllerBase
{
    private readonly AppDbContext _db;

    public StaffController(AppDbContext db)
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

    [HttpDelete("delete_library_card/{id}")]
    public async Task<IActionResult> DeleteLibraryCard(Guid id)
    {
        try
        {
            var libraryCard = await _db.LibraryCards.Where(l => l.Id == id).FirstOrDefaultAsync();

            if (libraryCard == null)
            {
                return NotFound(new { status = "library card not found"});
            }

            _db.LibraryCards.Remove(libraryCard);
            await _db.SaveChangesAsync();

            return Ok(new {message = "success"});
            
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