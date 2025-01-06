using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Register.API.Models.Domain;

namespace Register.API.Data
{
    public class RegisterDbContext: DbContext
    {
        public RegisterDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
                
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Courses)
                .WithMany(e => e.Students)
                .UsingEntity<StudentCourse>();
        
            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Courses)
                .WithMany(e => e.Teachers)
                .UsingEntity<TeacherCourse>();
        
            modelBuilder.Entity<Course>()
                .HasMany(e => e.Teachers)
                .WithMany(e => e.Courses)
                .UsingEntity<TeacherCourse>();

            modelBuilder.Entity<Course>()
              .HasMany(e => e.Students)
              .WithMany(e => e.Courses)
              .UsingEntity<StudentCourse>();
        }
    
    }
}
