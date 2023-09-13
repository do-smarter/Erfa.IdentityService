using Erfa.IdentityService.Services;
using Erfa.IdentityService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Erfa.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewEmployeeAsync([FromBody] RegisterEmployeeRequestModel model)
        {
            if(ModelState.IsValid)
            {
            var result = await _userService.RegisterNewEmployeeAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest("Some properties are not valid");


        }
    }
}
