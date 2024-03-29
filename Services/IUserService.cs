﻿using Erfa.IdentityService.ViewModels.Common;
using Erfa.IdentityService.ViewModels.Login;
using Erfa.IdentityService.ViewModels.RegisterNewEmployee;
using Erfa.IdentityService.ViewModels.ResetPassword;
using Erfa.IdentityService.ViewModels.ValidateUserToken;

namespace Erfa.IdentityService.Services
{
    public interface IUserService
    {
        Task<LoginResult> LoginAsync(LoginRequestModel model);
        Task<IdentityResponse> RegisterNewEmployeeAsync(RegisterEmployeeRequestModel model);
        Task<IdentityResponse> ResetPassword(ResetPasswordRequestModel model);
        Task<IdentityResponse> ValidateUserTokenAsync(ValidateUserTokenPayloadRequestModel model);
    }
}
