namespace CfCourseManagement.Api.Dtos.Teachers
{
    // Τι στέλνει ο client όταν κάνει update Teacher (PUT)
    public class TeacherUpdateDto
    {
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
