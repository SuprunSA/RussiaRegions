using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Model
{
    public class DistrictApiDTO
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubjectApiDTO>? Subjects { get; set; }
        public double Population { 
            get
            {
                if (Subjects != null)
                {
                    return Subjects.Sum(s => s.Population);
                }
                else return 0;
            }
            set
            {
                Math.Round(value, 3);
            }
        }
        public double Square {
            get 
            {
                if (Subjects != null)
                {
                    return Subjects.Sum(s => s.Square);
                }
                else return 0;
            } 
            set
            {
                Math.Round(value, 3);
            }
        }
        public double PopulationDencity { 
            get 
            {
                if (Population != 0)
                {
                    return Population / Square;
                }
                else return 0;
            }
            set
            {
                Math.Round(value, 0);
            }
        } 

        public DistrictApiDTO() { }

        public DistrictApiDTO(DistrictDbDTO district)
        {
            Code = district.Code;
            Name = district.Name;
            Subjects = district.Subjects.Select(s => new SubjectApiDTO(s));
        }

        public DistrictDbDTO Create()
        {
            return new DistrictDbDTO()
            {
                Code = Code,
                Name = Name,
                Subjects = Subjects.Select(s => s.Create()).ToList()
            };
        }
    }
}
