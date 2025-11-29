using CfCourseManagement.Api.Models;

namespace CfCourseManagement.Api.Services
{
    public interface ITeacherService
    {
        List<Teacher> GetAll();
        Teacher? GetById(int id);
        Teacher Create(Teacher teacher);
        bool Update(int id, Teacher teacher);
        bool Delete(int id);
    }
}
