using System.Collections.Generic;
using CourseRegistration.Models;

namespace CourseRegistration.Repository
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseByName(string name);
        Course InsertCourse(Course newCourse);
        bool UpdateCourse(Course course);
        bool DeleteCourse(string name);

        IEnumerable<CoreGoal> GetAllCoreGoals();
        CoreGoal GetCoreGoalById(string id);
        IEnumerable<Course> GetCoursesByGoalId(string goalId);
        CoreGoal AddCoreGoal(CoreGoal newGoal);
        void AddCoursesToCoreGoal(string goalId, List<string> courseNames);
        bool UpdateCoreGoal(string id, CoreGoal modifiedGoal);
        bool DeleteCoreGoal(string id);
        IEnumerable<CourseOffering> GetOfferingsByGoalIdAndSemester(string goalId, string semester);
        IEnumerable<CourseOffering> GetOfferingsBySemester(string semester);
        IEnumerable<CourseOffering> GetOfferingsBySemesterAndDepartment(string semester, string department);
    }
}