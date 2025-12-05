using Microsoft.AspNetCore.Mvc;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "MusicaApi is running!" });
        }
    }
}
