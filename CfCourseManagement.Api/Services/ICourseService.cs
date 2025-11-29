using CfCourseManagement.Api.Models;

namespace CfCourseManagement.Api.Services
{
    public interface ICourseService
    {
        List<Course> GetAll();
        Course? GetById(int id);
        Course Create(Course course);
        bool Update(int id, Course course);
        bool Delete(int id);
    }
}
