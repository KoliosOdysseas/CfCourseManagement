namespace CfCourseManagement.Api.Dtos.Teachers
{
    // Τι στέλνει ο client όταν δημιουργεί νέο Teacher (POST)
    public class TeacherCreateDto
    {
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
