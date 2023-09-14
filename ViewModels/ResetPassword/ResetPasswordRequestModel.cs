using System.ComponentModel.DataAnnotations;

namespace Erfa.IdentityService.ViewModels.ResetPassword
{
    public class ResetPasswordRequestModel
    {
        [Required]
        [StringLength(6, MinimumLength = 2)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
