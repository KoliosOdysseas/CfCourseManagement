using CfCourseManagement.Api.Dtos.Teachers;
using CourseManagementSystem.DTOs.Teachers;

namespace CfCourseManagement.Api.Services
{
    public interface ITeacherService
    {
        Task<List<TeacherDto>> GetAllAsync();
        Task<TeacherDto?> GetByIdAsync(int id);
        Task<TeacherDto> CreateAsync(TeacherCreateDto dto);
        Task<bool> UpdateAsync(int id, TeacherUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<TeacherInfoDto?> GetTeacherInfoAsync(int teacherId);
    }
}
