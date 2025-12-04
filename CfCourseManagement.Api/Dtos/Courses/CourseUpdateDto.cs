namespace CfCourseManagement.Api.Dtos.Courses
{
    // Τι στέλνει ο client όταν κάνει PUT /api/course/{id}
    public class CourseUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TeacherId { get; set; }
    }
}
