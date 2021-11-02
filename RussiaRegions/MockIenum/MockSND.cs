using System.Collections.Generic;
using DistrictsNSubjects;
using System.Linq;

namespace RussiaRegions
{
    static class MockSND
    {
        public static IEnumerable<Subject> MockSubjects(IEnumerable<District> districts)
        {
            return new List<Subject>()
            {
                new Subject(1234512, "Самарская область", districts.ToList().Find(d => d.Code == 235))
                { AdminCenterName = "Самара", Population = 1200.124f, Square = 233424.34f },
                new Subject(2342351, "Ульяновская область", districts.ToList().Find(d => d.Code == 235))
                { AdminCenterName = "Ульяновск", Population = 645.124f, Square = 232345.41f },
                new Subject(764575, "Московская область", districts.ToList().Find(d => d.Code == 235))
                {AdminCenterName = "Москва", Population = 12435.432f, Square = 4562347.93f },
                new Subject(4353, "Краснодарский край", districts.ToList().Find(d => d.Code == 7421))
                { AdminCenterName = "Краснодар", Population = 3456.654f, Square = 2743265.75f }
            };
        }

        public static IEnumerable<District> MockDistricts => new List<District>()
        {
            new District(235, "Центральный"),
            new District(7421, "Южный")
        };
    }
}
