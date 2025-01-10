using System.Text.Json.Serialization;

namespace Register.API.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public List<Teacher> Teachers { get; set; }

        [JsonIgnore]
        public List<Student> Students { get; set; } 

        [JsonIgnore]
        public List<TeacherCourse> TeacherCourses { get; set; }  

        [JsonIgnore]
        public List<StudentCourse> StudentCourses { get; set; } 
    }
}
