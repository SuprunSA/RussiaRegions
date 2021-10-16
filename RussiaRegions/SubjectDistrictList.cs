using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    class SubjectDistrictList
    {
        public List<Subject> Subjects = new List<Subject>();
        public List<FederalDistrict> Districts = new List<FederalDistrict>();

        public void CountDistrictPopulationDensity(List<Subject> subjects, List<FederalDistrict> federalDistricts)
        {
            foreach(var district in federalDistricts)
            {
                var population = 0.0f;
                var square = 0.0f;
                foreach (var subject in subjects)
                {
                    if(district.Name == subject.FederalDistrict.Name)
                    {
                        population += subject.Population;
                        square += subject.Square;
                    }
                }
                district.Population = population;
                district.Square = square;
            }
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
        }

        public void RemoveSubject(Subject subject)
        {
            Subjects.Remove(subject);
        }

        public void AddDistrict(FederalDistrict federalDistrict)
        {
            Districts.Add(federalDistrict);
        }

        public void RemoveDistrict(FederalDistrict federalDistrict)
        {
            RemoveDistrict(federalDistrict);
        }
    }
}
