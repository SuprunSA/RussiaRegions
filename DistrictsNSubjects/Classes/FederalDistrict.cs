﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DistrictsNSubjects
{
    public class FederalDistrict
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public double PopulationDencity { get; set; } = 0;
        public FederalDistrict(uint code, string name)
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
