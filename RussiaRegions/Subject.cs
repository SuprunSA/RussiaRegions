using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    public class Subject
    {
        public uint Code { get; }
        public string SubjectName { get; }
        public string AdminCenterName { get; set; }
        public float Population { get; set; }
        public float Square { get; set; }
        public string FederalDistrictName { get; }

        public Subject(uint code, string subjectName, string adminCenterName, float population, float square, string federalDistrict)
        {
            Code = code;
            SubjectName = subjectName;
            AdminCenterName = adminCenterName;
            Population = population;
            Square = square;
            FederalDistrictName = federalDistrict;
        }

        public Subject AddNewSubject(uint code, string subjectName, string adminCenterName, float population, float square, string federalDistrict)
        {
            var subject = new Subject(code, subjectName, adminCenterName, population, square, federalDistrict);
            return subject;
        }
    }
}
