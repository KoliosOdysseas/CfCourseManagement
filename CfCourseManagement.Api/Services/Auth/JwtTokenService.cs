using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CfCourseManagement.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CfCourseManagement.Api.Services.Auth
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        // IConfiguration = πρόσβαση στο appsettings.json

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Κρατάμε το configuration για να διαβάζουμε Jwt:Key, Issuer, Audience κλπ
        }

        public (string token, DateTime expiresAtUtc) CreateToken(User user)
        {


            // 1. Claims (τι πληροφορία κουβαλάει το token)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                // "Subject" → ποιος είναι ο χρήστης (username)

                new Claim(ClaimTypes.Name, user.UserName),
                // Standard claim → χρησιμοποιείται από ASP.NET για User.Identity.Name

                new Claim(ClaimTypes.Role, user.Role.Name),
                // Ο ρόλος του χρήστη (Admin / Teacher / Student)

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // Unique id για το token (για security / traceability)
            };



            // 2. Key + credentials (υπογραφή)
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );
            // Παίρνουμε το secret key από appsettings και το κάνουμε bytes

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            // Λέμε: "υπέγραψε το token με HMAC SHA256"



            // 3. Expiration
            var expiresAtUtc = DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["Jwt:ExpiresMinutes"]!)
            );
            // Πότε λήγει το token (UTC)


            // 4. Δημιουργία JWT 
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiresAtUtc,
                signingCredentials: credentials
            );



            // 5. Serialize token → string
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return (token, expiresAtUtc);
        }
    }
}
