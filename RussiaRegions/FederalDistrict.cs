using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    public class FederalDistrict
    {
        public uint Code { get; set; }
        public string Name { get; set; }
        public float Population { get; set; }
        public float Square { get; set; }
        public float PopulationDensity { get => Population / Square; set => Math.Round(value, 3); }
        public FederalDistrict(uint code, string name)
        {
            Code = code;
            Name = name;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Название: {0}\n", Name));
            sb.Append(string.Format("Код: {0}\n", Code));
            sb.Append(string.Format("Население: {0:# ##0.00} тыс. чел.\n", Population));
            sb.Append(string.Format("Площадь: {0:# ##0.00} кв. м.\n", Square));
            sb.Append(string.Format("Население: {0:# ##0.000}тыс. чел. / кв. м.\n", PopulationDensity));
            return sb.ToString();
        }
    }
}
