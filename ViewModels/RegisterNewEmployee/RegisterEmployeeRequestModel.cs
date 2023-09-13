using System.ComponentModel.DataAnnotations;

namespace Erfa.IdentityService.ViewModels.RegisterNewEmployee
{
    public class RegisterEmployeeRequestModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(1)]
        public string[] Roles { get; set; }
    }
}
