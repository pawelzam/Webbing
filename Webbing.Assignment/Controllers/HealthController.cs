using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webbing.Assignment.Api.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {

        [HttpGet("check")]
        public IActionResult Check()
        {
            return Ok(Guid.NewGuid().ToString());
        }

    }
}
