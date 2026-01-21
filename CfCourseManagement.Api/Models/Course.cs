namespace CfCourseManagement.Api.Models
{
    public class Course
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string? Description { get; set; } 
        public int Credits { get; set; }  
        public DateTime StartDate  { get; set; }  
        public DateTime EndDate { get; set; } 

        
        public int? TeacherId { get; set; }

       
        public Teacher? Teacher { get; set; }

        
        public List<Student> Students { get; set; } = new();

        
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();



    }
}
