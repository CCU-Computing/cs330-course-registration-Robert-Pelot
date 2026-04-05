using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Repository;
using CourseRegistration.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CourseRegistration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CourseRepository repo = new CourseRepository();
            CourseServices service = new CourseServices(repo);

            List<CourseOffering> theList = service.GetOfferingsByGoalIdAndSemester("CG2", "Spring 2021").ToList();
                foreach(CourseOffering c in theList)
            {
                Console.WriteLine(c);
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
