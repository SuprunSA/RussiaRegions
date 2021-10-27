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
            subjectDistrictList = new SubjectDistrictList(MockSND.MockSubjects(MockSND.MockDistricts).ToList(), MockSND.MockDistricts.ToList())
            {
                Subjects = MockSND.MockSubjects(MockSND.MockDistricts).ToList(),
                Districts = MockSND.MockDistricts.ToList()
            };
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(165, 50);
            Console.SetWindowPosition(0, 0);
            while (MainMenuInput()) ;
        }

        static readonly Menu mainMenu = new Menu(new MenuItem[] {
            new MenuAction(ConsoleKey.Tab, "Перейти к округам",
                () => { while (DistrictMenuInput()); }),
            new MenuClose(ConsoleKey.Escape, "Выход"),
        });

        static bool MainMenuInput()
        {
            Console.Clear();
            subjectDistrictList.SubjectMenu.Print();
            mainMenu.Print();
            subjectDistrictList.PrintAllSubjects();
            var key = Console.ReadKey().Key;
            return mainMenu.Action(key) ? subjectDistrictList.SubjectMenu.Action(key) : false;
        }
        static bool DistrictMenuInput()
        {
            Console.Clear();
            subjectDistrictList.DistrictMenu.Print();
            subjectDistrictList.PrintAllDistricts();
            return subjectDistrictList.DistrictMenu.Action(Console.ReadKey().Key);
        }
    }
}
