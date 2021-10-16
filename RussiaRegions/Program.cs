using System.Collections.Generic;
using System;
using System.Linq;

namespace RussiaRegions
{
    public class Program
    {
        static void Main(string[] args)
        {
            SubjectDistrictList subjectDistrictList = new SubjectDistrictList();

            foreach (var subject in MockSubjects)
            {
                subjectDistrictList.AddSubject(subject);
            }

            foreach (var district in MockDistricts)
            {
                subjectDistrictList.AddDistrict(district);
            }

            do
            {
                Console.Clear();
                subjectDistrictList.AddSubject(new Subject(ReadSubjectCode(), ReadSubjectName(), ReadFederalDistrictName(subjectDistrictList)));
                subjectDistrictList.Subjects[subjectDistrictList.Subjects.Count - 1].AdminCenterName = ReadSubjectAdminCenter();
                subjectDistrictList.Subjects[subjectDistrictList.Subjects.Count - 1].Population = ReadSubjectPopulation();
                subjectDistrictList.Subjects[subjectDistrictList.Subjects.Count - 1].Square = ReadSubjectSquare();
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить, или нажмите Enter, чтобы выйти ");
            } while (Console.ReadKey().Key != ConsoleKey.Enter);

            subjectDistrictList.CountDistrictPopulationDensity(subjectDistrictList.Subjects, subjectDistrictList.Districts);
            PrintAll(subjectDistrictList);
        }

        static IEnumerable<Subject> MockSubjects => new List<Subject>()
        {
            new Subject(1234512, "Самарская область", "Самара", 1200.124f, 233424.34f, new FederalDistrict(235 ,"Центральный")),
            new Subject(2342351, "Ульяновская область", "Ульяновск", 645.124f, 232345.41f, new FederalDistrict(235, "Центральный")),
            new Subject(764575, "Московская область", "Москва", 12435.432f, 4562347.93f, new FederalDistrict(835, "Город федерального значения")),
            new Subject(4353, "Краснодарский край", "Краснодар", 3456.654f, 2743265.75f, new FederalDistrict(7421, "Южный")),
        };

        static IEnumerable<FederalDistrict> MockDistricts => new List<FederalDistrict>()
        {
            new FederalDistrict(235, "Центральный"),
            new FederalDistrict(7421, "Южный"),
            new FederalDistrict(835, "Город федерального значения")
        };

        static uint ReadSubjectCode()
        {
            uint code;
            Console.WriteLine("Введите код ОКАТО субъекта ");
            while (!uint.TryParse(Console.ReadLine().Trim(), out code))
            {
                Console.Error.WriteLine("Код ОКАТО - положительное целое число.");
                Console.WriteLine("Введите код ОКАТО субъекта ");
            }
            return code;
        }

        static string ReadSubjectName()
        {
            Console.WriteLine("Введите название субъекта: ");
            return Console.ReadLine().Trim();
        }

        static string ReadSubjectAdminCenter()
        {
            Console.WriteLine("Введите название административного центра субъекта: ");
            return Console.ReadLine().Trim();
        }

        static float ReadSubjectPopulation()
        {
            float population;
            Console.WriteLine("Введите численность населения субъекта: ");
            while (!float.TryParse(Console.ReadLine().Trim(), out population) || population != Math.Round(population, 3))
            {
                Console.Error.WriteLine("Численность населения - положительное число.");
                Console.WriteLine("Введите численность населения субъекта: ");
            }
            return population;
        }

        static float ReadSubjectSquare()
        {
            float square;
            Console.WriteLine("Введите площадь субъекта: ");
            while (!float.TryParse(Console.ReadLine().Trim(), out square) || square != Math.Round(square, 2))
            {
                Console.Error.WriteLine("Площадь - положительное число.");
                Console.WriteLine("Введите площадь субъекта: ");
            }
            return square;
        }

        static FederalDistrict ReadFederalDistrictName(SubjectDistrictList objectList)
        {
            string name;
            FederalDistrict federalDistrict;
            Console.WriteLine("Введите название федерального округа: ");
            name = Console.ReadLine().Trim();
            foreach (var district in objectList.Districts)
            {
                if (district.Name == name)
                {
                    return district;
                }
            }
                federalDistrict = new FederalDistrict(ReadFederalDistrictCode(), name);
                return federalDistrict;
        }

        static uint ReadFederalDistrictCode()
        {
            uint code;
            Console.WriteLine("Введите код федерального округа: ");
            while (!uint.TryParse(Console.ReadLine().Trim(), out code))
            {
                Console.Error.WriteLine("Код - положительное целое число.");
                Console.WriteLine("Введите код федерального округа: ");
            }
            return code;
        }

        static void PrintAll(SubjectDistrictList subjectDistrictList)
        {
            Console.Clear();
            Console.WriteLine("\tСУБЪЕКТЫ");
            Console.WriteLine();

            foreach (var subject in subjectDistrictList.Subjects)
            {
                Console.WriteLine(subject);
            }

            Console.WriteLine("\tФЕДЕРАЛЬНЫЕ ОКРУГА");
            Console.WriteLine();

            foreach (var district in subjectDistrictList.Districts)
            {
                Console.WriteLine(district);
            }
        }
    }
}
