using Microsoft.AspNetCore.Identity;

namespace Erfa.IdentityService.Models
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPasswordChangeRequired { get; set; }
    }
}
