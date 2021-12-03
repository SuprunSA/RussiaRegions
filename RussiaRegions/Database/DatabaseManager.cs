using DistrictsNSubjects;
using Microsoft.EntityFrameworkCore;
using RussiaRegions.Database.Mapping;
using SubjectsAndDistrictsDbContext;
using SubjectsAndDistrictsDbContext.Connection;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RussiaRegions.Database
{
    public static class DatabaseManager
    {
        public static readonly string connectionString = new ConnectionStringConfig().ConnectionString;

        public static SubjectsAndDistrictsContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SubjectsAndDistrictsContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            return new SubjectsAndDistrictsContext(options);
        }

        public static List<District> GetDistricts()
        {
            using var context = GetContext();
            {
                return context
                        .Districts
                        .Select(d => DistrictMap.Map(d))
                        .ToList();
            }
        }

        public static List<Subject> GetSubjects()
        {
            using var context = GetContext();
            {
                return context
                        .Subjects
                        .Include(s => s.District)
                        .Select(s => SubjectMap.Map(s))
                        .ToList();
            }
        }

        public static void UpdateDatabase(IEnumerable<Subject> subjects, IEnumerable<District> districts)
        {
            Console.Clear();
            using var context = GetContext();
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {

                    var districtsInDatabase = context
                                                .Districts
                                                .AsNoTracking();

                    foreach (var districtInDatabase in districtsInDatabase)
                    {
                        var district = districts
                                            .FirstOrDefault(d => d.Code == districtInDatabase.Code);

                        if (district != null)
                        {
                            var distToUpdate = DistrictMap.Map(district);
                            List<SubjectDbDTO> subjectsToUpdate = subjects
                                                                    .Where(s => s.District.Code == district.Code)
                                                                    .Select(s => SubjectMap.Map(s))
                                                                    .ToList();

                            distToUpdate.Subjects = subjectsToUpdate;
                            context.Update(distToUpdate);
                        }
                        else context.Remove(districtInDatabase);
                    }

                    context.SaveChanges();
                    
                    var districtsToAdd = districts
                                            .Where(d => context.Districts.Find(d.Code) == null)
                                            .Select(d => DistrictMap.Map(d))
                                            .Select(d =>
                                            {
                                                d.Subjects = subjects
                                                                .Where(s => s.District.Code == d.Code)
                                                                .Select(s => SubjectMap.Map(s))
                                                                .ToList();
                                                return d;
                                            });
                                            

                    context.AddRange(districtsToAdd);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("При обновлении базы произошла ошибка: {0}", ex.Message);
                }
            }
        }
    }
}
