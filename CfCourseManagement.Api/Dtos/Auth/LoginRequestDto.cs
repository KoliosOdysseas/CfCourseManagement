namespace CfCourseManagement.Api.Dtos.Auth
{
    public class LoginRequestDto 
    {
        // DTO whose purpose is to receive the username and password from the client during login.
        public string UserName { get; set; } = string.Empty;

        // The password of the user trying to log in.
        public string Password { get; set; } = string.Empty;
        
    }
}
