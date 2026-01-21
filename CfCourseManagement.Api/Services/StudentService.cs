using CfCourseManagement.Api.Data;
using CfCourseManagement.Api.Dtos.Students;
using CfCourseManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CfCourseManagement.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDto>> GetAllAsync()
        {
            var students = await _context.Students
                .AsNoTracking()
                .ToListAsync();

            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Phone = s.Phone,
                Email = s.Email
            }).ToList();
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null) return null;

            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Phone = student.Phone,
                Email = student.Email
            };
        }

        public async Task<StudentDto> CreateAsync(StudentCreateDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Email = dto.Email
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Phone = student.Phone,
                Email = student.Email
            };
        }

        
        public async Task<bool> UpdateAsync(int id, StudentUpdateDto dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return false;

            student.FullName = dto.FullName;
            student.Phone = dto.Phone;
            student.Email = dto.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Υπάρχουν Enrollments για τον student
                throw new InvalidOperationException(
                    "Cannot delete student because they are enrolled in courses."
                );
            }
        }
    }
}
