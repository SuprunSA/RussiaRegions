using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SNDDbContext.ModelDB;
using System;
using System.IO;
using System.Linq;

namespace SNDDbContext
{
    class Program
    {
        static readonly string connectionString;
        static Program()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();
            string userId = "", password = "";
            config.Providers.Any(p => p.TryGet("SNDDb:UserId", out userId));
            config.Providers.Any(p => p.TryGet("SNDDb:Password", out password));
            connectionString = string.Format(
                config.GetConnectionString("DefaultConnection"),
                userId, password
            );
        }
        static void Main(string[] args)
        {
            PrintDistrictsNames();
        }

        private static DistrictsAndSubjectsDBContext CreateSNDDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DistrictsAndSubjectsDBContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return new DistrictsAndSubjectsDBContext(options);
        }

        private static void PrintDistrictsNames()
        {
            using (var context = CreateSNDDbContext())
            {
                var districts = context.Districts.ToList();
                foreach (var district in districts)
                {
                    Console.WriteLine(district.Name);
                }
            }
        }
    }
}