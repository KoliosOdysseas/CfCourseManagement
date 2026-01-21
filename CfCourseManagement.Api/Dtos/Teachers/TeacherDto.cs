namespace CfCourseManagement.Api.Dtos.Teachers
{
    public class TeacherDto
    {
        public int Id { get; set; }               
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
