namespace CfCourseManagement.Api.Dtos.Auth
{
    public class RegisterRequestDto
    {
        // DTO whose purpose is to receive user registration data from the client.
        public string UserName { get; set; } = string.Empty;

        // The password for the new user account.
        public string Password { get; set; } = string.Empty;

        // The email address of the new user.
        public string? Email { get; set; }

        // The role to be assigned to the new user (optional).
        public string? RoleName { get; set; }
        
    }
}
