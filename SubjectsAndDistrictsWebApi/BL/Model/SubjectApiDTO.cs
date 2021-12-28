using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Model
{
    public class SubjectApiDTO
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public string AdminCenterName { get; set; }
        public double Population { get; set; }
        public double Square { get; set; }
        public double PopulationDencity => Math.Round(Population / Square, 3);
        public uint DistrictId { get; set; }

        public SubjectApiDTO() { }

        public SubjectApiDTO(SubjectDbDTO subject)
        {
            Code = subject.Code;
            Name = subject.Name;
            AdminCenterName = subject.AdminCenterName;
            Population = subject.Population;
            Square = subject.Square;
            DistrictId = subject.DistrictId;
        }

        public SubjectDbDTO Create()
        {
            return new SubjectDbDTO()
            {
                Code = Code,
                Name = Name,
                AdminCenterName = AdminCenterName,
                Population = Population,
                Square = Square,
                DistrictId = DistrictId
            };
        }
    }
}
