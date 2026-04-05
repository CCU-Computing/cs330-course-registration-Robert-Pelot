using System;
using System.Collections.Generic;
using System.Linq;
using CourseRegistration.Models;
using CourseRegistration.Repository;


namespace CourseRegistration.Services
{
   public class CourseServices : ICourseServices
   {
      private readonly ICourseRepository _repo;

      public CourseServices(ICourseRepository courseRepo)
      {
         _repo = courseRepo;
      }
      // Get list of all courses:
      public List<Course> GetAllCourses()
      {
         List<Course> theCourses = _repo.GetAllCourses().ToList<Course>();
         return theCourses;
      }

      // Get specific course by name:

      public Course GetCourseByName(string name)
      {
         Course c = _repo.GetCourseByName(name);
         return c;
      }
      
      // Add a new course to the database:
      public Course AddCourse(Course newCourse)
      {
         return _repo.InsertCourse(newCourse);
      }

      // Update / Change a course in the database:
      public bool updateCourse(string name, Course updatedCourse)
      {
         Course existingCourse = _repo.GetAllCourses().FirstOrDefault(c => c.Name == name);
         if (existingCourse == null)
         {
            return false; 
         }
         existingCourse.Title = updatedCourse.Title;
         existingCourse.Credits = updatedCourse.Credits;
         existingCourse.Description = updatedCourse.Description;
         return _repo.UpdateCourse(existingCourse);
      }

      // Delete a course from the repo.
      public bool DeleteCourse(string name)
      {
         Course existingCourse = _repo.GetAllCourses().FirstOrDefault(c => c.Name == name);
         if (existingCourse == null)
         {
            return false;
         }
         return _repo.DeleteCourse(name);
      }

      // Get All Course goals:
      public List<CoreGoal> GetAllCoreGoals()
      {
         return _repo.GetAllCoreGoals().ToList();
      }

      // Get core goal by id:
      public CoreGoal GetCoreGoalById(string id)
      {
         CoreGoal goal = _repo.GetAllCoreGoals().FirstOrDefault(g => g.Id == id);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{id}' not found.");
         return goal;
      }

      // get all classes that match a core goal id:
      public CoreGoal GetCoreGoalWithCoursesById(string id)
      {
         CoreGoal goal = _repo.GetCoreGoalById(id);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{id}' not found.");
         goal.Courses = _repo.GetCoursesByGoalId(id).ToList();
         return goal;
      }

      // get all classes that match a core goal - just the class list:
      public IEnumerable<Course> GetCoursesForCoreGoalById(string id)
      {
         // Optional: verify that the core goal exists
         CoreGoal goal = _repo.GetCoreGoalById(id);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{id}' not found.");

         // Fetch all courses for this goal
         return _repo.GetCoursesByGoalId(id);
      }

      // Insert a new core goal
      public CoreGoal InsertCoreGoal(CoreGoal newGoal)
      {
         // optional: check if ID already exists
         var existingGoal = _repo.GetCoreGoalById(newGoal.Id);
         if (existingGoal != null)
            throw new Exception($"CoreGoal with ID '{newGoal.Id}' already exists.");

         return _repo.AddCoreGoal(newGoal);
      }

      // Assign existing courses to a core goal
      public void AssignCoursesToCoreGoal(string goalId, List<string> courseNames)
      {
         var goal = _repo.GetCoreGoalById(goalId);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{goalId}' not found.");

         _repo.AddCoursesToCoreGoal(goalId, courseNames);
      }

      public bool UpdateCoreGoal(string id, CoreGoal modifiedGoal)
      {
         var existingGoal = _repo.GetCoreGoalById(id);
         if (existingGoal == null)
            return false;

         return _repo.UpdateCoreGoal(id, modifiedGoal);
      }

      public bool DeleteCoreGoal(string id)
      {
         var existingGoal = _repo.GetCoreGoalById(id);
         if (existingGoal == null)
            return false;

         return _repo.DeleteCoreGoal(id);
      }

      public IEnumerable<CourseOffering> GetOfferingsByGoalIdAndSemester(string goalId, string semester)
      {
         // Check if the core goal exists
         var goal = _repo.GetCoreGoalById(goalId);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{goalId}' not found.");

         // If it exists, get the offerings from the repository
         return _repo.GetOfferingsByGoalIdAndSemester(goalId, semester);
      }

      public IEnumerable<CourseOffering> GetOfferingsBySemester(string semester)
      {
         return _repo.GetOfferingsBySemester(semester);
      }

      public IEnumerable<CourseOffering> GetOfferingsBySemesterAndDepartment(string semester, string department)
      {
         return _repo.GetOfferingsBySemesterAndDepartment(semester, department);
      }

   
   }
}
