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
      
      // USER STORY 2 below:
      /* As a student, I want to see all available courses so that I know what my options are */
      public List<Course> GetAllCourses()
      {
         List<Course> theCourses = repo.GetAllCourses().ToList<Course>();
         return theCourses;
      }

      // USER STORY 8 below:
      /* As a student, I want to search for a course by name, so that I
      can find out more information about the course and its offerings */
      public Course GetCourseByName(string name)
      {
         Course c = repo.GetCourseByName(name);
         return c;
      }
      
      // USER STORY 10 below:
      /* As a registrar, I want to add a new course to the system, so that
      students can start registering for it */
      public Course AddCourse(Course newCourse)
      {
         return repo.InsertCourse(newCourse);
      }

      // USER STORY 11 below:
      /* As a registrar, I want to update an existing course, so that I can
      keep course information up to date */
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
      public bool DeleteCourse(string name)
      {
         // Check if course exists
         Course existingCourse = repo.GetAllCourses()
                                    .FirstOrDefault(c => c.Name == name);

         if (existingCourse == null)
         {
            return false;
         }

         // Call repository to delete
         return repo.DeleteCourse(name);
      }
   }
}
