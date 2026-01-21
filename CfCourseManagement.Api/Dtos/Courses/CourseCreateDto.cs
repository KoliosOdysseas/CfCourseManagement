namespace CfCourseManagement.Api.Dtos.Courses
{
    // DTO for creating a new course
    public class CourseCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TeacherId { get; set; }
    }
}
