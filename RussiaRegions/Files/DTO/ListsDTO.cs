using System;
using System.Collections.Generic;
using System.Text;
using DistrictsNSubjects;
using System.Linq;

namespace RussiaRegions
{
    public class ListsDTO
    {
        public DistrictDTO[] Districts { get; set; }
        public SubjectDTO[] Subjects { get; set; }

        public ListsDTO(List<Subject> subjects, List<District> districts)
        {
            Districts = Map(districts.ToArray());
            Subjects = Map(subjects.ToArray());
        }
        
        public ListsDTO(){}

        public DistrictDTO[] Map(District[] districts)
        {
            var districtsDTO = new DistrictDTO[districts.Length];
            for (int i = 0; i < districts.Length; i++)
            {
                districtsDTO[i] = DistrictDTO.Map(districts[i]);
            }
            return districtsDTO;
        }

        public SubjectDTO[] Map(Subject[] subjects)
        {
            var subjectsDTO = new SubjectDTO[subjects.Length];
            for(int i = 0; i < subjects.Length; i++)
            {
                subjectsDTO[i] = SubjectDTO.Map(subjects[i]);
            }
            return subjectsDTO;
        }

        public Subject[] Map(SubjectDTO[] subjectDTOs, List<District> federalDistricts)
        {
            var subjects = new Subject[subjectDTOs.Length];
            for(int i = 0; i < subjectDTOs.Length; i++)
            {
                subjects[i] = SubjectDTO.Map(subjectDTOs[i], federalDistricts);
            }
            return subjects;
        }

        public District[] Map(DistrictDTO[] districtDTOs)
        {
            var districts = new District[districtDTOs.Length];
            for(int i = 0; i < districtDTOs.Length; i++)
            {
                districts[i] = DistrictDTO.Map(districtDTOs[i]);
            }
            return districts;
        }
    }
}
