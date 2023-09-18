using Erfa.IdentityService.Models;
using Erfa.IdentityService.ViewModels.Common;
using Erfa.IdentityService.ViewModels.Error;
using Erfa.IdentityService.ViewModels.Login;
using Erfa.IdentityService.ViewModels.RegisterNewEmployee;
using Erfa.IdentityService.ViewModels.ResetPassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Erfa.IdentityService.Services
{
    public class UserService : IUserService
    {
        private UserManager<Employee> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;

        public UserService(UserManager<Employee> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<LoginResult> LoginAsync(LoginRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                var res = new ErrorResponse
                {
                    Message = "Invalid username name or password",
                    IsSuccess = false,
                    StatusCode = 400,
                    Errors = new[] { "Invalid credentials" }
                };
                return new LoginFailResult(res);
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                var res = new ErrorResponse
                {
                    Message = "Invalid username name or password",
                    IsSuccess = false,
                    StatusCode = 400,
                    Errors = new[] { "Invalid credentials" }
                };
                return new LoginFailResult(res);
            }


            if (!user.IsActive)
            {
                var res = new ErrorResponse
                {
                    Message = "Yor profile is not active",
                    IsSuccess = false,
                    StatusCode = 403,
                    Errors = new[] { "Inactive profile" }
                };
                return new LoginFailResult(res);
            }

            if (user.IsPasswordChangeRequired)
            {
                var res = new ErrorResponse
                {
                    Message = "You must register your password",
                    IsSuccess = false,
                    StatusCode = 403,
                    Errors = new[] { "New password required" }
                };
                return new LoginFailResult(res);

            }

            var claims = new List<Claim>
        {
                new Claim("UserName", user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),

            };
            
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                var r = role;
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims.ToArray(),
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));


            var response = new LoginResponseModel
            {
                Message = "Welcome",
                IsSuccess = true,
                ExpireDate = token.ValidTo.ToLocalTime(),
                Roles = userRoles.ToArray(),
                UserName = user.UserName,
                StatusCode = 200,
            };
            return new LoginSuccessResult(token, response);
        }


        public async Task<IdentityResponse> RegisterNewEmployeeAsync(RegisterEmployeeRequestModel model)
        {

            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new ErrorResponse
                {
                    Message = "Confirm password doesn't match the password",
                    StatusCode = 400,
                    Errors = new[] { "Password divergence" },

                };

            foreach (var role in model.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return new ErrorResponse
                    {
                        Message = "User not created",
                        IsSuccess = false,
                        Errors = new[] { "Role not allowed" },
                        StatusCode = 400
                    };
                }
            }
            var identityUser = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                IsActive = true,
                IsPasswordChangeRequired = true,
            };

            var result = await _userManager
                .CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                foreach (var role in model.Roles)
                {
                    await _userManager.AddToRoleAsync(identityUser, role);

                }
                return new IdentityResponse
                {
                    Message = "User created",
                    IsSuccess = true,
                    StatusCode = 201
                };
            }

            return new ErrorResponse
            {
                Message = "User not created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToArray(),
                StatusCode = 400
            };
        }

        public async Task<IdentityResponse> ResetPassword(ResetPasswordRequestModel model)
        {
            if (model.NewPassword != model.ConfirmPassword)
                return new ErrorResponse
                {
                    Message = "Confirm password doesn't match the password",
                    StatusCode = 400,
                    Errors = new[] { "Password divergence" },

                };
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new ErrorResponse
                {
                    Message = "Invalid username name or password",
                    IsSuccess = false,
                    StatusCode = 400,
                    Errors = new[] { "Invalid credentials" }
                };
            }

            var result = await _userManager
                .CheckPasswordAsync(user, model.CurrentPassword);

            if (!result)
            {
                return new ErrorResponse
                {
                    Message = "Invalid username name or password",
                    IsSuccess = false,
                    StatusCode = 400,
                    Errors = new[] { "Invalid credentials" }
                };
            }

            if (model.CurrentPassword.Equals(model.ConfirmPassword))
            {
                return new ErrorResponse
                {
                    Message = "Choose different password",
                    IsSuccess = false,
                    StatusCode = 400,
                    Errors = new[] { "Same password provided" }
                };
            }

            if (!user.IsActive)
            {
                return new ErrorResponse
                {
                    Message = "Yor profile is not active",
                    IsSuccess = false,
                    StatusCode = 403,
                    Errors = new[] { "Inactive profile" }
                };
            }

            var updated = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmPassword);

            if (updated.Succeeded)
            {
                user.IsPasswordChangeRequired = false;
                await _userManager.UpdateAsync(user);
                return new IdentityResponse
                {

                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Password changed"
                };
            }

            var errors = new List<string>();
            foreach (var ie in updated.Errors)
            {
                errors.Add(ie.Description);
            }

            return new ErrorResponse
            {
                Message = "Change password failed.",
                IsSuccess = false,
                StatusCode = 400,
                Errors = errors.ToArray()
            };
        }
    }
}
