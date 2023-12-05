using Erfa.IdentityService.Services;
using Erfa.IdentityService.ViewModels.Login;
using Erfa.IdentityService.ViewModels.RegisterNewEmployee;
using Erfa.IdentityService.ViewModels.ResetPassword;
using Erfa.IdentityService.ViewModels.ValidateUserToken;
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

        [HttpPost("ValidateUserToken")]
        public async Task<IActionResult> ValidateUserToken([FromBody] ValidateUserTokenRequestModel model)
        {
            if (ModelState.IsValid)
            {
                ValidateUserTokenPayloadRequestModel withToken = new ValidateUserTokenPayloadRequestModel()
                {
                    Roles = model.Roles,
                    UserName = model.UserName,
                    SecurityToken = Request.Cookies["X-Access-Token"].ToString()
                };

                var res = await _userService.ValidateUserTokenAsync(withToken);
                if (res.IsSuccess)
                {
                    return Ok(res);
                }
                return StatusCode(res.StatusCode, res);
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
                                SameSite = SameSiteMode.None,
                                Secure = true,
                                Expires = DateTimeOffset.UtcNow.AddMinutes(20).AddHours(2),
                                Path = "/"
                            });
                    return Ok(result.Response);
                }
                return StatusCode(result.Response.StatusCode, result.Response);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Append("X-Access-Token", new JwtSecurityTokenHandler()
                        .WriteToken(new JwtSecurityToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ2YWxpZCI6ImZhbHNlIn0.j8yuVGFZKf9nHYbKX8x4kFxxCQZ4OJheWxHBrzIyvb0")),
                            new CookieOptions()
                            {
                                HttpOnly = true,
                                SameSite = SameSiteMode.None,
                                Secure = true,
                                Expires = DateTimeOffset.UtcNow.AddMinutes(20),
                                Path = "/"
                            });
            return StatusCode(204);
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

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPassword(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return StatusCode(result.StatusCode, result);
            }
            return BadRequest("Some properties are not valid");
        }
    }
}
