using Erfa.IdentityService.ViewModels.Common;
using Erfa.IdentityService.ViewModels.Login;
using Erfa.IdentityService.ViewModels.RegisterNewEmployee;

namespace Erfa.IdentityService.Services
{
    public interface IUserService
    {
        Task<LoginResult> LoginAsync(LoginRequestModel model);
        Task<IdentityResponse> RegisterNewEmployeeAsync(RegisterEmployeeRequestModel model);
    }
}
