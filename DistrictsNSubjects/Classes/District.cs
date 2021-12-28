using System;
using System.Collections.Generic;
using System.Text;

namespace DistrictsNSubjects
{
    public class District
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public double Population { get; set; } = 0;
        public double Square { get; set; } = 0;
        public double PopulationDencity { get; set; } = 0;
        public District(uint code, string name)
        {
            Code = code;
            Name = name;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0} {1}", Code, Name));
            return sb.ToString();
        }
    }
}
