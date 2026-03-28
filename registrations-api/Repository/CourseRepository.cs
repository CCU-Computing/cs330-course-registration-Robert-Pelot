using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;

namespace CourseRegistration.Repository
{
    public class CourseRepository
    {
        public IEnumerable<Course> AllCourses => GetAllCourses();

        public Course GetCourseByName(string name) => GetCoursebyName(name);

        private MySqlConnection _connection;

        public CourseRepository()
        {
            string connectionString = "server=localhost;user=csci330user;database=courseregistration;port=3306;password=csci330pass";
            _connection = new MySqlConnection(connectionString);
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
        }
    }
}
