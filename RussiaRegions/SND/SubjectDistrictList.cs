using System;
using System.Collections.Generic;
using System.Text;
using DistrictsNSubjects;
using System.Linq;

namespace RussiaRegions
{
    class SubjectDistrictList
    {
        public List<Subject> Subjects = new List<Subject>();
        readonly Menu SubjectSortDescMenu;
        readonly Menu SubjectSortMenu;
        readonly Menu SubjectChangeMenu;
        public Menu SubjectMenu;
        public ListSelector<Subject> SelectSubject { get; }
        public Subject SelectedSubject => SelectSubject.SelectedNode;
        Func<Subject, object> SubjectOrderBy = subject => subject.Population;
        public bool SubjectOrderByDescending = false;
        string subjectSearchBy = null;
        string SubjectSearchBy { get => subjectSearchBy; set => subjectSearchBy = value.ToLower(); }
        IList<Subject> OrderedSubjects
        {
            get
            {
                var subjects = Subjects;
                if (!string.IsNullOrEmpty(subjectSearchBy))
                {
                    subjects = subjects.Where(subject =>
                        subject.FederalDistrict.Name.ToLower().Contains(SubjectSearchBy)
                    ).ToList();
                }
                if (!SubjectOrderByDescending)
                {
                    subjects = subjects.OrderBy(SubjectOrderBy).ToList();
                }
                else
                {
                    subjects = subjects.OrderByDescending(SubjectOrderBy).ToList();
                }
                return subjects.ToList();
            }
        }
        Table<Subject> subjectTable = new Table<Subject>(new[]
        {
            new TableColumn<Subject>("Название", 20, subject => subject.Name),

            new TableColumn<Subject>("Код ОКАТО", 15, subject => subject.Code.ToString()),

            new TableColumn<Subject>("Административный центр", 23, subject => subject.AdminCenterName),

            new TableColumn<Subject>("Население", 25, subject => string.Format("{0:# ##0.000} тыс.чел.", subject.Population)),

            new TableColumn<Subject>("Площадь", 20, subject => string.Format("{0:# ##0.00} кв. м.", subject.Square)),

            new TableColumn<Subject>("Плотность населения", 23, subject => string.Format("{0:# ##0.000} тыс. чел. / кв. м.", subject.PopulationDencity)),

            new TableColumn<Subject>("Название округа", 17, subject => subject.FederalDistrict.Name),

            new TableColumn<Subject>("Код округа", 12, subject => subject.FederalDistrict.Code.ToString())
        });

        public List<FederalDistrict> Districts = new List<FederalDistrict>();
        public Menu DistrictMenu;
        readonly Menu DistrictDescMenu;
        readonly Menu DistrictChangeMenu;
        public ListSelector<FederalDistrict> SelectDistrict { get; }
        public FederalDistrict SelectedDistrict => SelectDistrict.SelectedNode;
        Func<FederalDistrict, object> DistrictOrderBy = district => district.PopulationDencity;
        public bool DistrictOrderByDescending = false;
        IList<FederalDistrict> OrderedDistricts
        {
            get
            {
                var districts = Districts;
                if (!DistrictOrderByDescending)
                {
                    districts = districts.OrderBy(DistrictOrderBy).ToList();
                }
                else
                {
                    districts = districts.OrderByDescending(DistrictOrderBy).ToList();
                }
                return districts.ToList();
            }
        }
        Table<FederalDistrict> districtTable = new Table<FederalDistrict>(new[]
                {
                    new TableColumn<FederalDistrict>("Название", 30, district => string.Format("{0} федеральный округ", district.Name)),

                    new TableColumn<FederalDistrict>("Код ОКЭР", 10, district => district.Code.ToString()),

                    new TableColumn<FederalDistrict>("Плотность населения", 23, district => district.PopulationDencity.ToString())
                });

