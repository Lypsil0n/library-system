using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
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
        var connected = await _db.Database.CanConnectAsync();
        Console.WriteLine(connected);
        
        if (!connected)
        {
            return StatusCode(500, new { status = "error" });
        }

        return Ok(new
            {status = "ok"}
        );


    }
}