using System;
using System.Collections.Generic;
using System.Text;

namespace DistrictsNSubjects
{
    public class SubjectDistrictList
    {
        public List<Subject> Subjects = new List<Subject>();
        public List<FederalDistrict> Districts = new List<FederalDistrict>();

        public float CountDistrictPopulationDensity(List<Subject> subjects, string name)
        {
            var population = 0.0f;
            var square = 0.0f;
            foreach (var subject in subjects)
            {
                if (name == subject.FederalDistrict.Name)
                {
                    population += subject.Population;
                    square += subject.Square;
                }
            }
            return population / square;
        }

        public SubjectDistrictList(IEnumerable<Subject> subjects, IEnumerable<FederalDistrict> federalDistricts)
        {
            foreach (var subject in subjects)
            {
                Subjects.Add(subject);
            }

            foreach (var district in federalDistricts)
            {
                Districts.Add(district);
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
