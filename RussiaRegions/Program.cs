using System.Collections.Generic;
using System;
using System.Linq;
using DistrictsNSubjects;

namespace RussiaRegions
{
    public class Program
    {
        readonly static SubjectDistrictList subjectDistrictList1;
        readonly static SubjectDistrictList subjectDistrictList2;

        static Program()
        {
            subjectDistrictList1 = new SubjectDistrictList(MockSND.MockSubjects(MockSND.MockDistricts).ToList())
            {
                Subjects = MockSND.MockSubjects(MockSND.MockDistricts).ToList(),
                Districts = MockSND.MockDistricts.ToList()
            };
            subjectDistrictList2 = new SubjectDistrictList(MockSND.MockDistricts.ToList())
            {
                Subjects = subjectDistrictList1.Subjects,
                Districts = subjectDistrictList1.Districts
            };
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(145, 50);
            Console.SetWindowPosition(0, 0);
            while (MainMenuInput());
        }

        static readonly Menu mainMenu = new Menu(new MenuItem[] {
            new MenuAction(ConsoleKey.Tab, "Перейти к округам",
                () => { while (DistrictMenuInput()); }),
            new MenuClose(ConsoleKey.Escape, "Выход"),
        });

        static bool MainMenuInput()
        {
            Console.Clear();
            subjectDistrictList1.SubjectMenu.Print();
            mainMenu.Print();
            subjectDistrictList1.PrintAllSubjects();
            var key = Console.ReadKey().Key;
            return mainMenu.Action(key) ? subjectDistrictList1.SubjectMenu.Action(key) : false;
        }
        static bool DistrictMenuInput()
        {
            Console.Clear();
            subjectDistrictList2.DistrictMenu.Print();
            subjectDistrictList2.PrintAllDistricts();
            return subjectDistrictList2.DistrictMenu.Action(Console.ReadKey().Key);
        }
    }
}
