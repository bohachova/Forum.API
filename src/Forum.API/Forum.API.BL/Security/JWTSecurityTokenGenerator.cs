
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Security
{
    public static class JWTSecurityTokenGenerator
    {
        public static string GetToken(int id, string username, UserRole userRole)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, userRole.ToString()),
                new Claim("UserId",id.ToString())
            };
            var jwt = new JwtSecurityToken(
            issuer: JWTAuthOptions.ISSUER,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                    signingCredentials: new SigningCredentials(JWTAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
