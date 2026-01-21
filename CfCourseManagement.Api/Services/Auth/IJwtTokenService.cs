using CfCourseManagement.Api.Models;


namespace CfCourseManagement.Api.Services.Auth
{
    public interface IJwtTokenService
    {
        (string token, DateTime expiresAtUtc) CreateToken(User user);
        
        
    }
}
