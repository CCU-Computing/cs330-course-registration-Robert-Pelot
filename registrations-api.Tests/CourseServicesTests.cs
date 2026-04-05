using CourseRegistration.Models;      // for Course, CoreGoal, CourseOffering
using CourseRegistration.Services;    // for CourseServices
using CourseRegistration.Repository;  // for ICourseRepository
using Moq;                             // for mocking
using Xunit;                           // for tests
using System;
using System.Collections.Generic;
using System.Linq;

namespace registrations_api.Tests
{
    public class CourseServicesTests
    {
        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalNotFound_ExceptionThrown()
        {
            // Arrange
            var mockRepo = new Mock<ICourseRepository>();
            mockRepo.Setup(r => r.GetCoreGoalById("CG5")).Returns((CoreGoal)null);
            var service = new CourseServices(mockRepo.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => service.GetOfferingsByGoalIdAndSemester("CG5", "Spring 2021"));
        }

        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalFoundAndOneCourseOfferingIsInSemester_OfferingIsReturned()
        {
            // Arrange
            var testCourses = GetTestCourses();
            var course = testCourses.First(); // ARTD 201

            var mockRepo = new Mock<ICourseRepository>();

            // Goal exists
            mockRepo.Setup(r => r.GetCoreGoalById("CG1")).Returns(new CoreGoal
            {
                Id = "CG1",
                Name = "English Literacy",
                Description = "test",
                Courses = testCourses
            });

            // Mock offerings for that goal
            mockRepo.Setup(r => r.GetOfferingsByGoalIdAndSemester("CG1", "Spring 2021"))
                .Returns(new List<CourseOffering>
                {
                    new CourseOffering
                    {
                        Semester = "Spring 2021",
                        Section = "1",
                        TheCourse = course
                    }
                });

            var service = new CourseServices(mockRepo.Object);

            // Act
            var result = service.GetOfferingsByGoalIdAndSemester("CG1", "Spring 2021");

            // Assert
            var offering = Assert.Single(result);
            Assert.Equal("Spring 2021", offering.Semester);
            Assert.Equal(course.Name, offering.TheCourse.Name);
        }

        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalFoundAndMultipleCourseOfferingsInSemester_AllOfferingsAreReturned()
        {
            // Arrange
            var testCourses = GetTestCourses(); // assume this returns at least 2 courses
            var course1 = testCourses[0]; // e.g., ARTD 201
            var course2 = testCourses[1]; // e.g., ENGL 102

            var mockRepo = new Mock<ICourseRepository>();

            // Goal exists
            mockRepo.Setup(r => r.GetCoreGoalById("CG1")).Returns(new CoreGoal
            {
                Id = "CG1",
                Name = "English Literacy",
                Description = "test",
                Courses = testCourses
            });

            // Multiple offerings for the semester
            mockRepo.Setup(r => r.GetOfferingsByGoalIdAndSemester("CG1", "Spring 2021"))
                .Returns(new List<CourseOffering>
                {
                    new CourseOffering
                    {
                        Semester = "Spring 2021",
                        Section = "1",
                        TheCourse = course1
                    },
                    new CourseOffering
                    {
                        Semester = "Spring 2021",
                        Section = "2",
                        TheCourse = course2
                    }
                });

            var service = new CourseServices(mockRepo.Object);

            // Act
            var result = service.GetOfferingsByGoalIdAndSemester("CG1", "Spring 2021");

            // Assert
            Assert.Equal(2, result.Count()); // both offerings should be returned
            Assert.Contains(result, o => o.TheCourse.Name == course1.Name);
            Assert.Contains(result, o => o.TheCourse.Name == course2.Name);
        }

        [Fact]
        public void GetOfferingsByGoalIdAndSemester_GoalFoundButNoCourseOfferingInSemester_ReturnsEmptyList()
        {
            // Arrange
            var testCourses = GetTestCourses(); // assume some courses exist
            var mockRepo = new Mock<ICourseRepository>();

            // Goal exists
            mockRepo.Setup(r => r.GetCoreGoalById("CG1")).Returns(new CoreGoal
            {
                Id = "CG1",
                Name = "English Literacy",
                Description = "test",
                Courses = testCourses
            });

            // No offerings for the semester
            mockRepo.Setup(r => r.GetOfferingsByGoalIdAndSemester("CG1", "Fall 2021"))
                .Returns(new List<CourseOffering>()); // empty list

            var service = new CourseServices(mockRepo.Object);

            // Act
            var result = service.GetOfferingsByGoalIdAndSemester("CG1", "Fall 2021");

            // Assert
            Assert.Empty(result); // should return an empty list
        }





        // Helper method to avoid repeating course definitions
        private List<Course> GetTestCourses()
        {
            return new List<Course>
            {
                new Course
                {
                    Name="ARTD 201",
                    Title="graphic design",
                    Credits=3.0,
                    Description="graphic design descr"
                },
                new Course
                {
                    Name="ARTS 101",
                    Title="art studio",
                    Credits=3.0,
                    Description="studio descr"
                }
            };
        }

        // TODO: Add unit tests for:
        // - Multiple course offerings in a semester
        // - Goal exists but no offerings in that semester
    }
}