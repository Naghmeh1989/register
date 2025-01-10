using Register.API.Models.Domain;

namespace Register.API.Models.DTO
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
