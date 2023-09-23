using System.ComponentModel.DataAnnotations;

namespace Erfa.IdentityService.ViewModels.Login
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
