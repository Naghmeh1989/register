using Register.API.Models.Domain;

namespace Register.API.Models.DTO
{
    public class StudentCourseDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
