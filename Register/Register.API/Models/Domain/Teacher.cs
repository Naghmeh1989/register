using System.Text.Json.Serialization;

namespace Register.API.Models.Domain
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> Courses { get; set; } 

        [JsonIgnore]
        public List<TeacherCourse> TeacherCourses { get; set; } 
    }
}
