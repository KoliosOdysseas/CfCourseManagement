namespace CfCourseManagement.Api.Models
{
    public class Course
    {
        public int Id { get; set; } // Πρωτεύον κλειδί (PK) στη βάση
        public string Title { get; set; } // Τίτλος μαθήματος (υποχρεωτικό)
        public string? Description { get; set; } // Περιγραφή (μπορεί να είναι κενό -> ?)
        public int Credits { get; set; }  // Π.χ. ώρες / μονάδες μαθήματος
        public DateTime StartDate  { get; set; }  // Ημερομηνία έναρξης
        public DateTime EndDate { get; set; } // Ημερομηνία λήξης

        // Foreign key προς Teacher (προαιρετικό για να μην σπάσουν τα παλιά δεδομένα)
        public int? TeacherId { get; set; }

        // Navigation property: αυτό το Course ανήκει σε έναν Teacher
        public Teacher? Teacher { get; set; }

        // Many-to-Many: Ένα Course μπορεί να έχει πολλούς Students
        public List<Student> Students { get; set; } = new();

        // Navigation property για τις εγγραφές
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();



    }
}
