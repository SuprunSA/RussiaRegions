using System;
using System.Linq;
using SubjectsAndDistrictsDbContext;
using Microsoft.EntityFrameworkCore;
using RussiaRegions.Database;
using RussiaRegions.Database.Mapping;
using DistrictsNSubjects;
using System.Collections.Generic;
using SubjectsAndDistrictsDbContext.Model.DTO;

namespace RussiaRegions
{
    public class Program
    {
        static bool useMock = false;
        readonly static SubjectDistrictList subjectDistrictList;

        static readonly Menu mainMenu = new Menu(new MenuItem[] {
            new MenuAction(ConsoleKey.Tab, "Перейти к округам",
                () => { while (DistrictMenu()); }),
            new MenuClose(ConsoleKey.Escape, "Выход"),
        });

        static Program()
        {
            if (useMock)
            {
                subjectDistrictList = new SubjectDistrictList(
                    MockSND.MockSubjects(MockSND.MockDistricts).ToList(),
                    MockSND.MockDistricts.ToList());
            }
            else
            {
                var districts = DatabaseManager.GetDistricts();
                var subjects = DatabaseManager.GetSubjects();

                subjectDistrictList = new SubjectDistrictList(subjects, districts);
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(170, 50);
            Console.SetWindowPosition(0, 0);
            while (MainMenu()) ;
            if (!useMock) DatabaseManager.UpdateDatabase(subjectDistrictList.Subjects, subjectDistrictList.Districts);
            /*FillDatabase();*/
        }

        static bool MainMenu()
        {
            Console.Clear();
            subjectDistrictList.SubjectMenu.Print();
            mainMenu.Print();
            subjectDistrictList.PrintAllSubjects();
            var key = Console.ReadKey().Key;
            return mainMenu.Action(key) && subjectDistrictList.SubjectMenu.Action(key);
        }
        static bool DistrictMenu()
        {
            Console.Clear();
            subjectDistrictList.DistrictMenu.Print();
            subjectDistrictList.PrintAllDistricts();
            return subjectDistrictList.DistrictMenu.Action(Console.ReadKey().Key);
        }

        static void FillDatabase()
        {
            using var context = DatabaseManager.GetContext();
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var districts = MockSND
                                        .MockDistricts
                                        .Select(d => DistrictMap.Map(d))
                                        .Select(d =>
                                        {
                                            d.Subjects = MockSND
                                                         .MockSubjects(MockSND.MockDistricts)
                                                         .Where(s => s.District.Code == d.Code)
                                                         .Select(s => SubjectMap.Map(s))
                                                         .ToList();
                                            return d;
                                        });

                    context.AddRange(districts);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                }
            }
        }
    }
}
