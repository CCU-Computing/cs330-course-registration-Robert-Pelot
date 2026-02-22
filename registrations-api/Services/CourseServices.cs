using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;


namespace CourseRegistration.Services
{
   public class CourseServices : ICourseServices
   {
      private CourseRepository repo = new CourseRepository();


      // USER STORY 1 below: 
      //As a student, I want to search for course offerings that meet core goals 
      // so that I can register easily for courses that meet my program requirements

      public List<CourseOffering> getOfferingsByGoalIdAndSemester(String theGoalId, String semester) 
      {
         List<CoreGoal> theGoals = repo.Goals;
         List<CourseOffering> theOfferings = repo.Offerings;
         CoreGoal theGoal=null;
         foreach(CoreGoal cg in theGoals) 
         {
            if(cg.Id.Equals(theGoalId)) 
            {
               theGoal=cg; break;
            }
         }
         if(theGoal==null) throw new Exception("Didn't find the goal");
         List<CourseOffering> courseOfferingsThatMeetGoal = new List<CourseOffering>();
         foreach(CourseOffering c in theOfferings) 
         {
            if(c.Semester.Equals(semester) && theGoal.Courses.Contains(c.TheCourse)) 
            {
               courseOfferingsThatMeetGoal.Add(c);
            }
         }
         return courseOfferingsThatMeetGoal;
      }
      
      // USER STORY 2 below:
      /* As a student, I want to see all available courses so that I know what my options are */
      public List<Course> getCourses()
      {
         List<CourseOffering> offerings = repo.Offerings;
         List<Course> courses = new List<Course>();
         foreach (CourseOffering o in offerings)
         {
            if (!courses.Contains(o.TheCourse))
            {
                  courses.Add(o.TheCourse);
            }
         }
         return courses;
      }

      // USER STORY 3 below:
      /* As a student, I want to see all course offerings by semester, so that I can choose from what's
         available to register for next semester */
      public List<CourseOffering> getCourseOfferingsBySemester(string semester)
      {
         List<CourseOffering> results = new List<CourseOffering>();

         foreach (CourseOffering o in repo.Offerings)
         {
            if (o.Semester.Equals(semester))
            {
                  results.Add(o);
            }
         }
         return results;
      }

      // USER STORY 4 below:
      /* As a student I want to see all course offerings by semester and department so that I can 
      choose major courses to register for */
      public List<CourseOffering> getCourseOfferingsBySemesterAndDept(string semester, string dept)
      {
         List<CourseOffering> results = new List<CourseOffering>();
         foreach (CourseOffering o in repo.Offerings)
         {
            // split "CSCI 201" → ["CSCI", "201"]
            string courseDept = o.TheCourse.Name.Split(' ')[0];
            if (o.Semester.Equals(semester)
                  && courseDept.Equals(dept))
            {
                  results.Add(o);
            }
         }
         return results;
      }

      // USER STORY 5 below:
      /* As a student I want to see all courses that meet a core goal, so that I can plan out
         my courses over the next few semesters and choose core courses that make sense for me */

         public List<Course> getCoursesByGoalId(string goalId)
         {
            CoreGoal theGoal = null;
            foreach (CoreGoal cg in repo.Goals)
            {
               if (cg.Id.Equals(goalId))
               {
                     theGoal = cg;
                     break;
               }
            }
            if (theGoal == null)
               throw new Exception("Didn't find the goal");
            return theGoal.Courses;
         }

      // USER STORY 6 below:
      /* As a student I want to find a course that meets two different core goals, so that I can
      "feed two birds with one seed" (save time by taking one class that will fulfill two 
         requirements */
      public List<Course> getCoursesByGoalIds(params string[] goalIds)
      {
         if (goalIds.Length == 0) return new List<Course>();
         HashSet<Course> intersectionSet = null;
         foreach (string goalId in goalIds)
         {
            CoreGoal theGoal = null;
            foreach (CoreGoal cg in repo.Goals)
            {
                  if (cg.Id.Equals(goalId))
                  {
                     theGoal = cg;
                     break;
                  }
            }
            if (theGoal == null)
                  throw new Exception($"Didn't find the goal {goalId}");
            // For the first goal, start the intersection set
            if (intersectionSet == null)
            {
                  intersectionSet = new HashSet<Course>(theGoal.Courses);
            }
            else
            {
                  // Keep only courses that are already in intersectionSet
                  intersectionSet.IntersectWith(theGoal.Courses);
            }
         }
         return new List<Course>(intersectionSet);
      }

