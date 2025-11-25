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
    }
}
