using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.API.Data;
using Register.API.Models.Domain;
using Register.API.Models.DTO;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly RegisterDbContext dbContext;
        public CoursesController(RegisterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll() 
        {
            var courses = dbContext.Courses.ToList();
            return Ok(courses);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id) 
        { 
            var course = dbContext.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }


        [HttpPost]
        public IActionResult Create(AddCourseRequestDto addCourseRequestDto)
        {
            var course = new Course
            {
                Title = addCourseRequestDto.Title
            };
            dbContext.Courses.Add(course);
            dbContext.SaveChanges();
            var courseDto = new CourseDto
            {
                Id = course.Id,
                Title = course.Title
            };
            return CreatedAtAction(nameof(GetById),new { id = course.Id }, course);
        }


        [HttpDelete]
        public IActionResult DeleteById(Guid id) 
        {
            var course = dbContext.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            dbContext.Courses.Remove(course);
            dbContext.SaveChanges();
            return Ok(course);
        }


        [HttpPut]
        public IActionResult Update(Guid id, UpdateCourseRequestDto updateCourseDto)
        {
            var course = dbContext.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            course.Title = updateCourseDto.Title;
            dbContext.SaveChanges();
            return Ok(course);
        }
    }
}
