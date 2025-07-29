using Hotel.Infrastructure.Presistance.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Infrastructure.Helpers
{
    public class GenerateToken
    {
        public static string Generate(int userID, string name, int roleId, string roleName)
        {
            var key = Encoding.ASCII.GetBytes(Constants.SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim("ID", userID.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, roleId.ToString()),       
                new Claim("RoleName", roleName)                     
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
