using System;
using static System.Console;
using System.Collections.Generic;

namespace RussiaRegions
{
    class Program
    {
        static Dictionary<string, string> DistrictNames = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            AddSubject();
        }
        static void AddSubject()
        {
            string districtName = ReadFederalDistrictName();
            do
            {
                string code = ReadSubjectCode();
                string name = ReadSubjectName();
                string adminDistrict = ReadAdminDistrict();
                double population = ReadSubjectPopulationSize();
                double square = ReadSubjectSquare();
                

                PrintSubject(code, name, adminDistrict, population, square, districtName);

                WriteLine("Нажмите Esc, чтобы выйти или любую другую клавишу, чтобы продолжить добавлять субъекты");
            } while (ReadKey().Key != ConsoleKey.Escape);
        }

        static void PrintSubject(string code, string name, string adminDistrict, double population, double square, string districtName)
        {
            string codeDist = "";
            WriteLine("\t Код: {0}. \t Наименование: {1}.", code, name);
            WriteLine("Административный центр: {0}.", adminDistrict);
            WriteLine("Население, тыс. чел.: {0}. \t Площадь, кв. м.: {1}.", population, square);
            foreach (string code_district in DistrictNames.Keys)
            {
                if (DistrictNames[code_district] == districtName)
                {
                    codeDist = code_district;
                    break;
                }
            }
            WriteLine("Федеральный округ: {0}. \t Код федерального округа: {1}", districtName, codeDist);
        }

        static string ReadSubjectCode()
        {
            Write("\n Введите код ОКАТО: ");
            string code = ReadLine();
            while (!ulong.TryParse(code, out _))
            {
                Error.WriteLine("Код ОКАТО должен состоять из цифр.");
                Write("Введите код ОКАТО: ");
                code = ReadLine();
            }
            return code;
        }

        static string ReadSubjectName()
        {
            Write("Введите название субъекта: ");
            string name;
            name = ReadLine();
            return name;
        }

        static string ReadAdminDistrict()
        {
            Write("Введите название административного центра: ");
            string adminDistrict;
            adminDistrict = ReadLine();
            return adminDistrict;
        }

        static double ReadSubjectPopulationSize()
        {
            double population;
            Write("Введите численность населения: ");
            while (!double.TryParse(ReadLine(), out population) || population < 0 || Math.Round(population, 3) != population)
            {
                Error.WriteLine("Численность населения - положительное число.");
                Write("Введите численность населения: ");
            }
            return population;
        }

        static double ReadSubjectSquare()
        {
            double square;
            Write("Введите площадь субъекта: ");
            while (!double.TryParse(ReadLine(), out square) || square < 0)
            {
                Error.WriteLine("Площадь субъекта - положительное число.");
                Write("Введите площадь cубъекта: ");
            }
            return square;
        }

        static string ReadFederalDistrictName()
        {
            Write("Введите название федерального округа: ");
            string districtName;
            districtName = ReadLine();
            if (DistrictNames.ContainsValue(districtName))
            {
                return districtName;
            }
            else
            {
                AddDistrictCode(districtName);
                return districtName;
            }

        }

        static void AddDistrictCode(string districtName)
        {
            Write("Введите код федерального округа: ");
            string code;
            code = ReadLine();
            while (!ulong.TryParse(code, out _))
            {
                Error.WriteLine("Код федерального округа должен состоять из цифр");
                Write("Введите код федерального округа: ");
                code = ReadLine();
            }
            DistrictNames.Add(code, districtName);
        }
    }
}
