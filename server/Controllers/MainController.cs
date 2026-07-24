using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;

[Route("api/v1")]
public class MainController : ControllerBase
{
    private readonly AppDbContext _db;

    public MainController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("health")]
    public async Task<IActionResult> Health()
    {
        try
        {
            var connected = await _db.Database.CanConnectAsync();

            if (!connected)
            {
                return StatusCode(500, new { status = "error", details = "database is offline or unreachable" });
            }

            return Ok(new
                {status = "ok"}
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }


    }

    [HttpPost("loan")]
    public async Task<IActionResult> Loan([FromBody] Loan loan)
    {
        try 
        {
            var libraryCard = await _db.LibraryCards.FirstOrDefaultAsync(l => l.Id == loan.UserId);

            if (libraryCard?.CurrentLoanAmount >= 5)
            {
                return Unauthorized(new {message = "loan amount exceeded"});
            }

            if (loan.PinCode != libraryCard?.PinCode)
            {
                return Unauthorized(new {message = "pin code is incorrect"});
            }

            libraryCard.CurrentLoanAmount++;

            await _db.SaveChangesAsync();
            return Ok( new { message = "succeded"});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }

    [HttpPost("return")]
    public async Task<IActionResult> Return([FromBody] Loan loan)
    {
        try 
        {
            var libraryCard = await _db.LibraryCards.FirstOrDefaultAsync(l => l.Id == loan.UserId);

            if (libraryCard?.CurrentLoanAmount <= 0)
            {
                return BadRequest(new {message = "no books to return"});
            }

            if (loan.PinCode != libraryCard?.PinCode)
            {
                return Unauthorized(new {message = "pin code is incorrect"});
            }

            libraryCard.CurrentLoanAmount--;

            await _db.SaveChangesAsync();
            return Ok( new { message = "succeded"});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }
}