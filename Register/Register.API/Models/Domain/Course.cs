namespace Register.API.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public List<Teacher> Teachers { get; } = [];

        public List<Student> Students { get; } = [];
        public List<TeacherCourse> TeacherCourses { get; } = [];
        public List<StudentCourse> StudentCourses { get; } = [];


    }
}
