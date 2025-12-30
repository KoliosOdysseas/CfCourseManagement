namespace CfCourseManagement.Api.Dtos.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
