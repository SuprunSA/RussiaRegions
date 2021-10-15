using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    class SubjectCatalog
    {
        public Dictionary<string, Subject> Subjects { get; } = new Dictionary<string, Subject>();
        
        public Subject this[string name]
        {
            get => Subjects[name];
        }

        public void PrintSubject(Subject subject)
        {
            Console.WriteLine("Код субъекта: {0}", Subjects[subject.SubjectName].Code);
            Console.WriteLine("Название субъекта: {0}", Subjects[subject.SubjectName].SubjectName);
            Console.WriteLine("Административный центр: {0}", Subjects[subject.SubjectName].AdminCenterName);
            Console.WriteLine("Федеральный округ: {0}", Subjects[subject.SubjectName].FederalDistrictName);
            Console.WriteLine("Население: {0} тыс. чел.", Subjects[subject.SubjectName].Population);
            Console.WriteLine("Площадь: {0} кв. м.", Subjects[subject.SubjectName].Square); 
            Console.WriteLine("Плотность населения: {0}", Math.Round(Subjects[subject.SubjectName].Population / Subjects[subject.SubjectName].Square, 3));
            Console.WriteLine();
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject.SubjectName, subject);
        }

        public void RemoveSubject(string name)
        {
            Subjects.Remove(name);
        }

        public void RemoveSubject(Subject subject)
        {
            RemoveSubject(subject.SubjectName);
        }

        public IEnumerable<string> SubjectNames => Subjects.Keys;
    }
}
