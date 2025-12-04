using CfCourseManagement.Api.Dtos.Teachers;
using CfCourseManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CfCourseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/teacher
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            return Ok(teachers);
        }

        // GET: api/teacher/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found");
            }

            return Ok(teacher);
        }

        // POST: api/teacher
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Teacher data is required");
            }

            var created = await _teacherService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT: api/teacher/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Teacher data is required");
            }

            var success = await _teacherService.UpdateAsync(id, dto);
            if (!success)
            {
                return NotFound($"Teacher with ID {id} not found");
            }

            return NoContent();
        }

        // DELETE: api/teacher/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _teacherService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Teacher with ID {id} not found");
            }

            return NoContent();
        }
    }
}
