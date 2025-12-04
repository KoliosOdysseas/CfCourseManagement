namespace CfCourseManagement.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        // unique username
        public string UserName { get; set; } = string.Empty;

        // saved as a hash for security
        public string PasswordHash { get; set; } = string.Empty;

        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        // one user has one role
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
