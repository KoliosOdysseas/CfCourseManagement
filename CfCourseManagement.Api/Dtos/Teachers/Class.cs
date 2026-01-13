namespace CourseManagementSystem.DTOs.Teachers
{
    public class TeacherCourseInfoDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;

        public int StudentsCount { get; set; }

        public List<string> StudentFullNames { get; set; } = new();
    }
}
