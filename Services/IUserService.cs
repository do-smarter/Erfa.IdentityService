using Erfa.IdentityService.ViewModels;

namespace Erfa.IdentityService.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterNewEmployeeAsync(RegisterEmployeeRequestModel model);
    }
}
