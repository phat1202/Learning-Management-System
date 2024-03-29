﻿using Microsoft.EntityFrameworkCore;
using Learning_Management_System.Models;
using Microsoft.Extensions.Hosting;
using Learning_Management_System.Extensions;
using CloudinaryDotNet.Actions;

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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<CommentLesson> CommentLessons {  get; set; }
        public DbSet<Reply> RepliesComments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            optionsBuilder.UseMySQL("Server = localhost; port = 3306; Database = lms_dbcontext; username = root; Password = 123456; Persist security Info = True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryCourse>().HasData(
                new CategoryCourse { CategoryId = 1, CategoryName = "Web Developement", CategoryImageCover = null },
                new CategoryCourse { CategoryId = 2, CategoryName = "Marketing", CategoryImageCover = null },
                new CategoryCourse { CategoryId = 3, CategoryName = "SEO", CategoryImageCover = null }
                );
        }
    }
}