      // USER STORY 7 below:
      /* As a freshman adviser, I want to see all the core goals which do not have any course offerings 
         for a given semester, so that I can work with departments to get some courses offered
         that students can take to meet those goals */
      public List<CoreGoal> getCoreGoalsThatAreNotCoveredBySemester(string semester)
      {
         List<CoreGoal> results = new List<CoreGoal>();
         foreach (CoreGoal cg in repo.Goals)
         {
            bool hasOffering = false;
            foreach (Course c in cg.Courses)
            {
                  foreach (CourseOffering o in repo.Offerings)
                  {
                     if (o.TheCourse == c && o.Semester.Equals(semester))
                     {
                        hasOffering = true;
                        break;
                     }
                  }
                  if (hasOffering) break;
            }
            if (!hasOffering)
            {
                  results.Add(new CoreGoal
                  {
                     Id = cg.Id,
                     Name = cg.Name,
                     Description = cg.Description,
                     Courses = new List<Course>()
                  });
            }
         }
         return results;
      }

      // USER STORY 8 below:
      /* As a student, I want to search for a course by name, so that I
      can find out more information about the course and its offerings */
      public Course getCourseByName(string name)
      {
         foreach (Course c in repo.Courses)
         {
            if (c.Name.Equals(name))
                  return c;
         }
         return null;
      }

      // USER STORY 9 below:
      /* As a student, I want to search for courses by department, so that I can
      find out what courses are offered in my major department */
      public List<Course> searchCoursesByDepartment(string dept)
      {
         List<Course> results = new List<Course>();
         foreach (Course c in repo.Courses)
         {
            string courseDept = c.Name.Split(' ')[0];

            if (courseDept.Equals(dept))
                  results.Add(c);
         }
         return results;
      }

      // USER STORY 10 below:
      /* As a registrar, I want to add a new course to the system, so that
      students can start registering for it */
      public Course addCourse(Course course)
      {
         repo.Courses.Add(course);
         return course;
      }

      // USER STORY 11 below:
      /* As a registrar, I want to update an existing course, so that I can
      keep course information up to date */
      public bool updateCourse(string name, Course updatedCourse)
      {
         foreach (Course c in repo.Courses)
         {
            if (c.Name.Equals(name))
            {
                  c.Title = updatedCourse.Title;
                  c.Credits = updatedCourse.Credits;
                  c.Description = updatedCourse.Description;
                  return true;
            }
         }
         return false;
      }

      // USER STORY 12 below:
      /* As a registrar, I want to delete a course from the system, so that
      students can no longer register for it and it is removed from the course catalog */
      public bool deleteCourse(string name)
      {
         Course courseToRemove = null;

         foreach (Course c in repo.Courses)
         {
            if (c.Name.Equals(name))
            {
                  courseToRemove = c;
                  break;
            }
         }
         if (courseToRemove == null)
            return false;
         repo.Courses.Remove(courseToRemove);
         // Remove from offerings
         repo.Offerings.RemoveAll(o => o.TheCourse == courseToRemove);
         // Remove from goals
         foreach (CoreGoal g in repo.Goals)
         {
            g.Courses.Remove(courseToRemove);
         }
         return true;
      }

      // USER STORY 13 below:
      /* As a student, I want to see all core goals that a course meets, so
      that I can decide if it's a course I want to take to meet my goals */
      public List<CoreGoal> getGoalsByCourseName(string name)
      {
         List<CoreGoal> results = new List<CoreGoal>();
         foreach (CoreGoal g in repo.Goals)
         {
            foreach (Course c in g.Courses)
            {
                  if (c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                  {
                     // Add a copy of the CoreGoal but with courses filtered to only this course
                     results.Add(new CoreGoal
                     {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        Courses = new List<Course>() { c } // only include the matched course
                     });
                     break;
                  }
            }
         }

         return results;
      }

      // USER STORY 14 below:
      /* As a student, I want to see all offerings of a course by semester, so
      that I can plan out when to take a course based on when it's offered */
      public List<CourseOffering> getOfferingsByCourseNameAndSemester(string name, string semester)
      {
         List<CourseOffering> results = new List<CourseOffering>();
         foreach (CourseOffering o in repo.Offerings)
         {
            if (o.TheCourse.Name.Equals(name) && o.Semester.Equals(semester))
            {
                  results.Add(o);
            }
         }
         return results;
      }
     
   }
}
