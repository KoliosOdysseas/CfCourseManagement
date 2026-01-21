using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CfCourseManagement.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CfCourseManagement.Api.Services.Auth
{
    // JWT token service implementation
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        // Create JWT token for the given user
        public (string token, DateTime expiresAtUtc) CreateToken(User user)
        {


            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
               

                new Claim(ClaimTypes.Name, user.UserName),
                

                new Claim(ClaimTypes.Role, user.Role.Name),
               

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                
            };


            // Generate signing credentials
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            // HMAC SHA256 algorithm for signing
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );




            // Calculate token expiration time
            var expiresAtUtc = DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["Jwt:ExpiresMinutes"]!)
            );



            // Create the JWT token
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiresAtUtc,
                signingCredentials: credentials
            );



            
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return (token, expiresAtUtc);
        }
    }
}
