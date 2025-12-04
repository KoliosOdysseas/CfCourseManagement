using System.ComponentModel.DataAnnotations;

namespace CfCourseManagement.Api.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        // Navigation property για τις εγγραφές
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }

}
