using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;


namespace CourseRegistration.Services
{
    public interface ICourseServices
    {
        List<CourseOffering> getOfferingsByGoalIdAndSemester(String theGoalId, String semester);
        // get all courses:
        List<Course> getCourses();
        // get all course offerings for a given semester:
        List<CourseOffering> getCourseOfferingsBySemester(string semester);
        // get all course offerings for a given semester and department:
        List<CourseOffering> getCourseOfferingsBySemesterAndDept(string semester, string dept);
        // get all course offerings for a given goal id:
        List<Course> getCoursesByGoalId(string goalId);
        // get all course offerings for a given goal ids(more than 1):
        List<Course> getCoursesByGoalIds(params string[] goalIds);
        // get all core goals that are not covered by any course offerings in a given semester:
        List<CoreGoal> getCoreGoalsThatAreNotCoveredBySemester(string semester);

        /// new additions to option below:
        Course getCourseByName(string name);
        List<Course> searchCoursesByDepartment(string dept);
        Course addCourse(Course course);
        bool updateCourse(string name, Course updatedCourse);
        bool deleteCourse(string name);
        List<CoreGoal> getGoalsByCourseName(string name);
        List<CourseOffering> getOfferingsByCourseNameAndSemester(string name, string semester);
    }   
}