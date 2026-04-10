using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;


namespace CourseRegistration.Services
{
    public interface ICourseServices
    {
        List<Course> GetAllCourses();       
        Course GetCourseByName(string name);
        Course AddCourse(Course newCourse);
        bool updateCourse(string name, Course updatedCourse);
<<<<<<< HEAD

        bool DeleteCourse(string name);
=======
        bool DeleteCourse(string name);
        List<CoreGoal> GetAllCoreGoals();
        CoreGoal GetCoreGoalById(string id);
        CoreGoal GetCoreGoalWithCoursesById(string id);
        IEnumerable<Course> GetCoursesForCoreGoalById(string id);
        CoreGoal InsertCoreGoal(CoreGoal newGoal);
        void AssignCoursesToCoreGoal(string goalId, List<string> courseNames);
        bool UpdateCoreGoal(string id, CoreGoal modifiedGoal);
        bool DeleteCoreGoal(string id);
        IEnumerable<CourseOffering> GetOfferingsByGoalIdAndSemester(string goalId, string semester);
        IEnumerable<CourseOffering> GetOfferingsBySemester(string semester);
        IEnumerable<CourseOffering> GetOfferingsBySemesterAndDepartment(string semester, string department);
>>>>>>> unittesting
    }   
}