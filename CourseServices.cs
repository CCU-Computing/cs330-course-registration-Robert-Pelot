using System;
using System.Collections.Generic;

namespace cs330_proj1
{
    public class CourseServices
    {
        private CourseRepository repo = new CourseRepository();


        // USER STORY 1 below: 
        //As a student, I want to search for course offerings that meet core goals 
        // so that I can register easily for courses that meet my program requirements

         public List<CourseOffering> getOfferingsByGoalIdAndSemester(String theGoalId, String semester) {
          //finish this method during the tutorial 
         //use the repo to get the data from the database (data store)
         List<CoreGoal> theGoals = repo.Goals;
         List<CourseOffering> theOfferings = repo.Offerings;
                     
         //Complete any other required functionality/business logic to satisfy the requirement
         CoreGoal theGoal=null;
         foreach(CoreGoal cg in theGoals) {
         if(cg.Id.Equals(theGoalId)) {
            theGoal=cg; break;
         
            }
         }
         if(theGoal==null) throw new Exception("Didn't find the goal");
         //search list of courses, then for each course, search offerings
         List<CourseOffering> courseOfferingsThatMeetGoal = new List<CourseOffering>();
                     
         foreach(CourseOffering c in theOfferings) {
            if(c.Semester.Equals(semester) 
               && theGoal.Courses.Contains(c.TheCourse) ) 
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
       
     }
}
