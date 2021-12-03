using DistrictsNSubjects;
using SubjectsAndDistrictsDbContext.Model.DTO;

namespace RussiaRegions.Database.Mapping
{
    public static class DistrictMap
    {
        public static District Map(DistrictDbDTO district)
        {
            if (district == null) return null;
            return new District(district.Code, district.Name);
        }

        public static DistrictDbDTO Map(District district)
        {
            if (district == null) return null;
            return new DistrictDbDTO() 
            { 
                Code = district.Code, 
                Name = district.Name 
            };
        }
    }
}
