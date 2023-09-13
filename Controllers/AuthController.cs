using Erfa.IdentityService.Services;
using Erfa.IdentityService.ViewModels.Login;
using Erfa.IdentityService.ViewModels.RegisterNewEmployee;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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

        [HttpPost("RegisterEmployee")]
        public async Task<IActionResult> RegisterNewEmployeeAsync([FromBody] RegisterEmployeeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterNewEmployeeAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return StatusCode(result.StatusCode, result);

            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model);

                if (result.Response.IsSuccess)
                {
                    Response.Cookies
                .Append("X-Access-Token", new JwtSecurityTokenHandler()
                .WriteToken(((LoginSuccessResult)result).Token),
                new CookieOptions()
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                });
                    return Ok(result.Response);
                }

                return StatusCode(result.Response.StatusCode, result.Response);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
