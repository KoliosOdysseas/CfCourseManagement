namespace CfCourseManagement.Api.Models
{
    public class Teacher
    {
        public int Id { get; set; }           // Primary Key (Identity)
        public string FullName { get; set; }  // Πλήρες όνομα καθηγητή
        public string? Email { get; set; }    // Προαιρετικό email
        public string? Phone { get; set; }    // Προαιρετικό τηλέφωνο

        // Navigation property: Ένας Teacher έχει πολλά Courses
        public List<Course> Courses { get; set; } = new();
    }
}
