using System.ComponentModel.DataAnnotations;

namespace Erfa.IdentityService.ViewModels.ValidateUserToken
{
    public class ValidateUserTokenPayloadRequestModel : ValidateUserTokenRequestModel
    {
        [Required]
        public string SecurityToken { get; set; } = string.Empty;
    }
}
