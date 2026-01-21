namespace CfCourseManagement.Api.Dtos.Auth
{
    public class AuthResponseDto
    {
        //DTO whose purpose is to return the JWT token and related info to the client after successful authentication.
        public string Token { get; set; } = string.Empty;

        // The expiration time of the token in UTC.
        public DateTime ExpiresAtUtc { get; set; }

        // The username of the authenticated user.
        public string UserName { get; set; } = string.Empty;

        // The role of the authenticated user.
        public string Role { get; set; } = string.Empty;
       
    }
}
