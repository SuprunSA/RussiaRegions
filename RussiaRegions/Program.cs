using System.Collections.Generic;
using System;
using System.Linq;

namespace RussiaRegions
{
    public class Program
    {
        static void Main(string[] args)
        {
            SubjectCatalog subjectCatalog = new SubjectCatalog();
            foreach (var subject in MockSubjects)
            {
                subjectCatalog.AddSubject(subject);
            }

            DistrictCatalog districtCatalog = new DistrictCatalog();
            foreach(var district in MockDistricts)
            {
                districtCatalog.AddDistrict(district);
            }

            do
            {
                subjectCatalog.AddSubject(new Subject(ReadSubjectCode(), 
                    ReadSubjectName(), 
                    ReadSubjectAdminCenter(), 
                    ReadSubjectPopulation(), 
                    ReadSubjectSquare(), 
                    ReadFederalDistrictName(districtCatalog.DistrictNames, districtCatalog))
                    );
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить, или нажмите Enter, чтобы выйти ");
            } while (Console.ReadKey().Key != ConsoleKey.Enter);

            Console.Clear();
            PrintAll(subjectCatalog, districtCatalog);
        }

        static IEnumerable<Subject> MockSubjects => new List<Subject>()
        {
            new Subject(1234512, "Самарская область", "Самара", 1200.124f, 233424.34f, "Центральный"),
            new Subject(2342351, "Ульяновская область", "Ульяновск", 645.124f, 232345.41f, "Центральный"),
            new Subject(764575, "Московская область", "Москва", 12435.432f, 4562347.93f, "Город федерального значения"),
            new Subject(4353, "Краснодарский край", "Краснодар", 3456.654f, 2743265.75f, "Южный"),
        };

        static IEnumerable<FederalDistrict> MockDistricts => new List<FederalDistrict>()
        {
            new FederalDistrict(235, "Центральный"),
            new FederalDistrict(7421, "Южный"),
            new FederalDistrict(835, "Город федерального значения")
        };

        static void FillSubject(SubjectCatalog subjectCatalog, DistrictCatalog districtCatalog, FederalDistrict federalDistrict, Subject subject)
        {
            Console.Clear();
            Console.WriteLine();
        }
        static uint ReadSubjectCode()
        {
            uint code;
            Console.WriteLine();
            Console.WriteLine("Введите код ОКАТО субъекта ");
            while (!uint.TryParse(Console.ReadLine(), out code))
            {
                Console.Error.WriteLine("Код ОКАТО - положительное целое число.");
                Console.WriteLine("Введите код ОКАТО субъекта ");
            }
            return code;
        }

        static string ReadSubjectName()
        {
            Console.WriteLine("Введите название субъекта: ");
            return Console.ReadLine();
        }

        static string ReadSubjectAdminCenter()
        {
            Console.WriteLine("Введите название административного центра субъекта: ");
            return Console.ReadLine();
        }

        static float ReadSubjectPopulation()
        {
            float population;
            Console.WriteLine("Введите численность населения субъекта: ");
            while (!float.TryParse(Console.ReadLine(), out population) || population != Math.Round(population, 3))
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
            while (!float.TryParse(Console.ReadLine(), out square) || square != Math.Round(square, 2))
            {
                Console.Error.WriteLine("Площадь - положительное число.");
                Console.WriteLine("Введите площадь субъекта: ");
            }
            return square;
        }

        static string ReadFederalDistrictName(IEnumerable<string> knownDistricts, DistrictCatalog districtCatalog)
        {
            string name;
            Console.WriteLine("Введите название федерального округа: ");
            name = Console.ReadLine();
            if (!knownDistricts.Contains(name))
            {
                districtCatalog.AddDistrict(new FederalDistrict(ReadFederalDistrictCode(name), name));
            }
            return name;
        }

        static uint ReadFederalDistrictCode(string name)
        {
            uint code;
            Console.WriteLine("Введите код федерального округа: ");
            while (!uint.TryParse(Console.ReadLine(), out code))
            {
                Console.Error.WriteLine("Код - положительное целое число.");
                Console.WriteLine("Введите код федерального округа: ");
            }
            return code;
        }

        static void PrintAll(SubjectCatalog subjectCatalog, DistrictCatalog districtCatalog)
        {
            Console.WriteLine("\tСУБЪЕКТЫ: ");
            Console.WriteLine();
            foreach (var subjectName in subjectCatalog.SubjectNames)
            {
                subjectCatalog.PrintSubject(subjectCatalog[subjectName]);
            }
            Console.WriteLine("\tФЕДЕРАЛЬНЫЕ ОКРУГА: ");
            Console.WriteLine();
            foreach (var districtName in districtCatalog.DistrictNames)
            {
                districtCatalog.PrintDistrict(districtCatalog[districtName]);
                districtCatalog.CountPopulationDensity(subjectCatalog, districtCatalog[districtName]);
            }
        }
    }
}
