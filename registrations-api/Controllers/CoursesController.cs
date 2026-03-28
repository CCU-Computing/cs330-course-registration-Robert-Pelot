using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CourseRegistration.Models;
using CourseRegistration.Services;
using System.Runtime.Versioning;
using System.IO;


namespace CourseRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // another option for the route is to specify the word you want:
    // [Route("Courses")]
    public class CoursesController : ControllerBase
    {
        private ICourseServices _courseServices;

        public CoursesController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

     // GET ALL COURSES
    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAllCourses()
    {
        try
        {
            IEnumerable<Course> list = _courseServices.GetAllCourses();
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return BadRequest("Failed to retrieve courses.");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{name}", Name = "GetCourseByName")]
    public IActionResult GetCourseByName(string name)
    {
        try
        {
            Course c = _courseServices.GetCourseByName(name);

            if (c != null)
                return Ok(c);
            else
                return NotFound($"Course '{name}' not found.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost]
    public IActionResult AddCourse(Course c)
        {
            try
            {
                Course createdCourse = _courseServices.AddCourse(c);
                if (createdCourse != null) return CreatedAtRoute("GetCourseByName", new { name = createdCourse.Name }, createdCourse);
                else return BadRequest(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    [HttpPut("{name}")]
    public IActionResult UpdateCourse(string name, [FromBody] Course updatedCourse)
    {
        bool success = _courseServices.updateCourse(name, updatedCourse);
        if (!success)
        {
            return NotFound($"Course '{name}' not found");
        } else return Ok(updatedCourse);
    }

    [HttpDelete("{name}")]
    public IActionResult DeleteCourse(string name)
    {
        bool success = _courseServices.DeleteCourse(name);

        if (!success)
        {
            return NotFound($"Course '{name}' not found.");
        }

        return Ok($"Course '{name}' deleted successfully.");
    }


}
}

