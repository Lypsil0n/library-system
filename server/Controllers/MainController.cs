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
}