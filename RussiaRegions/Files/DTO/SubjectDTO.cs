using DistrictsNSubjects;
using System.Xml.Serialization;

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
        public FederalDistrict FederalDistrict { get; set; }

        public static SubjectDTO Map(Subject subject)
        {
            return new SubjectDTO()
            {
                Code = subject.Code,
                Name = subject.Name,
                AdminCenterName = subject.AdminCenterName,
                Population = subject.Population,
                Square = subject.Square,
                FederalDistrict = subject.FederalDistrict
            };
        }

        public static Subject Map(SubjectDTO subjectDTO)
        {
            return new Subject(
                subjectDTO.Code,
                subjectDTO.Name,
                subjectDTO.FederalDistrict)
            {
                AdminCenterName = subjectDTO.AdminCenterName,
                Population = subjectDTO.Population,
                Square = subjectDTO.Square,
                PopulationDencity = subjectDTO.Population / subjectDTO.Square
            };
        }
    }
}
