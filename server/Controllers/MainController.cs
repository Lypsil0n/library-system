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
    public async Task<IActionResult> Loan([FromBody] LoanDetails loanDetails)
    {
        try 
        {
            var libraryCard = await _db.LibraryCards.FirstOrDefaultAsync(l => l.Id == loanDetails.UserId);
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == loanDetails.BookId);


            if (book == null)
            {
                return NotFound(new {message = "book not found"});
            }
            
            if (book.AmountAvailable <= 0)
            {
                return NotFound(new {message = "book is not available"});
            }

            if (libraryCard == null)
            {
                return NotFound(new {message = "library card not found"});
            }

            if (libraryCard?.CurrentLoanAmount >= 5)
            {
                return BadRequest(new {message = "loan amount exceeded"});
            }

            if (loanDetails.PinCode != libraryCard?.PinCode)
            {
                return Unauthorized(new {message = "pin code is incorrect"});
            }

            Loan loan = new Loan(libraryCard.Id, book.Id);

            _db.Loans.Add(loan);

            libraryCard.CurrentLoanAmount++;
            book.AmountAvailable--;

            await _db.SaveChangesAsync();
            return Ok( new { message = "succeded"});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message + e.StackTrace });
        }
    }

    [HttpPost("return")]
    public async Task<IActionResult> Return([FromBody] LoanDetails loanDetails)
    {
        try 
        {
            var libraryCard = await _db.LibraryCards.FirstOrDefaultAsync(l => l.Id == loanDetails.UserId);
            var loan = await _db.Loans.FirstOrDefaultAsync(l =>
                    l.LibraryCardId == loanDetails.UserId &&
                    l.BookId == loanDetails.BookId);
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == loanDetails.BookId);

            if (book == null)
            {
                return NotFound(new {message = "book not found"});
            }

            if (libraryCard == null)
            {
                return NotFound(new {message = "library card not found"});
            }

            if (loan == null)
            {
                return NotFound(new { message = "loan not found"});
            }

            if (libraryCard?.CurrentLoanAmount <= 0)
            {
                return BadRequest(new {message = "no books to return"});
            }

            if (loanDetails.PinCode != libraryCard?.PinCode)
            {
                return Unauthorized(new {message = "pin code is incorrect"});
            }

            _db.Loans.Remove(loan);

            libraryCard.CurrentLoanAmount--;
            book.AmountAvailable++;

            await _db.SaveChangesAsync();
            return Ok( new { message = "succeded"});
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "error", details = e.Message });
        }
    }
}