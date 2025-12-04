using CfCourseManagement.Api.Dtos.Courses;
using CfCourseManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CfCourseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        // GET: api/course/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            return Ok(course);
        }

        // POST: api/course
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Course data is required");
            }

            try
            {
                var created = await _courseService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = created.Id },
                    created);
            }
            catch (ArgumentException ex)
            {
                // Π.χ. λάθος TeacherId
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Course data is required");
            }

            try
            {
                var success = await _courseService.UpdateAsync(id, dto);
                if (!success)
                {
                    return NotFound($"Course with ID {id} not found");
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // Π.χ. λάθος TeacherId
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _courseService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Course with ID {id} not found");
            }

            return NoContent();
        }
    }
}
