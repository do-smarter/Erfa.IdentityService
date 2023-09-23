using System.ComponentModel.DataAnnotations;

namespace Erfa.IdentityService.ViewModels.ValidateUserToken
{
    public class ValidateUserTokenRequestModel
    {
        [StringLength(20, MinimumLength = 2)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string[] Roles { get; set; } = new string[0];
    }
}
