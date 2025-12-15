namespace CfCourseManagement.Api.Dtos.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        // Το JWT token που θα χρησιμοποιεί ο client σε κάθε επόμενο request.
        // Θα μπαίνει στο header: Authorization: Bearer <Token>

        public DateTime ExpiresAtUtc { get; set; }
        // Πότε λήγει το token (UTC).
        // Ο client μπορεί να ξέρει πότε να κάνει re-login ή refresh.

        public string UserName { get; set; } = string.Empty;
        // Ποιος χρήστης είναι logged in (χρήσιμο για UI).

        public string Role { get; set; } = string.Empty;
        // Ο ρόλος του χρήστη (Admin / Teacher / Student).
        // για authorization στο frontend.
    }
}
