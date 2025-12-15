namespace CfCourseManagement.Api.Dtos.Auth
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        // Το username που θα έχει ο χρήστης στο σύστημα.
        // Θα είναι UNIQUE 

        public string Password { get; set; } = string.Empty;
        // Το password σε plain text από το request.
        // θα γίνει hash πριν σωθεί στη βάση.

        public string? Email { get; set; }
        // Προαιρετικό email (δεν είναι υποχρεωτικό για login).

        public string RoleName { get; set; } = "Student";
        // Το όνομα του ρόλου που θα πάρει ο χρήστης.
        // Default = Student (ασφαλής επιλογή).
    }
}
