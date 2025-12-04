using CfCourseManagement.Api.Dtos.Courses;
using CfCourseManagement.Api.Dtos.Enrollments;
using CfCourseManagement.Api.Dtos.Students;

namespace CfCourseManagement.Api.Services
{
    public interface IEnrollmentService
    {
        Task<EnrollmentDto> EnrollAsync(EnrollmentCreateDto dto);
        Task<bool> UnenrollAsync(int studentId, int courseId);

        Task<List<StudentDto>> GetStudentsByCourseAsync(int courseId);
        Task<List<CourseDto>> GetCoursesByStudentAsync(int studentId);
    }
}
