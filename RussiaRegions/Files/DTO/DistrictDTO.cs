using DistrictsNSubjects;
using System.Xml.Serialization;

namespace RussiaRegions
{
    [XmlType(TypeName = "District")]
    public class DistrictDTO
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public double PopulationDencity { get; set; }

        public static DistrictDTO Map(FederalDistrict federalDistrict)
        {
            return new DistrictDTO()
            {
                Code = federalDistrict.Code,
                Name = federalDistrict.Name,
                PopulationDencity = federalDistrict.PopulationDencity
            };
        }

        public static FederalDistrict Map(DistrictDTO districtDTO)
        {
            return new FederalDistrict(districtDTO.Code, districtDTO.Name);
        }
    }
}
