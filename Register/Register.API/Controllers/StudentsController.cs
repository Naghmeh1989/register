using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Register.API.Data;
using Register.API.Models.Domain;
using Register.API.Models.DTO;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly RegisterDbContext dbContext;
        public StudentsController(RegisterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var students = dbContext.Students.ToList();
            return Ok(students);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var student = dbContext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpPost]
        public IActionResult Create(AddStudentRequestDto addStudentRequestDto)
        {
            var student = new Student
            {
                FirstName = addStudentRequestDto.FirstName,
                LastName = addStudentRequestDto.LastName,
            };
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
            var studentDto = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
            };
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, studentDto);
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterStudentDto registerStudentDto)
        {
            var course = dbContext.Courses.Find(registerStudentDto.CourseId);
            if (course == null)
            { 
                return NotFound();
            }
            else
            {
                var student = dbContext.Students.Find(registerStudentDto.StudentId);
                if (student == null)
                {
                    return NotFound();
                }
            }
            var studentCourse = new StudentCourse
            {
                CourseId = registerStudentDto.CourseId,
                StudentId = registerStudentDto.StudentId
            };
            dbContext.StudentCourses.Add(studentCourse);
            dbContext.SaveChanges();
            return Ok(studentCourse);
        }


        [HttpGet]
        [Route("get-course")]
        public IActionResult GetCourse(Guid student_id) 
        {
            var courses = dbContext.Courses
            .Include(x => x.Teachers).Include(x => x.Students.Where(x => x.Id == student_id))
            .Select(c => new CourseDto()
            {
                Id = c.Id,
                Title = c.Title,
                Teachers = c.Teachers
            });
             return Ok(courses);
        }


        [HttpDelete]
        public IActionResult Delete(Guid id) 
        {
            var student = dbContext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            };
            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
            return Ok(student);
        }


        [HttpPut]
        public IActionResult Update(Guid id, UpdateStudentRequestDto updateStudentDto)
        {
            var student = dbContext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            dbContext.SaveChanges();
            return Ok(student);
        }
    }
}
