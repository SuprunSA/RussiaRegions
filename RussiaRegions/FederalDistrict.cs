using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    public class FederalDistrict
    {
        public uint Code { get; }
        public string Name { get; }

        public FederalDistrict(uint code, string name)
        {
            Code = code;
            Name = name;
        }

        public FederalDistrict AddNewFederalDistrict(uint code, string name)
        {
            FederalDistrict federalDistrict = new FederalDistrict(code, name);
            return federalDistrict;
        }
    }
}
