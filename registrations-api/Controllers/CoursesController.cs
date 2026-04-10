using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CourseRegistration.Models;
using CourseRegistration.Services;
<<<<<<< HEAD
using System.Runtime.Versioning;
using System.IO;

=======
>>>>>>> unittesting

namespace CourseRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseServices _courseServices;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseServices courseServices, ILogger<CoursesController> logger)
        {
            _courseServices = courseServices;
            _logger = logger;
        }

<<<<<<< HEAD
     // GET ALL COURSES
    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAllCourses()
    {
        try
        {
            IEnumerable<Course> list = _courseServices.GetAllCourses();
            if (list != null)
=======
        // GET ALL COURSES
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetAllCourses()
        {
            try
>>>>>>> unittesting
            {
                var list = _courseServices.GetAllCourses();
                if (list != null)
                    return Ok(list);

                return StatusCode(500, "Unable to retrieve courses at this time. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all courses");
                return StatusCode(500, "An unexpected error occurred while retrieving courses. Please try again later.");
            }
        }

        // GET COURSE BY NAME
        [HttpGet("{name}", Name = "GetCourseByName")]
        public IActionResult GetCourseByName(string name)
        {
            try
            {
                var course = _courseServices.GetCourseByName(name);
                if (course != null)
                    return Ok(course);

                return NotFound($"No course found with the name '{name}'. Please check the name and try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving course '{name}'");
                return StatusCode(500, "An unexpected error occurred while retrieving the course. Please try again later.");
            }
        }

        // ADD COURSE
        [HttpPost]
        public IActionResult AddCourse(Course c)
        {
            try
            {
                if (c == null)
                    return BadRequest("Course data must be provided.");

                var createdCourse = _courseServices.AddCourse(c);
                if (createdCourse != null)
                    return CreatedAtRoute("GetCourseByName", new { name = createdCourse.Name }, createdCourse);

                return BadRequest("Course could not be created. Please ensure all required fields are provided and valid.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding course");
                return StatusCode(500, "An unexpected error occurred while adding the course. Please try again.");
            }
        }

        // UPDATE COURSE
        [HttpPut("{name}")]
        public IActionResult UpdateCourseByName(string name, [FromBody] Course updatedCourse)
        {
            try
            {
                var success = _courseServices.updateCourse(name, updatedCourse);
                if (!success)
                    return NotFound($"Course '{name}' does not exist. Verify the course name and try again.");

                return Ok(updatedCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating course '{name}'");
                return StatusCode(500, "An unexpected error occurred while updating the course. Please try again.");
            }
        }

        // DELETE COURSE
        [HttpDelete("{name}")]
        public IActionResult DeleteCourseByName(string name)
        {
            try
            {
                var success = _courseServices.DeleteCourse(name);
                if (!success)
                    return NotFound($"Cannot delete course '{name}' because it does not exist.");

                return Ok($"Course '{name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting course '{name}'");
                return StatusCode(500, "An unexpected error occurred while deleting the course. Please try again.");
            }
        }

        // GET ALL CORE GOALS
        [HttpGet("/coregoals", Name = "GetAllCoreGoals")]
        public IActionResult GetAllCoreGoals()
        {
            try
            {
                var goals = _courseServices.GetAllCoreGoals();
                return Ok(goals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving core goals");
                return StatusCode(500, "An unexpected error occurred while retrieving core goals. Please try again later.");
            }
        }

        // GET CORE GOAL BY ID
        [HttpGet("/coregoals/{id}", Name = "GetCoreGoalById")]
        public IActionResult GetCoreGoalById(string id)
        {
            try
            {
                var goal = _courseServices.GetCoreGoalById(id);
                if (goal == null)
                    return NotFound($"Core goal with ID '{id}' was not found. Verify the ID and try again.");

                return Ok(goal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving core goal '{id}'");
                return StatusCode(500, "An unexpected error occurred while retrieving the core goal. Please try again later.");
            }
        }

        // GET CORE GOAL WITH COURSES
        [HttpGet("/coregoals/{id}/withcourses", Name = "GetCoreGoalWithCoursesById")]
        public IActionResult GetCoreGoalWithCoursesById(string id)
        {
            try
            {
                var goal = _courseServices.GetCoreGoalWithCoursesById(id);
                if (goal == null)
                    return NotFound($"Core goal with ID '{id}' was not found. Verify the ID and try again.");

                return Ok(goal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving core goal with courses '{id}'");
                return StatusCode(500, "An unexpected error occurred while retrieving the core goal and its courses. Please try again later.");
            }
        }

        // GET COURSES FOR CORE GOAL
        [HttpGet("/coregoals/{id}/courses", Name = "GetCoursesForCoreGoalById")]
        public IActionResult GetCoursesForCoreGoalById(string id)
        {
            try
            {
                var courses = _courseServices.GetCoursesForCoreGoalById(id);
                if (courses == null)
                    return NotFound($"No courses found for core goal ID '{id}'.");

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving courses for core goal '{id}'");
                return StatusCode(500, "An unexpected error occurred while retrieving courses for the core goal. Please try again later.");
            }
        }

        // ADD CORE GOAL
        [HttpPost("/coregoals")]
        public IActionResult InsertCoreGoal([FromBody] CoreGoal newGoal)
        {
            try
            {
                if (newGoal == null)
                    return BadRequest("Core goal data must be provided.");

                var insertedGoal = _courseServices.InsertCoreGoal(newGoal);
                return Ok(insertedGoal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting core goal");
                return BadRequest("Unable to insert the core goal. Please check the input data and try again.");
            }
        }

        // ASSIGN COURSES TO CORE GOAL
        [HttpPost("/coregoals/{goalId}/assign-courses")]
        public IActionResult AssignCoursesToCoreGoal(string goalId, [FromBody] List<string> courseNames)
        {
            try
            {
                _courseServices.AssignCoursesToCoreGoal(goalId, courseNames);
                return Ok($"Courses assigned to CoreGoal '{goalId}' successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error assigning courses to core goal '{goalId}'");
                return BadRequest("Unable to assign courses. Please check the input data and try again.");
            }
        }

        // UPDATE CORE GOAL
        [HttpPut("/coregoals/{id}")]
        public IActionResult UpdateCoreGoal(string id, [FromBody] CoreGoal modifiedGoal)
        {
            try
            {
                var success = _courseServices.UpdateCoreGoal(id, modifiedGoal);
                if (!success)
                    return NotFound($"CoreGoal '{id}' not found. Verify the ID and try again.");

                return Ok(modifiedGoal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating core goal '{id}'");
                return StatusCode(500, "An unexpected error occurred while updating the core goal. Please try again later.");
            }
        }

        // DELETE CORE GOAL
        [HttpDelete("/coregoals/{id}")]
        public IActionResult DeleteCoreGoal(string id)
        {
            try
            {
                var success = _courseServices.DeleteCoreGoal(id);
                if (!success)
                    return NotFound($"CoreGoal '{id}' not found. Verify the ID and try again.");

                return Ok($"CoreGoal '{id}' and its course associations deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting core goal '{id}'");
                return StatusCode(500, "An unexpected error occurred while deleting the core goal. Please try again later.");
            }
        }

        
        [HttpGet("offerings")]
        public IActionResult GetOfferingsByGoalAndSemester([FromQuery] string goalId, [FromQuery] string semester)
        {
            try
            {
                var offerings = _courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);
                return Ok(offerings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("offerings/by-semester")]
        public IActionResult GetOfferingsBySemester([FromQuery] string semester)
        {
            if (string.IsNullOrEmpty(semester))
                return BadRequest("Semester query parameter is required.");

            var offerings = _courseServices.GetOfferingsBySemester(semester);
            return Ok(offerings);
        }

        [HttpGet("offerings/department")]
        public IActionResult GetOfferingsBySemesterAndDepartment([FromQuery] string semester, [FromQuery] string department)
        {
            var offerings = _courseServices.GetOfferingsBySemesterAndDepartment(semester, department);
            return Ok(offerings);
        }
    }
<<<<<<< HEAD

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

=======
}
>>>>>>> unittesting
