using Learning_Management_System.Models;

namespace Learning_Management_System.Extensions
{
    public class StatisticsService
    {
        private readonly LmsDbContext _context;
        public StatisticsService(LmsDbContext context)
        {
            _context = context;
        }
        //Index Page
        public int GetNumberOfEnrollment(Course item)
        {
            return _context.Enrollments.Count(student => student.CourseId == item.CourseId);
        }
        public string GetTeacherName(Course item)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == item.TeacherId).UserName;
        }
        public List<Course> GetCourse()
        {
            return _context.Courses.ToList();
        }
        public List<User> GetTecher()
        {
            return _context.Users.Where(u => u.IsTeacher == true).ToList();
        }
    }
}
