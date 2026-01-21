using CfCourseManagement.Api.Data;
using CfCourseManagement.Api.Dtos.Courses;
using CfCourseManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CfCourseManagement.Api.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseDto>> GetAllAsync()
        {
            var courses = await _context.Courses
                .AsNoTracking()
                .ToListAsync();

            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Credits = c.Credits,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                TeacherId = c.TeacherId
            }).ToList();
        }
        // Get a course by ID
        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Credits = course.Credits,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                TeacherId = course.TeacherId
            };
        }
        // Create a new course
        public async Task<CourseDto> CreateAsync(CourseCreateDto dto)
        {
            // check if TeacherId exists
            if (dto.TeacherId.HasValue)
            {
                var teacherExists = await _context.Teachers
                    .AnyAsync(t => t.Id == dto.TeacherId.Value);

                if (!teacherExists)
                {
                    throw new ArgumentException(
                        $"Teacher with ID {dto.TeacherId.Value} does not exist.");
                }
            }

            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Credits = dto.Credits,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TeacherId = dto.TeacherId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Credits = course.Credits,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                TeacherId = course.TeacherId
            };
        }
        // Update an existing course
        public async Task<bool> UpdateAsync(int id, CourseUpdateDto dto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return false;

            // check if TeacherId exists
            if (dto.TeacherId.HasValue)
            {
                var teacherExists = await _context.Teachers
                    .AnyAsync(t => t.Id == dto.TeacherId.Value);

                if (!teacherExists)
                {
                    throw new ArgumentException(
                        $"Teacher with ID {dto.TeacherId.Value} does not exist.");
                }
            }

            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Credits = dto.Credits;
            course.StartDate = dto.StartDate;
            course.EndDate = dto.EndDate;
            course.TeacherId = dto.TeacherId;

            await _context.SaveChangesAsync();
            return true;
        }
        // Delete a course
        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Assuming the exception is due to foreign key constraints (e.g., students enrolled in the course)
                throw new InvalidOperationException(
                    "Cannot delete course because there are students enrolled in it."
                );
            }
        }

    }
}
