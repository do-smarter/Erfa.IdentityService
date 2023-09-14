using System.ComponentModel.DataAnnotations;

namespace Erfa.IdentityService.ViewModels.Login
{
    public class LoginRequestModel
    {
        [Required]
        [StringLength(6, MinimumLength = 2)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;
    }
}
