namespace CfCourseManagement.Api.Dtos.Courses
{
    // DTO for transferring course data
    public class CourseDto
    {
        public int Id { get; set; }                      // ID του course
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Only for relational mapping
        public int? TeacherId { get; set; }
    }
}
