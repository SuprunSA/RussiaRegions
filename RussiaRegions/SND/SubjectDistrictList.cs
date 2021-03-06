using System;
using System.Collections.Generic;
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
                        subject.District.Name.ToLower().Contains(SubjectSearchBy)
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

            new TableColumn<Subject>("Площадь", 20, subject => string.Format("{0:# ##0.00} кв. км.", subject.Square)),

            new TableColumn<Subject>("Плотность населения", 28, subject => string.Format("{0:# ##0.000} тыс. чел. / кв. км.", subject.PopulationDencity)),

            new TableColumn<Subject>("Код округа", 12, subject => subject.District.Code.ToString())
        });

        public List<District> Districts = new List<District>();

        public Menu DistrictMenu;
        readonly Menu DistrictDescMenu;
        readonly Menu DistrictChangeMenu;
        readonly Menu DistrictSortMenu;
        public ListSelector<District> SelectDistrict { get; }
        public District SelectedDistrict => SelectDistrict.SelectedNode;
        Func<District, object> DistrictOrderBy = district => district.Population;
        public bool DistrictOrderByDescending = false;
        IList<District> OrderedDistricts
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
        Table<District> districtTable = new Table<District>(new[]
                {
                    new TableColumn<District>("Название", 30, district => string.Format("{0} федеральный округ", district.Name)),

                    new TableColumn<District>("Код ОКЭР", 10, district => district.Code.ToString()),

                    new TableColumn<District>("Население", 25, district => string.Format("{0:# ##0.000} тыс.чел.", district.Population)),

                    new TableColumn<District>("Площадь", 20, district => string.Format("{0:# ##0.000} тыс.чел.", district.Square)),

                    new TableColumn<District>("Плотность населения", 28, district => string.Format("{0:# ##0.000} тыс. чел. / кв. км.", district.PopulationDencity.ToString()))
                });

        public SubjectDistrictList(List<Subject> subjects, List<District> districts)
        {
            InputControl inputControl = new InputControl();

            Subjects = subjects;
            SelectSubject = new ListSelector<Subject>(() => OrderedSubjects);
            SubjectMenu = new Menu(new List<MenuItem>(SelectSubject.Menu.Items) {
                new MenuAction(ConsoleKey.F1, "Новый субъект", () => AddSubject(inputControl)),

                new MenuAction(ConsoleKey.F2, "Удаление субъекта", () => RemoveSubject(SelectedSubject)),

                new MenuAction(ConsoleKey.F3, "Изменение субъекта", ChangeSubject),

                new MenuAction(ConsoleKey.F4, "Сортировать", ChooseSubjectSort),

                new MenuAction(ConsoleKey.F5, "Фильтр по федеральным округам", DistrictFilter),

                new MenuAction(ConsoleKey.F6, "Поиск по названию", SearchSubjectByName),

                new MenuAction(ConsoleKey.F8, "Сохранить", () => SaveToFile(inputControl)),

                new MenuAction(ConsoleKey.F9, "Загрузить", () => LoadFromFile(inputControl))
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

            Districts = districts;
            SelectDistrict = new ListSelector<District>(() => OrderedDistricts);
            DistrictMenu = new Menu(new List<MenuItem>(SelectDistrict.Menu.Items)
            {
                new MenuAction(ConsoleKey.F1, "Добавление округа", () => AddDistrict(inputControl)),

                new MenuAction(ConsoleKey.F2, "Удаление округа", () => RemoveDistrict(SelectedDistrict)),

                new MenuAction(ConsoleKey.F3, "Изменение округа", ChangeDistrict),

                new MenuAction(ConsoleKey.F4, "Сортировать", ChooseDistrictSort),

                new MenuAction(ConsoleKey.F5, "Поиск по названию", () => SearchDistrictByName(inputControl)),

                new MenuAction(ConsoleKey.F6, "Поиск по коду", () => SearchDistrictByCode(inputControl)),

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
            DistrictSortMenu = new Menu(new List<MenuItem>(SelectDistrict.Menu.Items)
            {
                new MenuAction(ConsoleKey.D1, "Сортировка по численности наcеления",
                    () => DistrictOrderBy = district => district.Population),

                new MenuAction(ConsoleKey.D2, "Сортировка по площади",
                    () => DistrictOrderBy = district => district.Square),

                new MenuAction(ConsoleKey.D3, "Сортировка по плотности населения",
                    () => DistrictOrderBy = district => district.PopulationDencity),

                new MenuClose(ConsoleKey.Escape, "Выход")
            });
        }

        #region Districts
        void ChooseDistrictSort()
        {
            Console.Clear();
            DistrictSortMenu.Print();
            PrintAllDistricts();
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Escape)
            {
                return;
            }
            DistrictSortMenu.Action(key);
            SwitchDistrictDesc();
        }

        void AddDistrict(InputControl inputControl)
        {
            Console.Clear();
            inputControl.PrintDistrictList(Districts);
            var code = inputControl.ReadFederalDistrictCodeToSth();
            if (Districts.Where(d => d.Code == code).Count() == 1)
            {
                Console.WriteLine("Округ с таким кодом уже существует!");
                inputControl.Wait();
                return;
            }
            var name = inputControl.ReadFederalDistrictNameToSTH();
            Districts.Add(new District(code, name));
        }

        void SearchDistrictByCode(InputControl inputControl)
        {
            Console.Clear();
            var code = inputControl.ReadFederalDistrictCodeToSth();
            var district = Districts.Find(d => d.Code == code);
            if (district == null)
            {
                Console.WriteLine("Округа с таким кодом не найдено");
                inputControl.Wait();
            }
            else
            {
                SelectDistrict.SelectedNode = district;
            }
        }

        void SearchDistrictByName(InputControl inputControl)
        {
            Console.Clear();
            var name = inputControl.ReadFederalDistrictNameToSTH();
            var district = Districts.Find(d => d.Name == name);
            if (district == null)
            {
                Console.WriteLine("Округа с таким названием не найдено");
                inputControl.Wait();
            }
            else
            {
                SelectDistrict.SelectedNode = district;
            }
        }

        void ChangeDistrict()
        {
            Console.Clear();
            DistrictChangeMenu.Print();
            districtTable.Print(OrderedDistricts, SelectedDistrict);
            DistrictChangeMenu.Action(Console.ReadKey().Key);
        }

        void ChangeDistrictName(District federalDistrict, InputControl inputControl)
        {
            Console.Clear();
            var name = inputControl.ReadFederalDistrictNameToSTH();
            foreach (var subject in Subjects)
            {
                if (subject.District.Code == federalDistrict.Code)
                {
                    subject.District.Name = name;
                }
            }
            Districts.Find(d => d.Code == federalDistrict.Code).Name = name;
        }

        void RemoveDistrict(District federalDistrict)
        {
            Districts.Remove(federalDistrict);
            SelectDistrict.SelectedNodeIndex--;
            Subjects.RemoveAll(s => s.District.Code == federalDistrict.Code);
        }

        void CountPopulationDensity()
        {
            foreach (var district in Districts)
            {
                var population = 0.0;
                var square = 0.0;
                foreach (var subject in Subjects)
                {
                    if (subject.District.Code == district.Code)
                    {
                        population += subject.Population;
                        square += subject.Square;
                    }
                }
                if (population == 0.0)
                {
                    district.Population = 0.0;
                    district.Square = 0.0;
                    district.PopulationDencity = 0.0;
                    continue;
                }
                district.Population = population;
                district.Square = square;
                district.PopulationDencity = Math.Round(population / square, 3);
            }
        }

        public void PrintAllDistricts()
        {
            CountPopulationDensity();
            districtTable.Print(OrderedDistricts, SelectedDistrict);
        }

        public void SwitchDistrictDesc()
        {
            Console.Clear();
            DistrictDescMenu.Print();
            PrintAllDistricts();
            DistrictDescMenu.Action(Console.ReadKey().Key);
        }
        #endregion

        #region Subjects
        void ChangeDistrict(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            inputControl.PrintDistrictList(Districts);
            var code = inputControl.ReadFederalDistrictCodeToSth();
            var districtToDelete = subject.District;
            if (Districts.Where(d => d.Code == code).Count() == 1)
            {
                Subjects.Find(s => s == subject).District = Districts.Find(d => d.Code == code);
            }
            else
            {
                var district = new District(code, inputControl.ReadFederalDistrictNameToSTH());
                Subjects.Find(s => s == subject).District = district;
                Districts.Add(district);
            }
        }

        void ChangeSubjectName(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.Name = inputControl.ReadSubjectName();
        }

        void ChangeAdminCenter(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.AdminCenterName = inputControl.ReadSubjectAdminCenter();
        }

        void ChangePopulation(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.Population = inputControl.ReadSubjectPopulation();
        }

        void ChangeSquare(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            subject.Square = inputControl.ReadSubjectSquare();
        }

        void AddSubject(InputControl inputControl)
        {
            Console.Clear();
            AddSubject(new Subject(
                inputControl.ReadSubjectCode(),
                inputControl.ReadSubjectName(),
                inputControl.ReadFederalDistrictCode(Districts))
            {
                AdminCenterName = inputControl.ReadSubjectAdminCenter(),
                Population = inputControl.ReadSubjectPopulation(),
                Square = inputControl.ReadSubjectSquare()
            });
        }

        void RemoveSubject(Subject subject)
        {
            Console.Clear();
            Subjects.Remove(subject);
            SelectSubject.SelectedNodeIndex--;
        }

        void ChangeSubject()
        {
            Console.Clear();
            SubjectChangeMenu.Print();
            PrintAllSubjects();
            SubjectChangeMenu.Action(Console.ReadKey().Key);
        }

        void ChooseSubjectSort()
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
            SwitchSubjectDesc();
        }

        void DistrictFilter()
        {
            Console.Clear();
            Console.WriteLine("Введите федеральный округ, по которому сортировать субъекты, или пустое значение, чтобы вывести все субъекты: ");
            SubjectSearchBy = Console.ReadLine().Trim();
        }

        void SearchSubjectByName()
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

        void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
        }

        void SwitchSubjectDesc()
        {
            Console.Clear();
            SubjectSortDescMenu.Print();
            PrintAllSubjects();
            SubjectSortDescMenu.Action(Console.ReadKey().Key);
        }

        public void PrintAllSubjects()
        {
            subjectTable.Print(OrderedSubjects, SelectedSubject);
        }
        #endregion

        #region Files
        public void SaveToFile(InputControl inputControl)
        {
            var listDTO = new ListsDTO(Subjects, Districts);

            FileSelector.SaveToFile("Список", listDTO);

            inputControl.Wait();

        }

        public void LoadFromFile(InputControl inputControl)
        {
            var loadedList = FileSelector.LoadFromFile("Список");

            Console.WriteLine("Чтение данных");
            Districts = loadedList.Map(loadedList.Districts).ToList();
            Subjects = loadedList.Map(loadedList.Subjects, Districts).ToList();
            Console.WriteLine("Загрузка прошла успешно.");

            inputControl.Wait();

        }
        #endregion
    }
}