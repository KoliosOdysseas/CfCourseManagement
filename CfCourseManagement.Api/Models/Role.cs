namespace CfCourseManagement.Api.Models
{
    public class Role
    {
        public int Id { get; set; }

        // π.χ. "Admin", "Teacher", "Student"
        public string Name { get; set; } = string.Empty;

        // Ένας ρόλος μπορεί να έχει πολλούς χρήστες
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
