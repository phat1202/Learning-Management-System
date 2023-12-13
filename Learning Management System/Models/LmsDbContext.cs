using Microsoft.EntityFrameworkCore;
using Learning_Management_System.Models;
using Microsoft.Extensions.Hosting;
using Learning_Management_System.Extensions;

namespace Learning_Management_System.Models
{
    public class LmsDbContext : DbContext
    {
        public LmsDbContext()
        {

        }
        public LmsDbContext(DbContextOptions<LmsDbContext> options) : base(options) { }
        public DbSet<CategoryCourse> CategoryCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            optionsBuilder.UseMySQL("Server=localhost;port=3306;Database=lmsdb;username=root;Password=123456;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>().HasData(
                 new User { UserId = "1", UserName = "firstStudent", Email = "firstStudent@gmail.com", Password = "firstStudent".Hash(), IsStudent = true },
                 new User { UserId = "2", UserName = "firstTeacher", Email = "firstTeacher@gmail.com", Password = "firstTeacher".Hash(), IsTeacher = false });
            modelBuilder.Entity<CategoryCourse>().HasData(
                new CategoryCourse { CategoryId = 1, CategoryName ="Web Developement"}
                );
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseTitle = "ASP.NET Core", CourseDescription = "C#, SQL, EntityFramework",TeacherId = "2", CategoryId = 1 }
    );
        }
    }
}
