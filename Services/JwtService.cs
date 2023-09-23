using Erfa.IdentityService.Models;
using Erfa.IdentityService.ViewModels.ValidateUserToken;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Erfa.IdentityService.Services
{
    public class JwtService : IJwService
    {
        private IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidPayload(ValidateUserTokenPayloadRequestModel model)
        {
            JwtSecurityToken token = new JwtSecurityToken(model.SecurityToken);
            var roleClaims = token.Claims.Where(c => c.Type.Equals("Role")).ToArray();
            var userNameClaim = token.Claims.Where(c => c.Type.Equals("UserName"))
                                            .FirstOrDefault().Value;

            if (!model.UserName.Equals(userNameClaim))
            {
                return false;
            }

            var tokenRoles = new string[roleClaims.Length];

            for (int i = 0; i < roleClaims.Length; i++)
            {
                tokenRoles[i] = roleClaims[i].Value.ToString();
            }

            Array.Sort(tokenRoles);

            Array.Sort(model.Roles);

            if (!model.Roles.SequenceEqual(tokenRoles))
            {
                return false;
            }
            return true;
        }

        public JwtSecurityToken NewToken(Employee user, List<string> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim("UserName", user.UserName),
                new Claim("UserId", user.Id),
            };

            foreach (var role in userRoles)
            {
                var r = role;
                claims.Add(new Claim("Role", role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims.ToArray(),
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
