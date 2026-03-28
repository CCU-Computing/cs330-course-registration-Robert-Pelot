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

        bool DeleteCourse(string name);
    }   
}