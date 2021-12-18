using CoursesProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<CourseStudentViewModel> CourseStudents { get; set; }
        //public DbSet<TrainerStudentViewModel> TrainerStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<CourseStudentViewModel>().HasKey(
                cs => new { cs.CourseId, cs.StudentId }
                );

            //modelBuilder.
            //    Entity<TrainerStudentViewModel>().HasKey(
            //    ts => new { ts.TrainerId, ts.StudentId }
            //    );

        }
    }
}
