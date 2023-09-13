using Azure.Core;
using Erfa.IdentityService.Models;
using Erfa.IdentityService.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Erfa.IdentityService.Services
{
    public class UserService : IUserService
    {
        private UserManager<Employee> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserManagerResponse> RegisterNewEmployeeAsync(RegisterEmployeeRequestModel model)
        {

            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };



            foreach (var role in model.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return new UserManagerResponse
                    {
                        Message = "User not created",
                        IsSuccess = false,
                        Errors = new[] { "Role not allowed" },
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



            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                foreach (var role in model.Roles)
                {
                    await _userManager.AddToRoleAsync(identityUser, role);

                }
                return new UserManagerResponse
                {
                    Message = "User created",
                    IsSuccess = true,
                };
            }
            return new UserManagerResponse
            {
                Message = "User not created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToArray()
            };
        }
    }
}