        public SubjectDistrictList(List<Subject> subjects, List<FederalDistrict> federalDistricts)
        {
            subjects = Subjects;
            InputControl inputControl = new InputControl();
            SelectSubject = new ListSelector<Subject>(() => OrderedSubjects);
            SubjectMenu = new Menu(new List<MenuItem>(SelectSubject.Menu.Items) {
                new MenuAction(ConsoleKey.F1, "Новый субъект", () => AddSubject(inputControl)),

                new MenuAction(ConsoleKey.F2, "Удаление субъекта", () => RemoveSubject(SelectedSubject)),

                new MenuAction(ConsoleKey.F3, "Изменение субъекта", ChangeSubject),

                new MenuAction(ConsoleKey.F4, "Сортировать", ChooseSort),

                new MenuAction(ConsoleKey.F5, "Фильтр по федеральным округам", DistrictFilter),

                new MenuAction(ConsoleKey.F6, "Поиск по названию", SearchSubjectByName)
            });
            SubjectSortMenu = new Menu(new List<MenuItem>() {
                new MenuAction(ConsoleKey.D1, "Сортировка по численности наcеления",
                    () => SubjectOrderBy = subject => subject.Population),

                new MenuAction(ConsoleKey.D2, "Сортировка по площади",
                    () => SubjectOrderBy = subject => subject.Square),

                new MenuAction(ConsoleKey.D3, "Сортировка по плотности населения",
                    () => SubjectOrderBy = subject => subject.PopulationDencity),

                new MenuClose(ConsoleKey.Escape, "Выход")
            });
            SubjectSortDescMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "По возрастанию", () => SubjectOrderByDescending = false),
                new MenuAction(ConsoleKey.D2, "По убыванию", () => SubjectOrderByDescending = true)
            });
            SubjectChangeMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "Изменить название", () =>
                    ChangeSubjectName(SelectedSubject, inputControl)),

                new MenuAction(ConsoleKey.D2, "Изменить административный центр", () =>
                    ChangeAdminCenter(SelectedSubject, inputControl)),

                new MenuAction(ConsoleKey.D3, "Изменить население", () =>
                    ChangePopulation(SelectedSubject, inputControl)),

                new MenuAction(ConsoleKey.D4, "Изменить площадь", () =>
                    ChangeSquare(SelectedSubject, inputControl)),

                new MenuAction(ConsoleKey.D5, "Изменить округ", () =>
                    ChangeDistrict(SelectedSubject, inputControl)),

                new MenuClose(ConsoleKey.Escape, "Выход")
            });

            federalDistricts = Districts;
            SelectDistrict = new ListSelector<FederalDistrict>(() => OrderedDistricts);
            DistrictMenu = new Menu(new List<MenuItem>(SelectDistrict.Menu.Items) {
                new MenuAction(ConsoleKey.F1, "Изменение округа", ChangeDistrict),

                new MenuAction(ConsoleKey.F2, "Удаление округа", () => RemoveDistrict(SelectedDistrict)),

                new MenuAction(ConsoleKey.F3, "Сортировать по плотности населения", PopulationDencitySort),

                new MenuAction(ConsoleKey.F4, "Поиск по названию", () => SearchDistrictByName(inputControl)),

                new MenuAction(ConsoleKey.F5, "Поиск по коду", () => SearchDistrictByCode(inputControl)),

                new MenuClose(ConsoleKey.Tab, "Вернуться к субъектам")
            });
            DistrictDescMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "По возрастанию", () => DistrictOrderByDescending = false),
                new MenuAction(ConsoleKey.D2, "По убыванию", () => DistrictOrderByDescending = true)
            });
            DistrictChangeMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "Изменить название", () => ChangeDistrictName(SelectedDistrict, inputControl)),

                new MenuClose(ConsoleKey.Escape, "Выход")
            });
        }

        #region Districts
        public void SearchDistrictByCode(InputControl inputControl)
        {
            Console.Clear();
            var code = inputControl.ReadFederalDistrictCode();
            var district = Districts.Find(d => d.Code == code);
            if (district == null)
            {
                Console.WriteLine("Округа с таким кодом не найдено");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }
            else
            {
                SelectDistrict.SelectedNode = district;
            }
        }

        public void SearchDistrictByName(InputControl inputControl)
        {
            Console.Clear();
            var name = inputControl.ReadFederalDistrictNameToSTH();
            var district = Districts.Find(d => d.Name == name);
            if (district == null)
            {
                Console.WriteLine("Округа с таким названием не найдено");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }
            else
            {
                SelectDistrict.SelectedNode = district;
            }
        }

        public void PopulationDencitySort()
        {
            Console.Clear();
            DistrictDescMenu.Print();
            DistrictDescMenu.Action(Console.ReadKey().Key);
        }

        public void ChangeDistrict()
        {
            Console.Clear();
            DistrictChangeMenu.Print();
            districtTable.Print(OrderedDistricts, SelectedDistrict);
            DistrictChangeMenu.Action(Console.ReadKey().Key);
        }

        public void ChangeDistrictName(FederalDistrict federalDistrict, InputControl inputControl)
        {
            Console.Clear();
            var name = inputControl.ReadFederalDistrictNameToSTH();
            foreach (var subject in Subjects)
            {
                if (subject.FederalDistrict.Code == federalDistrict.Code)
                {
                    subject.FederalDistrict.Name = name;
                }
            }
            Districts.Find(d => d.Code == federalDistrict.Code).Name = name;
        }

        public void RemoveDistrict(FederalDistrict federalDistrict)
        {
            Districts.Remove(federalDistrict);
            Subjects.RemoveAll(s => s.FederalDistrict.Code == federalDistrict.Code);
            SelectDistrict.SelectedNodeIndex--;
        }

        public void CountPopulationDensity()
        {
            foreach(var district in Districts)
            {
                var population = 0.0;
                var square = 0.0;
                foreach(var subject in Subjects)
                {
                    if (subject.FederalDistrict.Code == district.Code)
                    {
                        population += subject.Population;
                        square += subject.Square;
                    }
                }
                district.PopulationDencity = Math.Round(population / square, 3);
            }
        }

        public void PrintAllDistricts()
        {
            CountPopulationDensity();
            districtTable.Print(OrderedDistricts, SelectedDistrict);
        }
        #endregion

        #region Subjects
        public void ChangeDistrict(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            var name = inputControl.ReadFederalDistrictNameToSTH();
            if (Subjects.Where(s => s.FederalDistrict.Code == subject.FederalDistrict.Code).Count() == 1)
            {
                RemoveDistrict(Districts.Find(d => d.Code == subject.FederalDistrict.Code));
            }
            if (Districts.Find(d => d.Name == name) != null)
            {
                Subjects.Find(s => s == subject).FederalDistrict = Districts.Find(d => d.Name == name);
            }
            else
            {
                var code = inputControl.ReadFederalDistrictCode();
                subject.FederalDistrict = new FederalDistrict(code, name);
                AddDistrict(subject.FederalDistrict);
            }
        }

        public void AddDistrict(FederalDistrict federalDistrict)
        {
            Districts.Add(federalDistrict);
        }

        public void ChangeSubjectName(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.Name = inputControl.ReadSubjectName();
        }

        public void ChangeAdminCenter(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.AdminCenterName = inputControl.ReadSubjectAdminCenter();
        }
        public void ChangePopulation(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.Population = inputControl.ReadSubjectPopulation();
        }

        public void ChangeSquare(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.Square = inputControl.ReadSubjectSquare();
        }

        public void AddSubject(InputControl inputControl)
        {
            Console.Clear();
            AddSubject(new Subject(
                inputControl.ReadSubjectCode(),
                inputControl.ReadSubjectName(),
                inputControl.ReadFederalDistrictName(Districts))
            {
                AdminCenterName = inputControl.ReadSubjectAdminCenter(),
                Population = inputControl.ReadSubjectPopulation(),
                Square = inputControl.ReadSubjectSquare()
            });
        }

        public void RemoveSubject(Subject subject)
        {
            Console.Clear();
            var subjects = Subjects.Where(s => s.FederalDistrict.Code == subject.FederalDistrict.Code);
            if (subjects.Count() == 1)
            {
                Districts.RemoveAll(d => d.Code == subject.FederalDistrict.Code);
            }
            Subjects.Remove(subject);
            SelectSubject.SelectedNodeIndex--;
        }

        public void ChangeSubject()
        {
            Console.Clear();
            SubjectChangeMenu.Print();
            PrintAllSubjects();
            SubjectChangeMenu.Action(Console.ReadKey().Key);
        }

        public void ChooseSort()
        {
            Console.Clear();
            SubjectSortMenu.Print();
            PrintAllSubjects();
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Escape)
            {
                return;
            }
            SubjectSortMenu.Action(key);
            SwitchDesc();
        }

        public void DistrictFilter()
        {
            Console.Clear();
            Console.WriteLine("Введите федеральный округ, по которому сортировать субъекты, или пустое значение, чтобы вывести все субъекты: ");
            SubjectSearchBy = Console.ReadLine().Trim();
        }

        public void SearchSubjectByName()
        {
            Console.Clear();
            Console.WriteLine("Введите название субъекта для поиска: ");
            var find = Console.ReadLine().Trim();
            var foundSubject = Subjects.Find(s => s.Name == find);
            if (foundSubject == null)
            {
                Console.WriteLine("Субъекта с таким названием не найдено");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }
            else
            {
                SelectSubject.SelectedNode = foundSubject;
            }
        }
        public SubjectDistrictList(IEnumerable<Subject> subjects)
        {
            foreach (var subject in subjects)
            {
                Subjects.Add(subject);
            }
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
        }

        public void SwitchDesc()
        {
            Console.Clear();
            SubjectSortDescMenu.Print();
            SubjectSortDescMenu.Action(Console.ReadKey().Key);
        }

        public void PrintAllSubjects()
        {
            subjectTable.Print(OrderedSubjects, SelectedSubject);
        }
        #endregion
    }
}
