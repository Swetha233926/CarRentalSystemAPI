using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalSystemAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
    }
    public class JwtService:IJwtService
    {
        private readonly IConfiguration configuration;
        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(string username, string role)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var expiresInMinutes = jwtSettings["ExpiresInMinutes"];
            if (string.IsNullOrEmpty(expiresInMinutes))
            {
                expiresInMinutes = "60";
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(expiresInMinutes)),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
