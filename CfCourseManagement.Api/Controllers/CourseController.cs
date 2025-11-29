using CfCourseManagement.Api.Models;
using CfCourseManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CfCourseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        // Ο controller ΔΕΝ παίρνει πια ApplicationDbContext, αλλά ICourseService
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService; // Κρατάμε το service για να το χρησιμοποιούμε στα endpoints
        }

        // GET: api/course
        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = _courseService.GetAll();
            return Ok(courses);
        }

        // GET: api/course/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _courseService.GetById(id);

            if (course == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            return Ok(course);
        }

        // POST: api/course
        [HttpPost]
        public IActionResult Create([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Course cannot be null");
            }

            var created = _courseService.Create(course);

            // Τώρα χρησιμοποιούμε το GetById για το CreatedAtAction
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/course/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Course data is required");
            }

            if (id != course.Id)
            {
                return BadRequest("Route id and course id do not match");
            }

            var success = _courseService.Update(id, course);

            if (!success)
            {
                return NotFound($"Course with ID {id} not found");
            }

            return NoContent();
        }

        // DELETE: api/course/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _courseService.Delete(id);

            if (!success)
            {
                return NotFound($"Course with ID {id} not found");
            }

            return NoContent();
        }
    }
}
