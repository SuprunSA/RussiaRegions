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

        public FederalDistrict ReadFederalDistrictName(List<FederalDistrict> federalDistricts)
        {
            string name;
            FederalDistrict federalDistrict;
            Console.WriteLine("Введите название федерального округа: ");
            name = Console.ReadLine().Trim();
            foreach (var district in federalDistricts)
            {
                if (district.Name == name)
                {
                    return district;
                }
            }
            federalDistrict = new FederalDistrict(ReadFederalDistrictCode(), name);
            federalDistricts.Add(federalDistrict);
            return federalDistrict;
        }

        public string ReadFederalDistrictNameToSTH()
        {
            Console.WriteLine("Введите название федерального округа: ");
            return Console.ReadLine().Trim();
        }

        public uint ReadFederalDistrictCode()
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
    }
}
