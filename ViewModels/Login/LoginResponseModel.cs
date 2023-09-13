using Erfa.IdentityService.ViewModels.Common;

namespace Erfa.IdentityService.ViewModels.Login
{
    public class LoginResponseModel : IdentityResponse
    {
        public DateTime? ExpireDate { get; set; }
        public string[] Roles { get; set; } = new string[0];
        public string UserName { get; set; } = string.Empty;
    }
}
