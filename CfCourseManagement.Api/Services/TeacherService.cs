using CfCourseManagement.Api.Data;
using CfCourseManagement.Api.Models;

namespace CfCourseManagement.Api.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;

        public TeacherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }

        public Teacher? GetById(int id)
        {
            return _context.Teachers.Find(id);
        }

        public Teacher Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return teacher;
        }

        public bool Update(int id, Teacher teacher)
        {
            var existing = _context.Teachers.Find(id);
            if (existing == null) return false;

            existing.FullName = teacher.FullName;
            existing.Email = teacher.Email;
            existing.Phone = teacher.Phone;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var existing = _context.Teachers.Find(id);
            if (existing == null) return false;

            _context.Teachers.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}
