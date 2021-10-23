using System;
using System.Collections.Generic;
using System.Text;

namespace DistrictsNSubjects
{
    public class FederalDistrict
    {
        public ulong Code { get; set; }
        public string Name { get; set; }
        public FederalDistrict(ulong code, string name)
        {
            Code = code;
            Name = name;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Название: {0}\n", Name));
            sb.Append(string.Format("Код: {0}", Code));
            return sb.ToString();
        }
    }
}
