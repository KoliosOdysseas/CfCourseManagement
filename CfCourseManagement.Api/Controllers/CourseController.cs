using CfCourseManagement.Api.Data;
using CfCourseManagement.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CfCourseManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context; // Dependency Injection του ApplicationDbContext
        }

        // GET: api/Course
        [HttpGet]
        public IActionResult GetAll()
            {
            var courses = _context.Courses.ToList();
            return Ok(courses);
        }
        // POST: api/course
        [HttpPost]
        public IActionResult Create([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Course cannot be null");
            }

            _context.Courses.Add(course);   // Προσθέτουμε το course στη βάση
            _context.SaveChanges();         // Αποθηκεύουμε τις αλλαγές

            return CreatedAtAction(nameof(GetAll), new { id = course.Id }, course);
        }
        // GET: api/course/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            return Ok(course);
        }
        // PUT: api/course/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Course updatedCourse)
        {
            if (updatedCourse == null)
            {
                return BadRequest("Invalid course data");
            }
            
            if (id != updatedCourse.Id)
            {
                return BadRequest("Route id and course id do not match");

            }
            // Εύρεση του υπάρχοντος μαθήματος
            var existingCourse = _context.Courses.Find(id);
            if (existingCourse == null)
            {
                return NotFound($"Course with ID {id} not found");
            }
            // Ενημέρωση των πεδίων
            existingCourse.Title = updatedCourse.Title;
            existingCourse.Description = updatedCourse.Description;
            existingCourse.Credits = updatedCourse.Credits;
            existingCourse.StartDate = updatedCourse.StartDate;
            existingCourse.EndDate = updatedCourse.EndDate;

            _context.SaveChanges(); // Αποθήκευση των αλλαγών στη βάση

            return NoContent(); // Επιστρέφουμε 204 No Content

        }
        // DELETE: api/course/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Εύρεση του μαθήματος προς διαγραφή
            var course = _context.Courses.Find(id);

            // Έλεγχος αν το μάθημα υπάρχει
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            // Διαγραφή του μαθήματος
            _context.Courses.Remove(course);

            // Αποθήκευση των αλλαγών στη βάση δεδομένων
            _context.SaveChanges();

            return NoContent(); // Επιστρέφουμε 204 No Content
        }



    }
}
