using System;
using System.Collections.Generic;
using CourseRegistration.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
=======
using MySql.Data.MySqlClient;
>>>>>>> unittesting

namespace CourseRegistration.Repository
{
    public class CourseRepository : ICourseRepository
    {
<<<<<<< HEAD
        public IEnumerable<Course> AllCourses => GetAllCourses();

        public Course GetCourseByName(string name) => GetCoursebyName(name);

        private MySqlConnection _connection;
=======
        public List<Course> Courses { get; set; }
        public List<CoreGoal> CoreGoals { get; set; }
        public List<CourseOffering> Offerings { get; set; }
>>>>>>> unittesting

        private MySqlConnection _connection;

        public IEnumerable<Course> AllCourses => GetAllCourses();
        public Course GetCourseByName(string name) => GetCoursebyName(name);

        public CourseRepository()
        {
            string connectionString = "server=localhost;user=csci330user;database=courseregistration;port=3306;password=csci330pass";
            _connection = new MySqlConnection(connectionString);
<<<<<<< HEAD
            _connection.Open();           
        }
        ~CourseRepository()
        {
            _connection.Close();
        }

        // NEW METHOD BELOW: get all courses from the database
        public IEnumerable<Course> GetAllCourses()
        {
            var statement = "SELECT * FROM courses";
            var command = new MySqlCommand(statement, _connection);
            var reader = command.ExecuteReader();

            List<Course> newList = new List<Course>(25);
            while(reader.Read())
            {
                Course c = new Course
                {
                    Name = reader.GetString("name"),
                    Title = reader.GetString("title"),
                    Credits = reader.GetDouble("credits"),        
                    Description = reader.GetString("description")
                };
                newList.Add(c);
            }
            reader.Close();
            return newList;
        }

        // NEW METHOD BELOW: get a course by name from the database
        public Course GetCoursebyName(string name)
        {
            var statement = "SELECT * FROM courses WHERE name = @name";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@name", name);
            var reader = command.ExecuteReader();

            Course c = null;
            if(reader.Read())
            {
                c = new Course
                {
                    Name = reader.GetString("name"),
                    Title = reader.GetString("title"),
                    Credits = reader.GetDouble("credits"),        
                    Description = reader.GetString("description")
                };
            }
            reader.Close();
            return c;
        }

        // INSERT NEW COURSE:

        public Course InsertCourse(Course newCourse)
        {
            var statement = "INSERT INTO courses (name, title, credits, description) VALUES (@newName, @newTitle, @newCredits, @newDescription)";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newName", newCourse.Name);
            command.Parameters.AddWithValue("@newTitle", newCourse.Title);
            command.Parameters.AddWithValue("@newCredits", newCourse.Credits);
            command.Parameters.AddWithValue("@newDescription", newCourse.Description);
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                return newCourse;
            } else
            {
                return null;
            }
        }

        // Update a course in the database:

        public bool UpdateCourse(Course course)
        {
            var statement = @"UPDATE courses
                            SET title=@Title, credits=@Credits, description=@Description
                            WHERE name=@Name";

            using (var command = new MySqlCommand(statement, _connection))
            {
                command.Parameters.AddWithValue("@Title", course.Title);
                command.Parameters.AddWithValue("@Credits", course.Credits);
                command.Parameters.AddWithValue("@Description", course.Description);
                command.Parameters.AddWithValue("@Name", course.Name);

                int rowsAffected = command.ExecuteNonQuery(); // returns the number of rows updated
                return rowsAffected > 0;
            }
        }

