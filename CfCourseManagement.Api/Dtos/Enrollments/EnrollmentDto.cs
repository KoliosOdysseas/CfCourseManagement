namespace CfCourseManagement.Api.Dtos.Enrollments
{
    public class EnrollmentDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime EnrolledAt { get; set; }
        public decimal? Grade { get; set; }
    }
}
