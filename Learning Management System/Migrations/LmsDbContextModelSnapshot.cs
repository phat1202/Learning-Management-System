﻿// <auto-generated />
using System;
using Learning_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Learning_Management_System.Migrations
{
    [DbContext(typeof(LmsDbContext))]
    partial class LmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Learning_Management_System.Models.CategoryCourse", b =>
                {
                    b.Property<int?>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CategoryId");

                    b.ToTable("CategoryCourses");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Web Developement"
                        });
                });

            modelBuilder.Entity("Learning_Management_System.Models.Chapter", b =>
                {
                    b.Property<int?>("ChapterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ChapterTitle")
                        .HasColumnType("longtext");

                    b.Property<int?>("CourseId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("TotalNumberOfLesson")
                        .HasColumnType("int");

                    b.HasKey("ChapterId");

                    b.HasIndex("CourseId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("Learning_Management_System.Models.Course", b =>
                {
                    b.Property<int?>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TeacherId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("CourseId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            CategoryId = 1,
                            CourseDescription = "C#, SQL, EntityFramework",
                            CourseTitle = "ASP.NET Core",
                            TeacherId = "2"
                        });
                });

            modelBuilder.Entity("Learning_Management_System.Models.Enrollment", b =>
                {
                    b.Property<int?>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Learning_Management_System.Models.Lesson", b =>
                {
                    b.Property<int?>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ChapterId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LessonName")
                        .HasColumnType("longtext");

                    b.HasKey("LessonId");

                    b.HasIndex("ChapterId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Learning_Management_System.Models.StudentProgress", b =>
                {
                    b.Property<int?>("ProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CompletionStatus")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastAccessed")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LessonId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("ProgressId");

                    b.HasIndex("LessonId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentProgresses");
                });

            modelBuilder.Entity("Learning_Management_System.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsStudent")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsTeacher")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            CreatedAt = new DateTime(2023, 12, 9, 16, 28, 54, 307, DateTimeKind.Local).AddTicks(7401),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "firstStudent@gmail.com",
                            IsActive = false,
                            IsDeleted = false,
                            IsStudent = true,
                            IsTeacher = false,
                            Password = "WnDvAVc6XNj2E65rrp/cjxaatG9G9uJAv6+qm4hOTPg=",
                            UpdatedAt = new DateTime(2023, 12, 9, 16, 28, 54, 307, DateTimeKind.Local).AddTicks(7387),
                            UserName = "firstStudent"
                        },
                        new
                        {
                            UserId = "2",
                            CreatedAt = new DateTime(2023, 12, 9, 16, 28, 54, 307, DateTimeKind.Local).AddTicks(7728),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "firstTeacher@gmail.com",
                            IsActive = false,
                            IsDeleted = false,
                            IsStudent = false,
                            IsTeacher = false,
                            Password = "1JeuIu/LWAqR9mTJV+7zV7sK9KYnsZJh+F5WbwhVmrA=",
                            UpdatedAt = new DateTime(2023, 12, 9, 16, 28, 54, 307, DateTimeKind.Local).AddTicks(7727),
                            UserName = "firstTeacher"
                        });
                });

            modelBuilder.Entity("Learning_Management_System.Models.Chapter", b =>
                {
                    b.HasOne("Learning_Management_System.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");
                });

            modelBuilder.Entity("Learning_Management_System.Models.Course", b =>
                {
                    b.HasOne("Learning_Management_System.Models.CategoryCourse", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning_Management_System.Models.User", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Learning_Management_System.Models.Enrollment", b =>
                {
                    b.HasOne("Learning_Management_System.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning_Management_System.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Learning_Management_System.Models.Lesson", b =>
                {
                    b.HasOne("Learning_Management_System.Models.Chapter", "chapter")
                        .WithMany()
                        .HasForeignKey("ChapterId");

                    b.Navigation("chapter");
                });

            modelBuilder.Entity("Learning_Management_System.Models.StudentProgress", b =>
                {
                    b.HasOne("Learning_Management_System.Models.Lesson", "lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning_Management_System.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lesson");

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
