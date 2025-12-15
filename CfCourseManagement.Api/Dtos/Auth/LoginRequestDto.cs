namespace CfCourseManagement.Api.Dtos.Auth
{
    public class LoginRequestDto 
    {
        public string UserName { get; set; } = string.Empty;
        // Το username που πληκτρολογεί ο χρήστης (default empty για να μην είναι null).

        public string Password { get; set; } = string.Empty;
        // Το password που πληκτρολογεί ο χρήστης (θα το συγκρίνουμε με PasswordHash στη βάση).
    }
}
