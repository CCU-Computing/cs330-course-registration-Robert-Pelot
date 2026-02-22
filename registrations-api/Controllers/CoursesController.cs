using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CourseRegistration.Models;
using CourseRegistration.Services;
using System.Runtime.Versioning;


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

    // -----------------------
    // User Story 1
    // Offerings that meet a core goal in a given semester
    // GET /Courses/offerings/goal/{goalId}?semester=Fall 2021
    [HttpGet("offerings/goal/{goalId}")]
    public ActionResult<IEnumerable<CourseOffering>> GetOfferingsByGoalAndSemester(string goalId, [FromQuery] string semester)
    {
        try
        {
            var result = _courseServices.getOfferingsByGoalIdAndSemester(goalId, semester);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // -----------------------
    // User Story 3
    // Get all offerings for a given semester
    // GET /Courses/offerings?semester=Fall 2021
    [HttpGet("offerings")]
    public ActionResult<IEnumerable<CourseOffering>> GetOfferingsBySemester([FromQuery] string semester)
    {
        var result = _courseServices.getCourseOfferingsBySemester(semester);
        return Ok(result);
    }

    // -----------------------
    // User Story 4
    // Get offerings by semester and department
    // GET /Courses/offerings/search?semester=Fall 2021&dept=CSCI
    [HttpGet("offerings/search")]
    public ActionResult<IEnumerable<CourseOffering>> GetOfferingsBySemesterAndDept([FromQuery] string semester, [FromQuery] string dept)
    {
        var result = _courseServices.getCourseOfferingsBySemesterAndDept(semester, dept);
        return Ok(result);
    }

    // -----------------------
    // User Story 5
    // Get courses that satisfy a single core goal
    // GET /Courses/goal/{goalId}
    [HttpGet("goal/{goalId}")]
    public ActionResult<IEnumerable<Course>> GetCoursesByGoal(string goalId)
    {
        var result = _courseServices.getCoursesByGoalId(goalId);
        return Ok(result);
    }

    // -----------------------
    // User Story 6
    // Get courses that satisfy multiple core goals
    // GET /Courses/goals?ids=CG1,CG2
    [HttpGet("goals")]
    public ActionResult<IEnumerable<Course>> GetCoursesByMultipleGoals([FromQuery] string ids)
    {
        var goalIds = ids.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var result = _courseServices.getCoursesByGoalIds(goalIds);
        return Ok(result);
    }

    // -----------------------
    // User Story 7
    // Get all core goals that have no offerings in a given semester
    // GET /Courses/goals/missing?semester=Fall 2021
    [HttpGet("goals/missing")]
    public ActionResult<IEnumerable<CoreGoal>> GetCoreGoalsNotCovered([FromQuery] string semester)
    {
        var result = _courseServices.getCoreGoalsThatAreNotCoveredBySemester(semester);
        return Ok(result);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAllCourses()
    {
        try
        {
            IEnumerable<Course> list = _courseServices.getCourses();
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

    [HttpGet("{name}")]
    public ActionResult<Course> GetCourseByName(string name)
    {
        var course = _courseServices.getCourseByName(name);
        if (course == null)
            return NotFound();
        return Ok(course);
    }

    [HttpGet("search")]
    public ActionResult<IEnumerable<Course>> SearchByDepartment([FromQuery] string dept)
    {
        var courses = _courseServices.searchCoursesByDepartment(dept);
        return Ok(courses);
    }

    [HttpPost]
    public ActionResult<Course> AddCourse([FromBody] Course course)
    {
        var created = _courseServices.addCourse(course);
        return CreatedAtAction(nameof(GetCourseByName), new { name = created.Name }, created);
    }

    [HttpPut("{name}")]
    public ActionResult UpdateCourse(string name, [FromBody] Course course)
    {
        bool updated = _courseServices.updateCourse(name, course);
        if (!updated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{name}")]
    public ActionResult DeleteCourse(string name)
    {
        bool deleted = _courseServices.deleteCourse(name);
        if (!deleted)
            return NotFound();
        return NoContent();
    }

    [HttpGet("{name}/goals")]
    public ActionResult<IEnumerable<CoreGoal>> GetGoalsForCourse(string name)
    {
        var goals = _courseServices.getGoalsByCourseName(name);
        return Ok(goals);
    }

    [HttpGet("{name}/offerings")]
    public ActionResult<IEnumerable<CourseOffering>> GetOfferingsForCourse(
        string name,
        [FromQuery] string semester)
    {
        var offerings = _courseServices
            .getOfferingsByCourseNameAndSemester(name, semester);
        return Ok(offerings);
    }
    }
}
