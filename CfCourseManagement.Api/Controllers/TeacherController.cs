using CfCourseManagement.Api.Dtos.Teachers; 
using CfCourseManagement.Api.Services; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 

namespace CfCourseManagement.Api.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService; // Service layer dependency.

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService; // DI: παίρνουμε το service από container.
        }

        // GET: api/teacher
        [HttpGet] // Αυτό το endpoint είναι public
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync(); // Παίρνουμε όλους τους teachers.
            return Ok(teachers); // 200 + data
        }

        // GET: api/teacher/{id}
        [HttpGet("{id}")] // Public endpoint 
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id); // Φέρνουμε teacher από service.
            if (teacher == null) 
            {
                return NotFound($"Teacher with ID {id} not found"); // 404
            }

            return Ok(teacher); // 200 + data
        }

        // POST: api/teacher
        [Authorize(Roles = "Admin")] // ΜΟΝΟ Admin μπορεί να δημιουργεί teacher.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
        {
            if (dto == null) // Αν δεν στάλθηκε σώμα request...
            {
                return BadRequest("Teacher data is required"); // 400
            }

            var created = await _teacherService.CreateAsync(dto); // Δημιουργία μέσω service.

            return CreatedAtAction(nameof(GetById), // 201 + Location header προς GET by id
                new { id = created.Id },
                created);
        }

        // PUT: api/teacher/{id}
        [Authorize(Roles = "Admin")] // ΜΟΝΟ Admin μπορεί να κάνει update teacher.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherUpdateDto dto)
        {
            if (dto == null) // Αν δεν στάλθηκε σώμα request...
            {
                return BadRequest("Teacher data is required"); // 400
            }

            var success = await _teacherService.UpdateAsync(id, dto); // Update μέσω service.
            if (!success) // Αν δεν βρέθηκε teacher...
            {
                return NotFound($"Teacher with ID {id} not found"); // 404
            }

            return NoContent(); // 204 (έγινε update, δεν επιστρέφουμε body)
        }

        // DELETE: api/teacher/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _teacherService.DeleteAsync(id);
                if (!success)
                    return NotFound($"Teacher with ID {id} not found");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409
            }
        }

    }
}
