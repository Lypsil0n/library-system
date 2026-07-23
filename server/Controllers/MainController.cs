using Microsoft.AspNetCore.Mvc;

[Route("api/v1")]
public class MainController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new
            {status = "ok"}
        );


    }
}