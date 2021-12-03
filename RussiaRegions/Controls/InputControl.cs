using System;
using System.Collections.Generic;
using System.Text;
using DistrictsNSubjects;

namespace RussiaRegions
{
    class InputControl
    {
        public uint ReadSubjectCode()
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

        public string ReadSubjectName()
        {
            Console.WriteLine("Введите название субъекта: ");
            return Console.ReadLine().Trim();
        }

        public string ReadSubjectAdminCenter()
        {
            Console.WriteLine("Введите название административного центра субъекта: ");
            return Console.ReadLine().Trim();
        }

        public float ReadSubjectPopulation()
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

        public float ReadSubjectSquare()
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

        public string ReadFederalDistrictName()
        {
            Console.WriteLine("Введите название федерального округа: ");
            return Console.ReadLine().Trim();
        }

        public string ReadFederalDistrictNameToSTH()
        {
            Console.WriteLine("Введите название федерального округа: ");
            return Console.ReadLine().Trim();
        }

        public uint ReadFederalDistrictCodeToSth()
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

        public District ReadFederalDistrictCode(List<District> districts)
        {
            uint code;
            Console.WriteLine("Введите код федерального округа: ");
            while (!uint.TryParse(Console.ReadLine().Trim(), out code))
            {
                Console.Error.WriteLine("Код - положительное целое число.");
                Console.WriteLine("Введите код федерального округа: ");
            }
            if (districts.Find(d => d.Code == code) != null)
            {
                return districts.Find(d => d.Code == code);
            }
            else 
            { 
                var district = new District(code, ReadFederalDistrictName());
                districts.Add(district);
                return district;
            }
        }

        public void Wait()
        {
            Console.WriteLine("Нажмите любую кнопку, чтобы продолжить...");
            Console.ReadKey();
        }

        public void PrintDistrictList(List<District> federalDistricts)
        {
            Console.WriteLine("Список имеющихся округов: ");
            foreach (var district in federalDistricts)
            {
                Console.WriteLine(district);
            }
        }
    }
}