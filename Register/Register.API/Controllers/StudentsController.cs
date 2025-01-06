using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            };
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
            { return NotFound(); }
            else
            {
                var student = dbContext.Students.Find(registerStudentDto.StudentId);

                if (student == null)
                { return NotFound(); }
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
        [Route("get-course/{id:guid}")]
        public async Task<IActionResult> GetCourse(Guid id) 
        {
            var studentCourses =  dbContext.StudentCourses.Where(x=>x.StudentId == id);
            Teacher[] teachers = { };
            if (studentCourses != null)
            {
                foreach (var studentCourse in studentCourses)
                {
                    studentCourse.CourseId = id;

                    var techerCourses = dbContext.TeacherCourses.Where(x => x.CourseId == id);


                    if (techerCourses != null)
                    {
                        foreach (var teacherCourse in techerCourses)
                        {
                            teacherCourse.TeacherId = id;
                            var teacher = dbContext.Teachers.Find(id);
                            if (teacher != null)
                            {
                                teachers.ToList().Add(teacher);
                            }
                        }
                    }

                }
            }
             
          return Ok(teachers);  
        }
    }
}
