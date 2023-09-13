using Erfa.IdentityService.ViewModels.Error;

namespace Erfa.IdentityService.ViewModels.Login
{
    public class LoginFailResult : LoginResult
    {
        public LoginFailResult(ErrorResponse error)
        {
            Response = error;
        }
    }
}
