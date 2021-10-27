using System;
using System.Collections.Generic;
using System.Text;
using DistrictsNSubjects;
using System.Linq;

namespace RussiaRegions
{
    class SubjectDistrictList
    {
        public SubjectDistrictList(IEnumerable<Subject> subjects, IEnumerable<FederalDistrict> federalDistricts)
        {
            Subjects = subjects.ToList();
            Districts = federalDistricts.ToList();
        }
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

            new TableColumn<Subject>("Население", 20, subject => string.Format("{0:# ##0.000} тыс.чел.", subject.Population)),

            new TableColumn<Subject>("Площадь", 20, subject => string.Format("{0:# ##0.00} кв. м.", subject.Square)),

            new TableColumn<Subject>("Плотность населения", 23, subject => string.Format("{0:# ##0.000} тыс. чел. / кв. м.", subject.PopulationDencity)),

            new TableColumn<Subject>("Федеральный округ", 15, subject => subject.FederalDistrict.Name)
        });

        #region Subjects
        public SubjectDistrictList(List<Subject> subjects)
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
                    ChangeDistrict(SelectedSubject)),

                new MenuClose(ConsoleKey.Escape, "Выход")
            });
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

        public void ChangeDistrict(Subject subject)
        {
            Console.Clear();
            Console.WriteLine("Введите название округа");
            var name = Console.ReadLine().Trim();
            if (Subjects.Where(s => s.FederalDistrict.Name == subject.FederalDistrict.Name).Count() == 1)
            {
                RemoveDistrict(Districts.Find(d => d.Name == subject.FederalDistrict.Name));
            }
            if (Districts.Find(d => d.Name == name) != null)
            {
                subject.FederalDistrict = Districts.Find(d => d.Name == name);
            }
            else
            {
                uint code;
                Console.WriteLine("Введите код округа: ");
                while (!uint.TryParse(Console.ReadLine(), out code))
                {
                    Console.Error.WriteLine("Код округа - положительное целое число.");
                    Console.WriteLine("Введите код округа: ");
                }
                subject.FederalDistrict = new FederalDistrict(code, name);
                AddDistrict(subject.FederalDistrict);
            }
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
            var subjects = Subjects.Where(s => s.FederalDistrict.Name == subject.FederalDistrict.Name);
            if (subjects.Count() == 1)
            {
                Districts.RemoveAll(d => d.Name == subject.FederalDistrict.Name);
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

        #region Districts
        public SubjectDistrictList(List<FederalDistrict> federalDistricts)
        {
            federalDistricts = Districts;
            InputControl inputControl = new InputControl();
            SelectDistrict = new ListSelector<FederalDistrict>(() => OrderedDistricts);
            DistrictMenu = new Menu(new List<MenuItem>(SelectDistrict.Menu.Items) {
                new MenuAction(ConsoleKey.F1, "Изменение округа", ChangeDistrict),

                new MenuAction(ConsoleKey.F2, "Удаление округа", () => RemoveDistrict(SelectedDistrict)),

                new MenuAction(ConsoleKey.F3, "Сортировать по плотности населения", PopulationDencitySort),

                new MenuAction(ConsoleKey.F4, "Поиск по названию", SearchDistrictByName),

                new MenuClose(ConsoleKey.Tab, "Вернуться к субъектам")
            });
            DistrictDescMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "По возрастанию", () => DistrictOrderByDescending = false),
                new MenuAction(ConsoleKey.D2, "По убыванию", () => DistrictOrderByDescending = true)
            });
            DistrictChangeMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "Изменить название", () => ChangeDistrictName(SelectedDistrict)),

                new MenuClose(ConsoleKey.Escape, "Выход")
            });
        }

        public void ChangeDistrictName(FederalDistrict federalDistrict)
        {
            Console.Clear();
            Console.WriteLine("Введите название округа: ");
            var name = Console.ReadLine().Trim();
            foreach (var subject in Subjects)
            {
                if (subject.FederalDistrict.Name == federalDistrict.Name)
                {
                    subject.FederalDistrict.Name = name;
                }
            }
            Districts.Find(d => d.Name == federalDistrict.Name).Name = name;
        }

        public void RemoveDistrict(FederalDistrict federalDistrict)
        {
            Districts.Remove(federalDistrict);
            Subjects.RemoveAll(s => s.FederalDistrict.Name == federalDistrict.Name);
            SelectDistrict.SelectedNodeIndex--;
        }

        public void ChangeDistrict()
        {
            Console.Clear();
            DistrictChangeMenu.Print();
            PrintAllDistricts();
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Escape)
            {
                return;
            }
            DistrictChangeMenu.Action(key);
        }

        public void PopulationDencitySort()
        {
            Console.Clear();
            DistrictDescMenu.Print();
            DistrictDescMenu.Action(Console.ReadKey().Key);
        }

        public void SearchDistrictByName()
        {
            Console.Clear();
            Console.WriteLine("Введите название округа: ");
            var name = Console.ReadLine().Trim();
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

        public void CountDistrictPopulationDensity()
        {
            foreach (var district in Districts)
            {
                var population = 0.0;
                var square = 0.0;
                foreach (var subject in Subjects)
                {
                    if (subject.FederalDistrict.Name == district.Name)
                    {
                        population += subject.Population;
                        square += subject.Square;
                    }
                }
                district.PopulationDencity = Math.Round(population / square, 3);
            }
        }

        
        public void AddDistrict(FederalDistrict federalDistrict)
        {
            Districts.Add(federalDistrict);
        }

        public void RemoveDistrict(string districtName, List<Subject> subjects)
        {
            subjects.RemoveAll(s => s.FederalDistrict.Name == districtName);
            RemoveDistrict(Districts.Find(d => d.Name == districtName));
        }

        public void PrintAllDistricts()
        {
            CountDistrictPopulationDensity();
            districtTable.Print(OrderedDistricts, SelectedDistrict);
        }
        #endregion
    }
}
