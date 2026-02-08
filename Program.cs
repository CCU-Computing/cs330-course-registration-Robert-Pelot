using System;
using cs330_proj1;
using System.Collections.Generic;

namespace cs330courses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("My name is Robert Pelot");
            
            CourseServices service = new CourseServices();

            Console.WriteLine("---------");
<<<<<<< HEAD
<<<<<<< HEAD
            
            // USER STORY 1 below:
=======
            /*
            // USER STORY 1 below:   
>>>>>>> user-story-6
=======
            /*
            // USER STORY 1 below:
>>>>>>> user-story-7
            List<CourseOffering> theList = service.getOfferingsByGoalIdAndSemester("CG2","Spring 2021");
            foreach(CourseOffering c in theList) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");
<<<<<<< HEAD

=======
            
>>>>>>> user-story-7
            // USER STORY 2 below:
            List<Course> theList2 = service.getCourses();
            foreach(Course c in theList2) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            // USER STORY 3 below:
            List<CourseOffering> theList3 = service.getCourseOfferingsBySemester("Fall 2020");
            foreach(CourseOffering c in theList3) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            // USER STORY 4 below:
            List<CourseOffering> theList4 = service.getCourseOfferingsBySemesterAndDept("Spring 2021","ARTD");
            foreach(CourseOffering c in theList4) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");

            // USER STORY 5 below:
            List<Course> theList5 = service.getCoursesByGoalId("CG2");
            foreach(Course c in theList5) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");
<<<<<<< HEAD
            
=======
            */            

<<<<<<< HEAD
>>>>>>> user-story-6
=======
>>>>>>> user-story-7
            // USER STORY 6 below:
            List<Course> theList6 = service.getCoursesByGoalIds("CG2","CG1");
            foreach(Course c in theList6) {
                Console.WriteLine(c);
            }
            Console.WriteLine("---------");
<<<<<<< HEAD

<<<<<<< HEAD
=======
            /*
>>>>>>> user-story-6
=======
            */

>>>>>>> user-story-7
            // USER STORY 7 below:
            List<CoreGoal> theList7 = service.getCoreGoalsThatAreNotCoveredBySemester("Spring 2022");
            foreach(CoreGoal c in theList7) {
                Console.WriteLine(c);
            }
<<<<<<< HEAD
            Console.WriteLine("---------");          

=======
            Console.WriteLine("---------");
<<<<<<< HEAD
           */           
>>>>>>> user-story-6
=======
            

>>>>>>> user-story-7
        }//end main
    }
}