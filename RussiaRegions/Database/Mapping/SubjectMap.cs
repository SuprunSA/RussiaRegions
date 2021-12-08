using DistrictsNSubjects;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace RussiaRegions.Database.Mapping
{
    public static class SubjectMap
    {
        public static Subject Map(SubjectDbDTO subject)
        {
            if (subject == null) return null;
            return new Subject(subject.Code, subject.Name, DistrictMap.Map(subject.District))
            {
                AdminCenterName = subject.AdminCenterName,
                Population = subject.Population,
                Square = subject.Square
            };
        } 

        public static SubjectDbDTO Map(Subject subject)
        {
            if (subject == null) return null;
            return new SubjectDbDTO()
            {
                Code = subject.Code,
                Name = subject.Name,
                District = DistrictMap.Map(subject.District),
                AdminCenterName = subject.AdminCenterName,
                Square = subject.Square,
                Population = subject.Population,
                DistrictId = subject.District.Code
            };
        }
    }
}
