using CfCourseManagement.Api.Data;
using CfCourseManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CfCourseManagement.Api.Services
{
    public class CourseService: ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        { 
         _context = context;
        }
        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course? GetById(int id)
        {
            return _context.Courses.Find(id);
        }
        public Course Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }
        public bool Update(int id, Course course)
        {
            var existingCourse = _context.Courses.Find(id);
            if (existingCourse == null)
            {
                return false;
            }
            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Credits = course.Credits;
            existingCourse.StartDate = course.StartDate;
            existingCourse.EndDate = course.EndDate;

            _context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return false;
            }
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return true;
        }
    }
}
