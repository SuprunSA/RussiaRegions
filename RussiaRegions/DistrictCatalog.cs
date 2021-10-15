using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    class DistrictCatalog
    {
        Dictionary<string, FederalDistrict> Districts { get; } = new Dictionary<string, FederalDistrict>();
        public FederalDistrict this[string name]
        {
            get => Districts[name];
        }

        public void CountPopulationDensity(SubjectCatalog subjectsCatalog, FederalDistrict federalDistrict)
        {
            float currPopulation = 0.0f;
            float currSquare = 0.0f;
            foreach (var subjectName in subjectsCatalog.SubjectNames)
            {
                if (federalDistrict.Name == subjectsCatalog[subjectName].FederalDistrictName)
                {
                    currPopulation += subjectsCatalog[subjectName].Population;
                    currSquare += subjectsCatalog[subjectName].Square;
                }
            }
            Console.WriteLine("Население округа: {0}", currPopulation);
            Console.WriteLine("Площадь региона: {0}", currSquare);
            Console.WriteLine("Плотность населения: {0} тыс. чел. / кв. м.", Math.Round(currPopulation / currSquare, 3));
            Console.WriteLine();
        }

        public void PrintDistrict(FederalDistrict federalDistrict)
        {
            Console.WriteLine("Код федерального округа: {0} \t", federalDistrict.Code);
            Console.WriteLine("Название: {0}", federalDistrict.Name);
        }

        public void AddDistrict(FederalDistrict federalDistrict)
        {
            Districts.Add(federalDistrict.Name, federalDistrict);
        }

        public void RemoveDistrict(string name)
        {
            Districts.Remove(name);
        }

        public void RemoveDistrict(FederalDistrict federalDistrict)
        {
            RemoveDistrict(federalDistrict.Name);
        }

        public IEnumerable<string> DistrictNames => Districts.Keys;
    }
}
