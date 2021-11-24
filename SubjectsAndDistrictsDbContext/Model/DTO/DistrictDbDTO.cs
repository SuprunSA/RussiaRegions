using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SubjectsAndDistrictsDbContext.Model.DTO
{
    [Table("Dstrict")]
    public class DistrictDbDTO
    {
        public uint Code { get; set; }
        public string Name { get; set; }

        public ICollection<SubjectDbDTO> Subjects { get; set; } = new HashSet<SubjectDbDTO>();
    }
}
