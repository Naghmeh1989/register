using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.API.Data;
using Register.API.Models.Domain;
using Register.API.Models.DTO;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly RegisterDbContext dbContext;

        public TeachersController(RegisterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        { 
            var teachers = dbContext.Teachers.ToList();
            return Ok(teachers);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var teacher = dbContext.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null) 
            {
                return NotFound();
            }
            return Ok(teacher);
        }
        [HttpPost]
        public IActionResult Create(AddTeacherRequestDto addTeacherRequestDto)
        {
            var teacher = new Teacher
            {
                FirstName = addTeacherRequestDto.FirstName,
                LastName = addTeacherRequestDto.LastName
            };
            dbContext.Teachers.Add(teacher);
            dbContext.SaveChanges();
            var techerDto = new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
            };
            return CreatedAtAction(nameof(GetById), new { id = teacher.Id },techerDto);
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterTeacherDto registerTeacherDto)
        {
            var course = dbContext.Courses.Find(registerTeacherDto.CourseId);
            if (course == null)
            { return NotFound(); }

            var teacher = dbContext.Teachers.Find(registerTeacherDto.TeacherId);
            if (teacher == null)
            { return NotFound(); }

            var teacherCourse = new TeacherCourse
            {
                CourseId = course.Id,
                TeacherId = teacher.Id

            };
            dbContext.TeacherCourses.Add(teacherCourse);
            dbContext.SaveChanges();
            return Ok(teacherCourse);
        }
    }
}
