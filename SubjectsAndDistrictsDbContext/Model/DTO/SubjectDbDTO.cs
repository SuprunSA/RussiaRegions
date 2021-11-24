using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SubjectsAndDistrictsDbContext.Model.DTO
{
    [Table("Subject")]
    public class SubjectDbDTO
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public string AdminCenterName { get; set; }
        public double Population { get; set; }
        public double Square { get; set; }

        public DistrictDbDTO District { get; set; }

    }
}
