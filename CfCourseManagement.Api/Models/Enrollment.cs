namespace CfCourseManagement.Api.Models
{
    public class Enrollment
    {
        // Composite key: StudentId + CourseId

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // Προαιρετικά extra πεδία
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public decimal? Grade { get; set; }    // ή int? για βαθμό
    }
}
