namespace Register.API.Models.Domain
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> Courses { get; } = [];
        public List<TeacherCourse> TeacherCourses { get; } = [];
       

    }
}
