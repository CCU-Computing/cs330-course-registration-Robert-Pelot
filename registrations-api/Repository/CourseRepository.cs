using System;
using System.Collections.Generic;
using CourseRegistration.Models;

namespace CourseRegistration.Repository
{
    public class CourseRepository
    {
        public List<Course> Courses { get; set; }
        public List<CoreGoal> Goals { get; set; }
        public List<CourseOffering> Offerings { get; set; }

        public CourseRepository()
        {
            Courses = new List<Course>();
            Goals = new List<CoreGoal>();
            Offerings = new List<CourseOffering>();

            // Existing 4 courses
            Course c1 = new Course()
            {
                Name = "ARTD 201",
                Title = "Graphic Design",
                Credits = 3.0,
                Description = "Graphic design course"
            };
            Course c2 = new Course()
            {
                Name = "ARTS 101",
                Title = "Art Studio",
                Credits = 3.0,
                Description = "Studio art course"
            };
            Course c3 = new Course()
            {
                Name = "STAT 201",
                Title = "Statistics",
                Credits = 4.0,
                Description = "Intro to statistics"
            };
            Course c4 = new Course()
            {
                Name = "ENGL 302",
                Title = "Math as a Communication Language",
                Credits = 4.0,
                Description = "Effective communication using quantitative concepts"
            };

            // 7 new courses
            Course c5 = new Course()
            {
                Name = "CSCI 101",
                Title = "Intro to Computer Science",
                Credits = 3.0,
                Description = "Fundamentals of programming"
            };
            Course c6 = new Course()
            {
                Name = "HIST 201",
                Title = "World History",
                Credits = 3.0,
                Description = "Survey of global history"
            };
            Course c7 = new Course()
            {
                Name = "PHYS 101",
                Title = "Physics I",
                Credits = 4.0,
                Description = "Mechanics and motion"
            };
            Course c8 = new Course()
            {
                Name = "CHEM 101",
                Title = "Chemistry I",
                Credits = 4.0,
                Description = "Introductory chemistry"
            };
            Course c9 = new Course()
            {
                Name = "MATH 301",
                Title = "Calculus II",
                Credits = 4.0,
                Description = "Advanced calculus"
            };
            Course c10 = new Course()
            {
                Name = "ENG 201",
                Title = "English Literature",
                Credits = 3.0,
                Description = "Survey of literature"
            };
            Course c11 = new Course()
            {
                Name = "BIO 101",
                Title = "Biology I",
                Credits = 4.0,
                Description = "Foundations of biology"
            };

            // Add all courses to repository
            Courses.AddRange(new List<Course> { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11 });

            // Course Offerings
            Offerings.Add(new CourseOffering() { TheCourse = c1, Section = "D1", Semester = "Spring 2021" });
            Offerings.Add(new CourseOffering() { TheCourse = c3, Section = "01", Semester = "Spring 2021" });
            Offerings.Add(new CourseOffering() { TheCourse = c2, Section = "01", Semester = "Spring 2022" });
            Offerings.Add(new CourseOffering() { TheCourse = c5, Section = "01", Semester = "Fall 2021" });
            Offerings.Add(new CourseOffering() { TheCourse = c6, Section = "02", Semester = "Spring 2022" });
            Offerings.Add(new CourseOffering() { TheCourse = c7, Section = "01", Semester = "Fall 2022" });
            Offerings.Add(new CourseOffering() { TheCourse = c8, Section = "01", Semester = "Spring 2021" });
            Offerings.Add(new CourseOffering() { TheCourse = c9, Section = "02", Semester = "Fall 2021" });
            Offerings.Add(new CourseOffering() { TheCourse = c10, Section = "01", Semester = "Fall 2022" });
            Offerings.Add(new CourseOffering() { TheCourse = c11, Section = "01", Semester = "Spring 2022" });

            // Core Goals
            Goals.Add(new CoreGoal()
            {
                Id = "CG1",
                Name = "Artistic Expression",
                Description = "Desc for artistic expression",
                Courses = new List<Course>() { c1, c2, c10, c11 } // Art, Studio, Literature
            });
            Goals.Add(new CoreGoal()
            {
                Id = "CG2",
                Name = "Quantitative Literacy",
                Description = "Desc for quantitative literacy",
                Courses = new List<Course>() { c3, c5, c7, c9 } // Stats, CS, Physics, Calculus
            });
            Goals.Add(new CoreGoal()
            {
                Id = "CG3",
                Name = "Effective Communication",
                Description = "Desc for communication",
                Courses = new List<Course>() { c4, c5, c6, c10, c11 } // Math/Comm, History, Biology
            });
        }

        public List<CourseOffering> getOfferingsByGoalIdAndSemester(string theGoalId, string semester)
        {
            CoreGoal theGoal = null;
            foreach (CoreGoal cg in Goals)
            {
                if (cg.Id.Equals(theGoalId))
                {
                    theGoal = cg;
                    break;
                }
            }
            if (theGoal == null) throw new Exception("Didn't find the goal");

            List<CourseOffering> courseOfferingsThatMeetGoal = new List<CourseOffering>();

            foreach (CourseOffering c in Offerings)
            {
                if (c.Semester.Equals(semester) && theGoal.Courses.Contains(c.TheCourse))
                {
                    courseOfferingsThatMeetGoal.Add(c);
                }
            }

            return courseOfferingsThatMeetGoal;
        }
    }
}
