using CfCourseManagement.Api.Data;
using CfCourseManagement.Api.Dtos.Courses;
using CfCourseManagement.Api.Dtos.Enrollments;
using CfCourseManagement.Api.Dtos.Students;
using CfCourseManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CfCourseManagement.Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EnrollmentDto> EnrollAsync(EnrollmentCreateDto dto)
        {
            // 1.checks before enrolling a student in a course
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == dto.StudentId);

            if (!studentExists)
            {
                throw new ArgumentException($"Student with ID {dto.StudentId} does not exist.");
            }

            // 2.check if course exists
            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == dto.CourseId);

            if (!courseExists)
            {
                throw new ArgumentException($"Course with ID {dto.CourseId} does not exist.");
            }

            // 3.check if the student is already enrolled in the course
            var alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e => e.StudentId == dto.StudentId && e.CourseId == dto.CourseId);

            if (alreadyEnrolled)
            {
                throw new InvalidOperationException("Student is already enrolled in this course.");
            }

            // 4. check if the course has reached its maximum capacity (optional)
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrolledAt = DateTime.UtcNow
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return new EnrollmentDto
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrolledAt = enrollment.EnrolledAt,
                Grade = enrollment.Grade
            };
        }

        public async Task<bool> UnenrollAsync(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment == null) return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<StudentDto>> GetStudentsByCourseAsync(int courseId)
        {
            // optional: check if course exists
            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == courseId);

            if (!courseExists)
            {
                throw new ArgumentException($"Course with ID {courseId} does not exist.");
            }

            var students = await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.Student)
                .AsNoTracking()
                .ToListAsync();

            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email
            }).ToList();
        }

        public async Task<List<CourseDto>> GetCoursesByStudentAsync(int studentId)
        {
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == studentId);

            if (!studentExists)
            {
                throw new ArgumentException($"Student with ID {studentId} does not exist.");
            }

            var courses = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.Course)
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
    }
}
