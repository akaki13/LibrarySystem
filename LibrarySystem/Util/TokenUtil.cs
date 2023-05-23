using LibrarySystemModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibrarySystem.Util
{
    public static class TokenUtil
    {
        public static string CreateToken(User user,RoleUser role , IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role.Role.Title)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var Token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.Now.AddDays(2),
                 signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public static string CreateToken(string id, IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var Token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.Now.AddDays(2),
                 signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public static int DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var jwtData = jwt.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return int.Parse(jwtData.Value);
        }
    }
}
