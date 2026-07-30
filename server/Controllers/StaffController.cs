using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using Microsoft.AspNetCore.Identity;

[Route("api/v1")]
public class StaffController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IPasswordHasher<Staff> _passwordHasher;

    private readonly Random rand = new Random();

    public StaffController(AppDbContext db, IPasswordHasher<Staff> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("create_staff")]
    public async Task<IActionResult> CreateStaff([FromBody] CreateStaffRequest request)
    {
        try
        {

            var staff = new Staff
            {
              FirstName = request.FirstName,
              LastName = request.LastName,
              Username = request.FirstName.Substring(0, 2).ToLower() + rand.Next(1000, 10000),
              Email = request.Email  
            };

            staff.PasswordHash = _passwordHasher.HashPassword(staff, request.Password);

            _db.Staff.Add(staff);
            await _db.SaveChangesAsync();

            return Ok(new {status = "success"});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var staff = await _db.Staff.FirstOrDefaultAsync(s => s.Email == request.Email);

            if (staff == null)
            {
                return Unauthorized( new {message = "no account with that email found"});
            }

            var result = _passwordHasher.VerifyHashedPassword(
                staff,
                staff.PasswordHash,
                request.Password
            );

            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized( new {message = "incorrect password"});
            }

            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return Ok( new { message = "success", userId = staff.Id, role = "staff", token = token});

        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
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

    [HttpGet("get_library_card_by_id/{id}")]
    public async Task<IActionResult> GetLibraryCardBySsn(Guid id)
    {
        try
        {
            var libraryCard = await _db.LibraryCards.Where(l => l.Id == id).FirstOrDefaultAsync();

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