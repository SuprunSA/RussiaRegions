using DistrictsNSubjects;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RussiaRegions
{
    [XmlType(TypeName = "Subject")]
    public class SubjectDTO
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public string AdminCenterName { get; set; }
        public double Population { get; set; }
        public double Square { get; set; }
        public uint FederalDistrictCode { get; set; }

        public static SubjectDTO Map(Subject subject)
        {
            return new SubjectDTO()
            {
                Code = subject.Code,
                Name = subject.Name,
                AdminCenterName = subject.AdminCenterName,
                Population = subject.Population,
                Square = subject.Square,
                FederalDistrictCode = subject.FederalDistrict.Code
            };
        }

        public static Subject Map(SubjectDTO subjectDTO, List<District> federalDistricts)
        {
            return new Subject(
                subjectDTO.Code,
                subjectDTO.Name,
                federalDistricts.Find(d => d.Code == subjectDTO.FederalDistrictCode))
            {
                AdminCenterName = subjectDTO.AdminCenterName,
                Population = subjectDTO.Population,
                Square = subjectDTO.Square,
                PopulationDencity = subjectDTO.Population / subjectDTO.Square
            };
        }
    }
}
