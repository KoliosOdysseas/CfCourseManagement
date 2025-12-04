namespace CfCourseManagement.Api.Dtos.Students
{
    public class StudentCreateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
