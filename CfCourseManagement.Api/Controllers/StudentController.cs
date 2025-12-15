using CfCourseManagement.Api.Dtos.Students;
using CfCourseManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CfCourseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        // Admin ή Teacher μπορούν να δουν τους students
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        // GET: api/student/{id}
        // Admin ή Teacher μπορούν να δουν student
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found");
            }

            return Ok(student);
        }

        // POST: api/student
        // ΜΟΝΟ Admin μπορεί να δημιουργεί students
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Student data is required");
            }

            var created = await _studentService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT: api/student/{id}
        // ΜΟΝΟ Admin μπορεί να κάνει update students
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Student data is required");
            }

            var success = await _studentService.UpdateAsync(id, dto);
            if (!success)
            {
                return NotFound($"Student with ID {id} not found");
            }

            return NoContent();
        }

        // DELETE: api/student/{id}
        // ΜΟΝΟ Admin μπορεί να διαγράφει students
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _studentService.DeleteAsync(id);
                if (!success)
                    return NotFound($"Student with ID {id} not found");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