        // Delete a course in the database:
        public bool DeleteCourse(string name)
        {
            var statement = "DELETE FROM courses WHERE name = @Name";

            using (var command = new MySqlCommand(statement, _connection))
            {
                command.Parameters.AddWithValue("@Name", name);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
=======
            _connection.Open();
        }

        ~CourseRepository()
        {
            _connection.Close();
>>>>>>> unittesting
        }

        // GET ALL COURSES
        public IEnumerable<Course> GetAllCourses()
        {
            var statement = "SELECT * FROM courses";
            var command = new MySqlCommand(statement, _connection);
            var reader = command.ExecuteReader();
            List<Course> newList = new List<Course>(25);

            try
            {
                while (reader.Read())
                {
                    Course c = new Course
                    {
                        Name = reader.GetString("name"),
                        Title = reader.GetString("title"),
                        Credits = reader.GetDouble("credits"),
                        Description = reader.GetString("description")
                    };
                    newList.Add(c);
                }
            }
            finally
            {
                reader.Close(); // ensures reader is closed even on exception
            }

            return newList;
        }

        // GET COURSE BY NAME
        public Course GetCoursebyName(string name)
        {
            var statement = "SELECT * FROM courses WHERE name = @name";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@name", name);
            var reader = command.ExecuteReader();

            Course c = null;
            try
            {
                if (reader.Read())
                {
                    c = new Course
                    {
                        Name = reader.GetString("name"),
                        Title = reader.GetString("title"),
                        Credits = reader.GetDouble("credits"),
                        Description = reader.GetString("description")
                    };
                }
            }
            finally
            {
                reader.Close();
            }

            return c;
        }

        // INSERT NEW COURSE
        public Course InsertCourse(Course newCourse)
        {
            var statement = "INSERT INTO courses (name, title, credits, description) VALUES (@newName, @newTitle, @newCredits, @newDescription)";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newName", newCourse.Name);
            command.Parameters.AddWithValue("@newTitle", newCourse.Title);
            command.Parameters.AddWithValue("@newCredits", newCourse.Credits);
            command.Parameters.AddWithValue("@newDescription", newCourse.Description);

            int result = command.ExecuteNonQuery();
            return result == 1 ? newCourse : null;
        }

        // UPDATE COURSE
        public bool UpdateCourse(Course course)
        {
            var statement = @"UPDATE courses SET title=@Title, credits=@Credits, description=@Description
                            WHERE name=@Name";
            using var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@Title", course.Title);
            command.Parameters.AddWithValue("@Credits", course.Credits);
            command.Parameters.AddWithValue("@Description", course.Description);
            command.Parameters.AddWithValue("@Name", course.Name);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        // DELETE COURSE
        public bool DeleteCourse(string name)
        {
            var statement = "DELETE FROM courses WHERE name = @Name";
            using var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@Name", name);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        // GET ALL CORE GOALS
        public IEnumerable<CoreGoal> GetAllCoreGoals()
        {
            var statement = "SELECT * FROM CoreGoals";
            var command = new MySqlCommand(statement, _connection);
            var reader = command.ExecuteReader();
            List<CoreGoal> goals = new List<CoreGoal>();

            try
            {
                while (reader.Read())
                {
                    goals.Add(new CoreGoal
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Description = reader.GetString("description")
                    });
                }
            }
            finally
            {
                reader.Close();
            }

            return goals;
        }

        // GET CORE GOAL BY ID
        public CoreGoal GetCoreGoalById(string id)
        {
            var statement = "SELECT * FROM CoreGoals WHERE id = @Id";
            using var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new CoreGoal
                {
                    Id = reader.GetString("id"),
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description")
                };
            }

            return null;
        }

        // GET COURSES BY CORE GOAL ID
        public IEnumerable<Course> GetCoursesByGoalId(string goalId)
        {
            var statement = @"
                SELECT c.* 
                FROM Courses c
                JOIN CoreGoalCourses gc ON c.name = gc.CourseName
                WHERE gc.GoalID = @GoalId";
            using var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@GoalId", goalId);
            var reader = command.ExecuteReader();

            List<Course> courses = new List<Course>();
            try
            {
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        Name = reader.GetString("name"),
                        Title = reader.GetString("title"),
                        Credits = reader.GetDouble("credits"),
                        Description = reader.GetString("description")
                    });
                }
            }
            finally
            {
                reader.Close();
            }

            return courses;
        }

        // ADD CORE GOAL
        public CoreGoal AddCoreGoal(CoreGoal newGoal)
        {
            var statement = "INSERT INTO CoreGoals (id, name, description) VALUES (@Id, @Name, @Description)";
            using var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@Id", newGoal.Id);
            command.Parameters.AddWithValue("@Name", newGoal.Name);
            command.Parameters.AddWithValue("@Description", newGoal.Description);
            command.ExecuteNonQuery();
            return newGoal;
        }

        // ASSIGN COURSES TO CORE GOAL
        public void AddCoursesToCoreGoal(string goalId, List<string> courseNames)
        {
            foreach (var courseName in courseNames)
            {
                var statement = @"INSERT INTO CoreGoalCourses (GoalID, CourseName) VALUES (@GoalID, @CourseName)";
                using var command = new MySqlCommand(statement, _connection);
                command.Parameters.AddWithValue("@GoalID", goalId);
                command.Parameters.AddWithValue("@CourseName", courseName);
                command.ExecuteNonQuery();
            }
        }

        // UPDATE CORE GOAL
        public bool UpdateCoreGoal(string id, CoreGoal modifiedGoal)
        {
            var statement = @"UPDATE CoreGoals
                            SET name = @Name, description = @Description
                            WHERE id = @Id";

            using var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@Name", modifiedGoal.Name);
            command.Parameters.AddWithValue("@Description", modifiedGoal.Description);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        // DELETE CORE GOAL
        public bool DeleteCoreGoal(string id)
        {
            using var transaction = _connection.BeginTransaction();
            try
            {
                // Delete associated rows in CoreGoalCourses
                var deleteCoursesStmt = "DELETE FROM CoreGoalCourses WHERE GoalID = @Id";
                using var cmd1 = new MySqlCommand(deleteCoursesStmt, _connection, transaction);
                cmd1.Parameters.AddWithValue("@Id", id);
                cmd1.ExecuteNonQuery();

                // Delete the CoreGoal itself
                var deleteGoalStmt = "DELETE FROM CoreGoals WHERE id = @Id";
                using var cmd2 = new MySqlCommand(deleteGoalStmt, _connection, transaction);
                cmd2.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd2.ExecuteNonQuery();

                transaction.Commit();
                return rowsAffected > 0;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        
        // GET OFFERINGS BY CORE GOAL ID AND SEMESTER
// GET OFFERINGS BY CORE GOAL ID AND SEMESTER
        public IEnumerable<CourseOffering> GetOfferingsByGoalIdAndSemester(string goalId, string semester)
        {
            // Step 1: Get all course names for the goal and semester
            var statement = @"
                SELECT o.Course AS CourseName, o.Semester, o.Section
                FROM CourseOfferings o
                JOIN CoreGoalCourses gc ON o.Course = gc.CourseName
                WHERE gc.GoalID = @GoalId AND o.Semester = @Semester";

            List<CourseOffering> offerings = new List<CourseOffering>();
            List<string> courseNames = new List<string>();

            using (var command = new MySqlCommand(statement, _connection))
            {
                command.Parameters.AddWithValue("@GoalId", goalId);
                command.Parameters.AddWithValue("@Semester", semester);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Save course names for later
                        courseNames.Add(reader.GetString("CourseName"));

                        // Save semester and section info temporarily
                        offerings.Add(new CourseOffering
                        {
                            Semester = reader.GetString("Semester"),
                            Section = reader.GetString("Section")
                            // We'll fill TheCourse next
                        });
                    }
                }
            }

            // Step 2: Now fetch Course objects for each course name
            for (int i = 0; i < offerings.Count; i++)
            {
                offerings[i].TheCourse = GetCoursebyName(courseNames[i]);
            }

            return offerings;
        }

        public IEnumerable<CourseOffering> GetOfferingsBySemester(string semester)
        {
            var statement = @"
                SELECT o.Course AS CourseName, o.Semester, o.Section
                FROM CourseOfferings o
                WHERE o.Semester = @Semester";

            List<CourseOffering> offerings = new List<CourseOffering>();
            List<string> courseNames = new List<string>();

            using (var command = new MySqlCommand(statement, _connection))
            {
                command.Parameters.AddWithValue("@Semester", semester);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseNames.Add(reader.GetString("CourseName"));
                        offerings.Add(new CourseOffering
                        {
                            Semester = reader.GetString("Semester"),
                            Section = reader.GetString("Section")
                        });
                    }
                }
            }

            // Populate the Course objects
            for (int i = 0; i < offerings.Count; i++)
            {
                offerings[i].TheCourse = GetCoursebyName(courseNames[i]);
            }

            return offerings;
        }

        public IEnumerable<CourseOffering> GetOfferingsBySemesterAndDepartment(string semester, string department)
        {
            // Step 1: Get all course offerings for the semester and department
            var statement = @"
                SELECT o.Course AS CourseName, o.Semester, o.Section
                FROM CourseOfferings o
                JOIN Courses c ON o.Course = c.name
                WHERE o.Semester = @Semester
                AND LEFT(c.name, 4) = @Department";

            List<CourseOffering> offerings = new List<CourseOffering>();
            List<string> courseNames = new List<string>();

            using (var command = new MySqlCommand(statement, _connection))
            {
                command.Parameters.AddWithValue("@Semester", semester);
                command.Parameters.AddWithValue("@Department", department);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseNames.Add(reader.GetString("CourseName"));
                        offerings.Add(new CourseOffering
                        {
                            Semester = reader.GetString("Semester"),
                            Section = reader.GetString("Section")
                            // We'll fill TheCourse next
                        });
                    }
                }
            }

            // Step 2: Fetch Course objects for each course name
            for (int i = 0; i < offerings.Count; i++)
            {
                offerings[i].TheCourse = GetCoursebyName(courseNames[i]);
            }

            return offerings;
        }




            

    }
}