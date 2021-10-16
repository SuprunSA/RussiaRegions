using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    public class Subject
    {
        public uint Code { get; }
        public string SubjectName { get; set; }
        public string AdminCenterName { get; set; }
        public float Population { get; set; }
        public float Square { get; set; }
        public float PopulationDencity { get => Population / Square;  set => Math.Round(value, 3); }
        public FederalDistrict FederalDistrict { get; set; }

        public Subject(uint code, string subjectName, FederalDistrict federalDistrict)
        {
            Code = code;
            SubjectName = subjectName;
            FederalDistrict = federalDistrict;
        }

        public Subject(uint code, string subjectName, string adminCenterName, float population, float square, FederalDistrict federalDistrict)
        {
            Code = code;
            SubjectName = subjectName;
            AdminCenterName = adminCenterName;
            Population = population;
            Square = square;
            PopulationDencity = population / square;
            FederalDistrict = federalDistrict;
        } // конструктор для создания Mock списка 23 - 29 в Program

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Название: {0}\n", SubjectName));
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
