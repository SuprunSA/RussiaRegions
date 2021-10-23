using System.Collections.Generic;
using System;
using System.Linq;
using DistrictsNSubjects;

namespace RussiaRegions
{
    public class Program
    {
        static void Main(string[] args)
        {
            SubjectDistrictList subjectDistrictList = new SubjectDistrictList(MockSND.MockSubjects(MockSND.MockDistricts), MockSND.MockDistricts);
            InputControl inputControl = new InputControl();
            Console.SetWindowSize(150, 50);
            Console.SetWindowPosition(0, 0);
            while (MainMenuInput(subjectDistrictList, inputControl));
        }

        static readonly Menu MainMenu = new Menu(new[] {
            new MenuItem(ConsoleKey.F1, "Добавить субъект"),
            new MenuItem(ConsoleKey.F2, "Вывести субъекты"),
            new MenuItem(ConsoleKey.F3, "Вывести округа"),
            new MenuItem(ConsoleKey.F4, "Вывести округа и их субъекты"),
            new MenuItem(ConsoleKey.F5, "Удалить субъект"),
            new MenuItem(ConsoleKey.F6, "Удалить округ"),
            new MenuItem(ConsoleKey.Escape, "Выход"),
        });

        static bool MainMenuInput(SubjectDistrictList subjectDistrictList, InputControl inputControl)
        {
            Console.Clear();
            MainMenu.Print();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.F1:
                    Console.Clear();
                    subjectDistrictList.AddSubject(new Subject(inputControl.ReadSubjectCode(), inputControl.ReadSubjectName(), inputControl.ReadFederalDistrictName(subjectDistrictList))
                    {
                        AdminCenterName = inputControl.ReadSubjectAdminCenter(),
                        Population = inputControl.ReadSubjectPopulation(),
                        Square = inputControl.ReadSubjectSquare()
                    });
                    inputControl.Wait();
                    Console.Clear();
                    break;
                case ConsoleKey.F2:
                    Console.Clear();
                    inputControl.PrintSubjects(subjectDistrictList);
                    inputControl.Wait();
                    Console.Clear();
                    break;
                case ConsoleKey.F3:
                    Console.Clear();
                    inputControl.PrintDistricts(subjectDistrictList);
                    inputControl.Wait();
                    Console.Clear();
                    break;
                case ConsoleKey.F4:
                    Console.Clear();
                    inputControl.PrintAll(subjectDistrictList);
                    inputControl.Wait();
                    Console.Clear();
                    break;
                case ConsoleKey.F5:
                    Console.Clear();
                    inputControl.ReadSubjectNameToRemove(subjectDistrictList);
                    inputControl.Wait();
                    Console.Clear();
                    break;
                case ConsoleKey.F6:
                    Console.Clear();
                    inputControl.ReadDistrictNameToRemove(subjectDistrictList);
                    inputControl.Wait();
                    Console.Clear();
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    return false;
            }
            return true;
        }
    }
}
