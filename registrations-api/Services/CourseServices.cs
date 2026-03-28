using System;
using System.Collections.Generic;
using System.Linq;
using CourseRegistration.Models;
using CourseRegistration.Repository;
using linq = System.Linq;


namespace CourseRegistration.Services
{
   public class CourseServices : ICourseServices
   {
      private CourseRepository repo = new CourseRepository();
      
      // Get list of all courses:
      public List<Course> GetAllCourses()
      {
         List<Course> theCourses = repo.GetAllCourses().ToList<Course>();
         return theCourses;
      }

      // Get specific course by name:

      public Course GetCourseByName(string name)
      {
         Course c = repo.GetCourseByName(name);
         return c;
      }
      
      // Add a new course to the database:
      public Course AddCourse(Course newCourse)
      {
         return repo.InsertCourse(newCourse);
      }

      // Update / Change a course in the database:
      public bool updateCourse(string name, Course updatedCourse)
      {
         Course existingCourse = repo.GetAllCourses().FirstOrDefault(c => c.Name == name);
         if (existingCourse == null)
         {
            return false; 
         }
         existingCourse.Title = updatedCourse.Title;
         existingCourse.Credits = updatedCourse.Credits;
         existingCourse.Description = updatedCourse.Description;
         return repo.UpdateCourse(existingCourse);
      }

      // Delete a course from the repo.
      public bool DeleteCourse(string name)
      {
         Course existingCourse = repo.GetAllCourses().FirstOrDefault(c => c.Name == name);
         if (existingCourse == null)
         {
            return false;
         }
         return repo.DeleteCourse(name);
      }

      // Get All Course goals:
      public List<CoreGoal> GetAllCoreGoals()
      {
         return repo.GetAllCoreGoals().ToList();
      }

      // Get core goal by id:
      public CoreGoal GetCoreGoalById(string id)
      {
         CoreGoal goal = repo.GetAllCoreGoals().FirstOrDefault(g => g.Id == id);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{id}' not found.");
         return goal;
      }

      // get all classes that match a core goal id:
      public CoreGoal GetCoreGoalWithCoursesById(string id)
      {
         CoreGoal goal = repo.GetCoreGoalById(id);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{id}' not found.");
         goal.Courses = repo.GetCoursesByGoalId(id).ToList();
         return goal;
      }

      // get all classes that match a core goal - just the class list:
      public IEnumerable<Course> GetCoursesForCoreGoalById(string id)
      {
         // Optional: verify that the core goal exists
         CoreGoal goal = repo.GetCoreGoalById(id);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{id}' not found.");

         // Fetch all courses for this goal
         return repo.GetCoursesByGoalId(id);
      }

      // Insert a new core goal
      public CoreGoal InsertCoreGoal(CoreGoal newGoal)
      {
         // optional: check if ID already exists
         var existingGoal = repo.GetCoreGoalById(newGoal.Id);
         if (existingGoal != null)
            throw new Exception($"CoreGoal with ID '{newGoal.Id}' already exists.");

         return repo.AddCoreGoal(newGoal);
      }

      // Assign existing courses to a core goal
      public void AssignCoursesToCoreGoal(string goalId, List<string> courseNames)
      {
         var goal = repo.GetCoreGoalById(goalId);
         if (goal == null)
            throw new Exception($"CoreGoal with ID '{goalId}' not found.");

         repo.AddCoursesToCoreGoal(goalId, courseNames);
      }

      public bool UpdateCoreGoal(string id, CoreGoal modifiedGoal)
      {
         var existingGoal = repo.GetCoreGoalById(id);
         if (existingGoal == null)
            return false;

         return repo.UpdateCoreGoal(id, modifiedGoal);
      }

      public bool DeleteCoreGoal(string id)
      {
         var existingGoal = repo.GetCoreGoalById(id);
         if (existingGoal == null)
            return false;

         return repo.DeleteCoreGoal(id);
      }

   
   }
}
