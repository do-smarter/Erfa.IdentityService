using System.IdentityModel.Tokens.Jwt;

namespace Erfa.IdentityService.ViewModels.Login
{
    public class LoginSuccessResult : LoginResult
    {
        public LoginSuccessResult(JwtSecurityToken token, LoginResponseModel loginResponseModel)
        {
            Token = token;
            Response = loginResponseModel;
        }

        public JwtSecurityToken Token { get; set; }
    }
}
