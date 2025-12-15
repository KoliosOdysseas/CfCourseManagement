using CfCourseManagement.Api.Models;


namespace CfCourseManagement.Api.Services.Auth
{
    public interface IJwtTokenService
    {
        (string token, DateTime expiresAtUtc) CreateToken(User user);
        // Επιστρέφει:
        // 1) το JWT token (string)
        // 2) πότε λήγει (UTC)

        // Το παίρνει ως είσοδο έναν User (από τη βάση) ώστε να βάλει μέσα claims (π.χ. username, role).
    }
}
