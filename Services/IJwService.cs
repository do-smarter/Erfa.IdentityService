using Erfa.IdentityService.Models;
using Erfa.IdentityService.ViewModels.ValidateUserToken;
using System.IdentityModel.Tokens.Jwt;

namespace Erfa.IdentityService.Services
{
    public interface IJwService
    {
        JwtSecurityToken NewToken(Employee user, List<string> userRoles);
        bool IsValidPayload(ValidateUserTokenPayloadRequestModel model);
    }
}
