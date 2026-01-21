namespace CfCourseManagement.Api.Dtos.Teachers
{
    public class TeacherUpdateDto
    {
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
