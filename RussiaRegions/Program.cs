using System.Collections.Generic;
using System;
using System.Linq;
using DistrictsNSubjects;

namespace RussiaRegions
{
    public class Program
    {
        readonly static SubjectDistrictList subjectDistrictList;

        static Program()
        {
            subjectDistrictList = new SubjectDistrictList(
                MockSND.MockSubjects(MockSND.MockDistricts).ToList(), 
                MockSND.MockDistricts.ToList()) { };
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(170, 50);
            Console.SetWindowPosition(0, 0);
            while (MainMenu());
        }

        static readonly Menu mainMenu = new Menu(new MenuItem[] {
            new MenuAction(ConsoleKey.Tab, "Перейти к округам",
                () => { while (DistrictMenu()); }),
            new MenuClose(ConsoleKey.Escape, "Выход"),
        });

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
    }
}
