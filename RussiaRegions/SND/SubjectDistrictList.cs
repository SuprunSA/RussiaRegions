using System;
using System.Collections.Generic;
using DistrictsNSubjects;
using System.Linq;

namespace RussiaRegions
{
    class SubjectDistrictList
    {
        public List<Subject> Subjects = new List<Subject>();
        public IEnumerable<Subject> SubjectList => Subjects;
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

            new TableColumn<Subject>("Площадь", 20, subject => string.Format("{0:# ##0.00} кв. км.", subject.Square)),

            new TableColumn<Subject>("Плотность населения", 28, subject => string.Format("{0:# ##0.000} тыс. чел. / кв. км.", subject.PopulationDencity)),

            new TableColumn<Subject>("Название округа", 17, subject => subject.FederalDistrict.Name),

            new TableColumn<Subject>("Код округа", 12, subject => subject.FederalDistrict.Code.ToString())
        });

        public List<District> Districts = new List<District>();
        public IEnumerable<District> DistrictList => Districts;
        public Menu DistrictMenu;
        readonly Menu DistrictDescMenu;
        readonly Menu DistrictChangeMenu;
        public ListSelector<District> SelectDistrict { get; }
        public District SelectedDistrict => SelectDistrict.SelectedNode;
        Func<District, object> DistrictOrderBy = district => district.PopulationDencity;
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

                    new TableColumn<District>("Плотность населения", 28, district => string.Format("{0:# ##0.000} тыс. чел. / кв. км.", district.PopulationDencity.ToString()))
                });

        public SubjectDistrictList(List<Subject> subjects, List<District> federalDistricts)
        {
            InputControl inputControl = new InputControl();

            subjects = Subjects;
            SelectSubject = new ListSelector<Subject>(() => OrderedSubjects);
            SubjectMenu = new Menu(new List<MenuItem>(SelectSubject.Menu.Items) {
                new MenuAction(ConsoleKey.F1, "Новый субъект", () => AddSubject(inputControl)),

                new MenuAction(ConsoleKey.F2, "Удаление субъекта", () => RemoveSubject(SelectedSubject)),

                new MenuAction(ConsoleKey.F3, "Изменение субъекта", ChangeSubject),

                new MenuAction(ConsoleKey.F4, "Сортировать", ChooseSort),

                new MenuAction(ConsoleKey.F5, "Фильтр по федеральным округам", DistrictFilter),

                new MenuAction(ConsoleKey.F6, "Поиск по названию", SearchSubjectByName),
/*
                new MenuAction(ConsoleKey.F8, "Сохранить", () => SaveSubjectsToFile("список субъектов", inputControl)),

                new MenuAction(ConsoleKey.F9, "Загрузить", () => LoadSubjecetsFromFile("список субъектов", inputControl))*/
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
            SelectDistrict = new ListSelector<District>(() => OrderedDistricts);
            DistrictMenu = new Menu(new List<MenuItem>(SelectDistrict.Menu.Items)
            {
                new MenuAction(ConsoleKey.F1, "Добавление округа", () => AddDistrict(inputControl)),

                new MenuAction(ConsoleKey.F2, "Удаление округа", () => RemoveDistrict(SelectedDistrict)),

                new MenuAction(ConsoleKey.F3, "Изменение округа", ChangeDistrict),

                new MenuAction(ConsoleKey.F4, "Сортировать по плотности населения", PopulationDencitySort),

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
        }

        #region Districts
        void LoadDistricts(IEnumerable<District> districts)
        {
            Districts.Clear();
            foreach(var district in districts)
            {
                AddDistrict(district);
            }
        }

        void AddDistrict(District federalDistrict)
        {
            Districts.Add(federalDistrict);
        }

        void AddDistrict(InputControl inputControl)
        {
            Console.Clear();
            inputControl.PrintDistrictList(Districts);
            var code = inputControl.ReadFederalDistrictCode();
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
            var code = inputControl.ReadFederalDistrictCode();
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

        void PopulationDencitySort()
        {
            Console.Clear();
            DistrictDescMenu.Print();
            DistrictDescMenu.Action(Console.ReadKey().Key);
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
                if (subject.FederalDistrict.Code == federalDistrict.Code)
                {
                    subject.FederalDistrict.Name = name;
                }
            }
            Districts.Find(d => d.Code == federalDistrict.Code).Name = name;
        }

        void RemoveDistrict(District federalDistrict)
        {
            Districts.Remove(federalDistrict);
            Subjects.RemoveAll(s => s.FederalDistrict.Code == federalDistrict.Code);
            SelectDistrict.SelectedNodeIndex--;
        }

        void CountPopulationDensity()
        {
            foreach (var district in Districts)
            {
                var population = 0.0;
                var square = 0.0;
                foreach (var subject in Subjects)
                {
                    if (subject.FederalDistrict.Code == district.Code)
                    {
                        population += subject.Population;
                        square += subject.Square;
                    }
                }
                if (population == 0.0)
                {
                    district.PopulationDencity = 0;
                    continue;
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
        /*void SaveSubjectsToFile(string name, InputControl inputControl)
        {
            try
            {
                Console.WriteLine("Сохранение в файл");
                FileSelector.SaveToFile(name, SubjectList.Select(s => SubjectDTO.Map(s)).ToArray());
                Console.WriteLine("Сохранение прошло успешно.");
            }
            catch (Exception e)
            {
                Console.WriteLine("При сохранении в файл произошла ошибка: {0}", e.ToString());
            }
            finally
            {
                inputControl.Wait();
            }
        }

        void LoadSubjecetsFromFile(string name, InputControl inputControl)
        {
            try
            {
                var loadedData = FileSelector.LoadFromFile<SubjectDTO>(name);
                if (loadedData != null)
                {
                    Console.WriteLine("Чтение данных");
                    LoadSubjects(loadedData.Select(s => SubjectDTO.Map(s, Districts)));
                    Console.WriteLine("Загрузка прошла успешно.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка, файл содержит некорректные данные: ", e.Message);
            }
            finally
            {
                inputControl.Wait();
            }
        }*/

        void LoadSubjects(IEnumerable<Subject> subjects)
        {
            Subjects.Clear();
            foreach (var subject in subjects)
            {
                AddSubject(subject);
            }
        }

        void ChangeDistrict(Subject subject, InputControl inputControl)
        {
            Console.Clear();
            inputControl.PrintDistrictList(Districts);
            var code = inputControl.ReadFederalDistrictCode();
            var districtToDelete = subject.FederalDistrict;
            if (Districts.Where(d => d.Code == code).Count() == 1)
            {
                Subjects.Find(s => s == subject).FederalDistrict = Districts.Find(d => d.Code == code);
            }
            else
            {
                var district = new District(code, inputControl.ReadFederalDistrictNameToSTH());
                Subjects.Find(s => s == subject).FederalDistrict = district;
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
                inputControl.ReadFederalDistrictName(Districts))
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

        void ChooseSort()
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

        void SwitchDesc()
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

        public void SaveToFile(InputControl inputControl)
        {
            try
            {

            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка при сохранении в файл: ", e.Message);
            }
            finally
            {
                inputControl.Wait();
            }
        }
    }
}
