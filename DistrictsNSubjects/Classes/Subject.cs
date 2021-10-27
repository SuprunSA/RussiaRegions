using System;
using System.Collections.Generic;
using System.Text;

namespace DistrictsNSubjects
{
    public class Subject
    {
        public uint Code { get; }
        public string Name { get; set; }
        public string AdminCenterName { get; set; }
        public double Population { get; set; }
        public double Square { get; set; }
        public double PopulationDencity { get => Population / Square; set => Math.Round(value, 3); }
        public FederalDistrict FederalDistrict { get; set; }

        public Subject(uint code, string subjectName, FederalDistrict federalDistrict)
        {
            Code = code;
            Name = subjectName;
            FederalDistrict = federalDistrict;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Название: {0}\n", Name));
            sb.Append(string.Format("Код: {0}\n", Code));
            sb.Append(string.Format("Административный центр: {0}\n", AdminCenterName));
            sb.Append(string.Format("Население: {0:# ##0.000} тыс. чел.\n", Population));
            sb.Append(string.Format("Площадь: {0:# ##0.00} кв. м.\n", Square));
            sb.Append(string.Format("Плотность населения: {0:# ##0.000} тыс. чел. / кв. м.\n", PopulationDencity));
            sb.Append(string.Format("Федеральный округ: {0}\n", FederalDistrict.Name));
            return sb.ToString();
        }
    }
}
