using CfCourseManagement.Api.Dtos.Courses; 
using CfCourseManagement.Api.Services; 
using Microsoft.AspNetCore.Authorization; 
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
        // Public endpoint 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        // GET: api/course/{id}
        // Public endpoint
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
        // ΜΟΝΟ Admin ή Teacher
        [Authorize(Roles = "Admin,Teacher")]
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
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/course/{id}
        // ΜΟΝΟ Admin ή Teacher
        [Authorize(Roles = "Admin,Teacher")]
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
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/course/{id}
        // ΜΟΝΟ Admin ή Teacher
        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _courseService.DeleteAsync(id);
                if (!success)
                    return NotFound($"Course with ID {id} not found");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
