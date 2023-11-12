using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Learning_Management_System.Models;

namespace Learning_Management_System.Models
{
    public class LmsDbContext : IdentityDbContext<ApplicationUser>
    {
        public LmsDbContext()
        {

        }
        public LmsDbContext(DbContextOptions<LmsDbContext> options) : base(options) { }

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CategoryCourse> CategoryCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Chapter> Chapters {  get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        //public DbSet<Test> Tests { get; set; }
        //public DbSet<TestScore> TestScores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            optionsBuilder.UseMySQL("Server=localhost;port=3306;Database=lmsdb;username=root;Password=123456;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
        public DbSet<Learning_Management_System.Models.Student> Student { get; set; } = default!;
    }
}
