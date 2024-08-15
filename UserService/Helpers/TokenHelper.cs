// Helpers/TokenHelper.cs
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserService.DTOS;

namespace UserService.Helpers
{
    public class TokenHelper
    {
        private readonly string key = "%$#$^%*&^(*&(*(&(^*%$%$#%@$@$@$@$@GFDGD%$#%#%#%$#";

        public string GenerateToken(UserLoginDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Role))
            {
                throw new ArgumentNullException("User, Username, and Role cannot be null or empty");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Role", user.Role),
                new Claim("Username", user.Username)
            };

            var token = new JwtSecurityToken("www.abc.com",
              "www.abc.com",
              claims,
              expires: DateTime.Now.AddMinutes(15000), // token valid for mins
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
