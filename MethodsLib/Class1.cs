using System;
using static System.Console;

public class methods
{
    static void AddSubject()
        {
            do
            {
                string code = ReadSubjectCode();
                string name = ReadSubjectName();
                string adminDistrict = ReadAdminDistrict();
                decimal population = ReadSubjectPopulationSize();
                decimal square = ReadSubjectSquare();
                string districtName = ReadSubjectFederalDistrictName();

                PrintSubject(code, name, adminDistrict, population, square, districtName);

                WriteLine("Нажмите Esc, чтобы выйти или любую другую клавишу, чтобы продолжить добавлять субъекты");
            } while (ReadKey().Key != ConsoleKey.Escape);
        }

        static void PrintSubject(string code, string name, string adminDistrict, decimal population, decimal square, string districtName)
        {
            WriteLine("\t Код: {0}. \t Наименование: {1}.", code, name);
            WriteLine("Административный центр: {0}.", adminDistrict);
            WriteLine("Население: {0}. \t Площадь: {1}.", population, square);
            WriteLine("Федеральный округ: {0}.", districtName);
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

        static decimal ReadSubjectPopulationSize()
        {
            decimal population;
            Write("Введите численность населения: ");
            while (!decimal.TryParse(ReadLine(), out population) || population < 0 || Math.Round(population, 3) != population)
            {
                Error.WriteLine("Численность населения - положительное число.");
                Write("Введите численность населения: ");
            }
            return population;
        }

        static decimal ReadSubjectSquare()
        {
            decimal square;
            Write("Введите площадь субъекта: ");
            while (!decimal.TryParse(ReadLine(), out square) || square < 0 || Math.Round(square, 2) != square)
            {
                Error.WriteLine("Площадь субъекта - положительное число.");
                Write("Введите площадь cубъекта: ");
            }
            return square;
        }

        static string ReadSubjectFederalDistrictName()
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