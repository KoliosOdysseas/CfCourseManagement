using CfCourseManagement.Api.Dtos.Enrollments;
using CfCourseManagement.Api.Services;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;

namespace CfCourseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // POST: api/enrollment
        // Admin or Student can enroll
        [Authorize(Roles = "Admin,Student")]
        [HttpPost]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Enrollment data is required.");
            }

            try
            {
                var enrollment = await _enrollmentService.EnrollAsync(dto);
                return Ok(enrollment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        //delete: api/enrollment?studentId=5&courseId=2
        // Admin or Student can unenroll
        [Authorize(Roles = "Admin,Student")]
        [HttpDelete]
        public async Task<IActionResult> Unenroll(
            [FromQuery] int studentId,
            [FromQuery] int courseId)
        {
            var success = await _enrollmentService.UnenrollAsync(studentId, courseId);
            if (!success)
            {
                return NotFound("Enrollment not found for given student and course.");
            }

            return NoContent();
        }

        // GET: api/enrollment/course/2/students
        // Admin or Teacher can see students of a course
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("course/{courseId}/students")]
        public async Task<IActionResult> GetStudentsByCourse(int courseId)
        {
            try
            {
                var students = await _enrollmentService.GetStudentsByCourseAsync(courseId);
                return Ok(students);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/enrollment/student/5/courses
        // Admin, Teacher or Student can see courses of a student
        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet("student/{studentId}/courses")]
        public async Task<IActionResult> GetCoursesByStudent(int studentId)
        {
            try
            {
                var courses = await _enrollmentService.GetCoursesByStudentAsync(studentId);
                return Ok(courses);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
