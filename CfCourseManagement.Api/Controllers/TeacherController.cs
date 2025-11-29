using CfCourseManagement.Api.Models;
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
        public IActionResult GetAll()
        {
            var teachers = _teacherService.GetAll();
            return Ok(teachers);
        }

        // GET: api/teacher/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var teacher = _teacherService.GetById(id);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found");
            }

            return Ok(teacher);
        }

        // POST: api/teacher
        [HttpPost]
        public IActionResult Create([FromBody] Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher cannot be null");
            }

            var created = _teacherService.Create(teacher);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/teacher/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is required");
            }

            if (id != teacher.Id)
            {
                return BadRequest("Route id and teacher id do not match");
            }

            var success = _teacherService.Update(id, teacher);
            if (!success)
            {
                return NotFound($"Teacher with ID {id} not found");
            }

            return NoContent();
        }

        // DELETE: api/teacher/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _teacherService.Delete(id);
            if (!success)
            {
                return NotFound($"Teacher with ID {id} not found");
            }

            return NoContent();
        }
    }
}
