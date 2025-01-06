namespace Register.API.Models.Domain
{
    public class TeacherCourse
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
