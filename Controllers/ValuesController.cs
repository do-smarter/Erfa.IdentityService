
using Microsoft.AspNetCore.Mvc;

namespace Erfa.GatewayApi
{
    [Route("/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("test/{status}", Name = "test")]

        public async Task<IActionResult> GetItemDetails(int status)
        {
            return StatusCode(status, $"Status is {status}");

        }
    }
}
