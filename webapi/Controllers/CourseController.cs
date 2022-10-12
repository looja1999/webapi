using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]

    public class CourseController : Controller
    {   
        public readonly DataContext _dataContext; 

        public CourseController(DataContext dataContext) 
        {   
            _dataContext = dataContext; 
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCourses(){

            var courses = await _dataContext.Courses.ToListAsync();

            return Ok(courses);  
        }
        
        [HttpPost]

        public async Task<IActionResult> AddCourse([FromBody] Course courseRequest){

            courseRequest.Id = Guid.NewGuid(); 

            await _dataContext.Courses.AddAsync(courseRequest); 
            await _dataContext.SaveChangesAsync(); 
            
            return Ok(courseRequest); 
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetCourse([FromRoute] Guid id){
            var course = await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == id); 

            if (course == null){
                return NotFound(); 
            } 

            return Ok(course);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateCourse([FromRoute] Guid id, Course updateCourseReq){

            var course = await _dataContext.Courses.FindAsync(id); 

            if (course == null){
                return NotFound(); 
            }

            course.CourseName = updateCourseReq.CourseName; 
            course.CourseTeacher = updateCourseReq.CourseTeacher; 

            await _dataContext.SaveChangesAsync(); 

            return Ok(course); 
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid id){

            var course = await _dataContext.Courses.FindAsync(id); 

            if (course == null){
                return NotFound(); 
            }

            _dataContext.Courses.Remove(course);
            await _dataContext.SaveChangesAsync(); 

            return Ok(course); 
        }

    }
}