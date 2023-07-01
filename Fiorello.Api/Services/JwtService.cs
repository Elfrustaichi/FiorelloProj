using Fiorello.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Fiorello.Api.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(AppUser user,IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("Fullname",user.Fullname)
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:Secret"]));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken
                (
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddDays(10),
                issuer: _configuration["Security:Issuer"],
                audience: _configuration["Security:Audience"]
                );

            var stringToken=new JwtSecurityTokenHandler().WriteToken(token);

            return stringToken;
        }
    }
}
