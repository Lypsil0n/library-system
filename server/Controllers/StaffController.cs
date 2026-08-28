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

    public async Task<bool> TokenCheck(Guid userId, string token)
    {
        var staff = await _db.Staff.FindAsync(userId);

        if (staff == null)
            return false;

        if (string.IsNullOrWhiteSpace(token))
            return false;

        if (string.IsNullOrWhiteSpace(staff.TokenHash))
            return false;

        var result = _passwordHasher.VerifyHashedPassword(
            staff,
            staff.TokenHash,
            token
        );


        return result == PasswordVerificationResult.Success;
    }

    [HttpGet("is_current_token_valid")]
    public async Task<IActionResult> IsCurrentTokenValid([FromHeader] Guid userId, [FromHeader] string token)
    {
        var result = await TokenCheck(userId, token);

        if (!result)
        {
            return Unauthorized(new { valid = result, message = "not ok"});
        }

        return Ok(new { valid = result, message = "ok"});
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

    [HttpGet("get_staff_by_id")]
    public async Task<IActionResult> GetStaffById([FromHeader] Guid userId, [FromHeader] string token)
    {
        if (!await TokenCheck(userId, token))
        {
            return Unauthorized(new { message = "token is invalid"});
        }

        try
        {
            var staff = await _db.Staff.FindAsync(userId);

            return Ok(staff);
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
                return Unauthorized( new {message = "invalid email or password"});
            }

            var result = _passwordHasher.VerifyHashedPassword(
                staff,
                staff.PasswordHash,
                request.Password
            );

            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized( new {message = "invalid email or password"});
            }

            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            string tokenHash = _passwordHasher.HashPassword(staff, token);

            staff.TokenHash = tokenHash;
            await _db.SaveChangesAsync();

            return Ok( new { message = "success", data = new { userId = staff.Id, role = "staff", token = token}});

        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        try
        {   
            var staff = await _db.Staff.FindAsync(request.UserId);

            if (staff == null)
            {
                return Unauthorized( new {message = "invalid user"});
            }

            staff.TokenHash = null;

            await _db.SaveChangesAsync();

            return Ok( new { message = "success" });

        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpPost("create_library_card")]
    public async Task<IActionResult> CreateLibraryCard([FromHeader] Guid userId, [FromHeader] string token, [FromBody]LibraryCard libraryCard)
    {   
        if (!await TokenCheck(userId, token))
        {
            return Unauthorized(new { message = "token is invalid"});
        }

        try
        {
            if (!libraryCard.PostalCode.Contains(" "))
            {
                libraryCard.PostalCode = libraryCard.PostalCode.Insert(libraryCard.PostalCode.Length - 2, " ");
            }

            _db.LibraryCards.Add(libraryCard);
            await _db.SaveChangesAsync();

            return Ok( new {status = "success", id = libraryCard.Id});
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpGet("get_all_library_cards")]
    public async Task<IActionResult> GetAllLibraryCards([FromHeader] Guid userId, [FromHeader] string token)
    {
        if (!await TokenCheck(userId, token))
        {
            return Unauthorized(new { message = "token is invalid"});
        }

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
    public async Task<IActionResult> GetLibraryCardBySsn([FromHeader] Guid userId, [FromHeader] string token, string ssn)
    {
        if (!await TokenCheck(userId, token))
        {
            return Unauthorized(new { message = "token is invalid"});
        }

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
    public async Task<IActionResult> GetLibraryCardById([FromHeader] Guid userId, [FromHeader] string token, Guid id)
    {
        if (!await TokenCheck(userId, token))
        {
            return Unauthorized(new { message = "token is invalid"});
        }

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
    public async Task<IActionResult> DeleteLibraryCard([FromHeader] Guid userId, [FromHeader] string token, Guid id)
    {
        if (!await TokenCheck(userId, token))
        {
            return Unauthorized(new { message = "token is invalid"});
        }

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
    public async Task<IActionResult> CreateBook([FromHeader] Guid userId, [FromHeader] string token, [FromBody]Book book)
    {
        if (!await TokenCheck(userId, token))
        {
            return Unauthorized(new { message = "token is invalid"});
        }

        if (book is null)
        {
            return BadRequest(new { status = "error", details = "Request body is missing or invalid." });
        }

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