using System.ComponentModel.DataAnnotations;

namespace CfCourseManagement.Api.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;


        public string? Phone { get; set; }



        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }

}
