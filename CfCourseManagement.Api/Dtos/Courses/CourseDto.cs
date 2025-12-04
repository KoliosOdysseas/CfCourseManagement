namespace CfCourseManagement.Api.Dtos.Courses
{
    public class CourseDto
    {
        public int Id { get; set; }                      // ID του course
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Μόνο το TeacherId, όχι ολόκληρο το Teacher object
        public int? TeacherId { get; set; }
    }
}
