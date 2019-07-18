TRUNCATE TABLE  dbo.Certificate;

CREATE TABLE [dbo].[ProgramDGs] (
    [id]                 INT             NOT NULL,
    [name]               NVARCHAR (1200) NOT NULL,
    [dateNumberApproved] NVARCHAR (100)  NOT NULL,
    [typeId]             INT             NOT NULL,
    CONSTRAINT [PK_dbo.ProgramDGs] PRIMARY KEY CLUSTERED ([name] ASC),
    FOREIGN KEY ([typeId]) REFERENCES [dbo].[TypeDocument] ([Id])
);

INSERT INTO dbo.Lesson(name)
VALUES(N'I. Введение в профессию.' + CHAR(13) +
N'II. Виды СЛО и условия их образования.' + CHAR(13) +
N'III. Концепция чистого ВС.' + CHAR(13) +
N'IV. Авиационные события, связанные с наземным обследованием ВС.' + CHAR(13) +
N'V. Средства противообледенительной обработки ВС.' + CHAR(13) +
N'VI. Противообледенительные жидкости.' + CHAR(13) +
N'VII. Методы противообледенительной обработки ВС.' + CHAR(13) +
N'VIII. Процедуры контроля качества состояния поверхностей ВС.' + CHAR(13) +
N'IX. Обеспечение безопасности и регулярности полетов, профилактика авиационных событий.'
);


/////Certificate
/// <summary>
        /// Проверка на наличие сертификата по его номеру
        /// </summary>
        /// <param name="context"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private bool isSerificate(DataBaseContext context, string group)
        {
            var _certificate = context.Certificate
                    .Where(c => c.party == group);
            if (_certificate.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Инкапсуляция одинаковых строк при функции загрузки сертификата
        /// У них разные типы бланков
        /// </summary>
        /// <param name="certification"></param>
        /// <param name="bookmarkWord"></param>
        private void loadCertificate(Certificate certification, string[] bookmarkWord)
        {
            Record[] DateFromFile = new Record[1];
            DateFromFile[0] = new Record();
            DateFromFile[0].AddPropertyRecord("Фамилия", certification.Students.surname);
            DateFromFile[0].AddPropertyRecord("Имя", certification.Students.name);
            DateFromFile[0].AddPropertyRecord("Отчество", certification.Students.patronymic);
            DateFromFile[0].AddPropertyRecord("ДатаРождения", certification.Students.dateDirth);
            DateFromFile[0].AddPropertyRecord("Оценка", certification.mark);
            DateFromFile[0].AddPropertyRecord("Номер", certification.party);
            DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile, certification.startEducation, certification.endEducation,
                                                                certification.issueDate, certification.party, certification.Programs);
            dataSpec.CorrectionLoad();
            Document_ Evidence = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordEvidenceTemplate, certification.party);
            Evidence.AddBookmarksWord(bookmarkWord);
            Evidence.CreateDocument();
        }

        /// <summary>
        /// Загрузка сертификата из базы данных
        /// </summary>
        /// <param name="certification"></param>
        public void LoadCertificate(Certificate certification)
        {
            if (certification.Programs.TypeDocument.Id == 0)
            {
                string[] bookmarkWord = new string[19] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "ПовышенияКвалификации", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                loadCertificate(certification, bookmarkWord);
            }
            if (certification.Programs.TypeDocument.Id == 1 || certification.Programs.TypeDocument.Id == 2)
            {
                string[] bookmarkWord = new string[18] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                loadCertificate(certification, bookmarkWord);
            }
        }

        /// <summary>
        /// Сохраняем сертификат в базу данных,
        /// также сохраняет студента если его нет в базе
        /// </summary>
        /// <param name="DataForDocuments">данные для сохранения</param>
        public void SaveCertificate(Record DataForDocuments)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Certificate certifications = new Certificate();
                    certifications.endEducation = DataForDocuments.GetOneStudent()["КД"] + "." + DataForDocuments.GetOneStudent()["КМ"] + "." + DataForDocuments.GetOneStudent()["КГ"];
                    certifications.startEducation = DataForDocuments.GetOneStudent()["НД"] + "." + DataForDocuments.GetOneStudent()["НМ"] + "." + DataForDocuments.GetOneStudent()["НГ"];
                    certifications.issueDate = DataForDocuments.GetOneStudent()["ПД"] + "." + DataForDocuments.GetOneStudent()["ПМ"] + "." + DataForDocuments.GetOneStudent()["ПГ"];
                    certifications.party = DataForDocuments.GetOneStudent()["Номер"];
                    certifications.mark = DataForDocuments.GetOneStudent()["Оценка"];

                    string name = DataForDocuments.GetOneStudent()["Имя"].Trim();
                    string surname = DataForDocuments.GetOneStudent()["Фамилия".Trim()];
                    string patronymic = DataForDocuments.GetOneStudent()["Отчество"].Trim();
                    string dateBirth = DataForDocuments.GetOneStudent()["ДатаРождения"].Trim();

                    Students student = new Students();
                    certifications.idStudent = student.SaveStudent(context, name, surname, patronymic, dateBirth);

                    string nameProgramm = DataForDocuments.GetOneStudent()["Программа"];
                    var programs1 = context.Programs
                        .Where(c => c.name == nameProgramm);
                    certifications.idProgramm = programs1.First().id;

                    var certificat = context.Certificate.Where(c => c.party == certifications.party);
                    if (certificat.Count() < 1)
                    {
                        context.Certificate.Add(certifications);
                    }

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.EntityValidationErrors);
            }
            
        }

        /// <summary>
        /// Поиск сертификата по номеру
        /// </summary>
        /// <param name="context">доступ к базе данных</param>
        /// <param name="group">номер</param>
        /// <returns></returns>
        public int FindIdCertificateByNumber(DataBaseContext context, string number)
        {
            int idCertificate = 0;
            if (isSerificate(context, number))
            {
                idCertificate = context.Certificate
                        .Where(c => c.party == number).First().id;
            }
            return idCertificate;
        }


        ////////////////////////CertificateDG
        /// <summary>
        /// Проверка на наличие сертификата по его номеру
        /// </summary>
        /// <param name="context"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private bool isSerificateDG(DataBaseContext context, string group)
        {
            var _certificate = context.CertificateDGs
                    .Where(c => c.party == group);
            if (_certificate.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public void LoadCertification(CertificateDGs certification)
        {
            string[] bookmarkWord = new string[7] { "КогдаКемУтверждена", "ДатаВыдачи", "Имя", "НазваниеПрограммы", "Номер", "Отчество", "Фамилия" };
            Record[] DateFromFile = new Record[1];
            DateFromFile[0] = new Record();
            DateFromFile[0].AddPropertyRecord("Фамилия", certification.Students.surname);
            DateFromFile[0].AddPropertyRecord("Имя", certification.Students.name);
            DateFromFile[0].AddPropertyRecord("Отчество", certification.Students.patronymic);
            DateFromFile[0].AddPropertyRecord("ДатаРождения", certification.Students.dateDirth);
            DateFromFile[0].AddPropertyRecord("Номер", certification.party);
            CertificateDangerousGoodsSpec dataSpec = new CertificateDangerousGoodsSpec(DateFromFile, certification.issueDate, certification.party, certification.ProgramDGs);
            dataSpec.CorrectionLoad();
            Document_ document = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordCertificateDGTemplate, certification.party);
            document.AddBookmarksWord(bookmarkWord);
            document.CreateDocument();
        }

        /// <summary>
        /// Сохраняем сертификат в базу данных,
        /// также сохраняет студента если его нет в базе
        /// </summary>
        /// <param name="DataForDocuments">данные для сохранения</param>
        public void SaveSertification(Record DataForDocuments)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                CertificateDGs certifications = new CertificateDGs();
                certifications.issueDate = DataForDocuments.GetOneStudent()["ДатаВыдачи"];
                certifications.party = DataForDocuments.GetOneStudent()["Номер"];
                string name = DataForDocuments.GetOneStudent()["Имя"].Trim();
                string surname = DataForDocuments.GetOneStudent()["Фамилия".Trim()];
                string patronymic = DataForDocuments.GetOneStudent()["Отчество"].Trim();
                string dateBirth = DataForDocuments.GetOneStudent()["ДатаРождения"].Trim();

                Students student = new Students();
                certifications.idStudent = student.SaveStudent(context, name, surname, patronymic, dateBirth);


                string nameProgramm = DataForDocuments.GetOneStudent()["НазваниеПрограммы"].Trim();
                var programs1 = context.ProgramDGs
                    .Where(c => c.name == nameProgramm);
                certifications.idProgramDG = programs1.First().id;

                var certificat = context.CertificateDGs.Where(c => c.party == certifications.party);
                if (certificat.Count() < 1)
                {
                    context.CertificateDGs.Add(certifications);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Поиск сертификата по номеру
        /// </summary>
        /// <param name="context">доступ к базе данных</param>
        /// <param name="group">номер</param>
        /// <returns></returns>
        public int FindIdCertificateByNumber(DataBaseContext context, string number)
        {
            int idCertificateDG = 0;
            if (isSerificateDG(context, number))
            {
                idCertificateDG = context.CertificateDGs
                        .Where(c => c.party == number).First().id;
            }
            return idCertificateDG;
        }
        
        //////////////////////////////////////Lesson

        private string lessonCorrect(string lesson)
        {
            string lessonCorrect = " ";
            lessonCorrect = lesson.Replace('.', (char)13);
            return lessonCorrect;
        }

        /// <summary>
        /// Поиск сертификата по номеру
        /// </summary>
        /// <param name="context">доступ к базе данных</param>
        /// <param name="group">номер</param>
        /// <returns></returns>
        public int FindIdLesson(DataBaseContext context, string lesson)
        {
            int idLesson = -1;
            idLesson = context.Lesson
                        .Where(c => c.Name == lesson).First().Id;
            return idLesson;
        }

        /// <summary>
        /// Добавляю уроки и получаю id это записи
        /// </summary>
        /// <param name="lesson">уроки</param>
        /// <returns>id уроков в БД</returns>
        public int AddLesson(DataBaseContext dataBaseContext, string lesson)
        {
            int id = -1;
            //using (DataBaseContext dataBaseContext = new DataBaseContext())
            //{
                Lesson Lessons = new Lesson();
                Lessons.Name = lessonCorrect(lesson);
                dataBaseContext.Lesson.Add(Lessons);
                dataBaseContext.SaveChanges();
                id = FindIdLesson(dataBaseContext, lesson);
            //}
            return id;
        }

        ///////////////////////////////////////////////////////////////////Programspublic void AddProgramm(Programm012Model programm012Model, string type)
        {
            if (programm012Model == null)
            {
                MessageBug.AddMessage("Модель не заполнена");
                return;
            }
            if (programm012Model.Name == null)
            {
                MessageBug.AddMessage("Название программы не заполнено");
                return;
            }
            if (programm012Model.Lesson == null)
            {
                MessageBug.AddMessage("Уроки не заполнены");
                return;
            }
            if (programm012Model.Type == null)
            {
                MessageBug.AddMessage("Тип не выбран");
                return;
            }
            if (type.Equals("Свидетельство") && programm012Model.Training == null)
            {
                MessageBug.AddMessage("Повышение квалификации не заполнено");
                return;
            }
            if (programm012Model.Clock == null)
            {
                MessageBug.AddMessage("Часы не заполнены");
                return;
            }

            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Lesson lesson = new Lesson();
                TypeDocument typeDocument = new TypeDocument();
                int idTypeDocument = typeDocument.ConvertNameToId(type);
                int idLesson = lesson.AddLesson(dataBaseContext, programm012Model.Lesson);
                if (idLesson == -1 || idTypeDocument == -1)
                {
                    return;
                }
                Programs programs = new Programs
                {
                    name = programm012Model.Name,
                    training = programm012Model.Training,
                    clock = programm012Model.Clock,
                    lessonId = idLesson,
                    typeId = idTypeDocument
                };
                dataBaseContext.Programs.Add(programs);
                dataBaseContext.SaveChanges();
            }
        }

        ///////////////////////////////////////////////////////////////Students
        /// <summary>
        /// Проверка есть ли данные о студенте в базе данных
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        /// <param name="datebirth">Дата Рождения</param>
        /// <returns></returns>
        public bool IsStudent(DataBaseContext context, string name, string surname, string patronymic, string datebirth)
        {
            var student = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == datebirth);
            if (student.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка есть ли данные о студенте в базе данных
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        /// <returns></returns>
        public bool IsStudent(DataBaseContext context, string name, string surname, string patronymic)
        {
            var student = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic);
            if (student.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка есть ли данные о студенте в базе данных
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <returns></returns>
        public bool IsStudent(DataBaseContext context, string name, string surname)
        {
            var student = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname);
            if (student.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка есть ли данные о студенте в базе данных
        /// </summary>
        /// <param name="context"></param>
        /// <param name="surname">Фамилия</param>
        /// <returns></returns>
        public bool IsStudent(DataBaseContext context, string surname)
        {
            var student = context.Students
                    .Where(c => c.surname == surname);
            if (student.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Сохраняет данные о студенте в базу данных
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="patronymic"></param>
        /// <param name="datebirth"></param>
        /// <returns></returns>
        public int SaveStudent(DataBaseContext context, string name, string surname, string patronymic, string datebirth)
        {
            bool isStudent = IsStudent(context, name, surname, patronymic, datebirth);
            int idStudent;
            if (isStudent)
            {
                idStudent = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == datebirth).First().id;
            }
            else
            {
                Students s = new Students()
                {
                    name = name,
                    surname = surname,
                    patronymic = patronymic,
                    dateDirth = datebirth
                };
                context.Students.Add(s);
                context.SaveChanges();
                idStudent = context.Students
                .Where(c => c.name == name &&
                        c.surname == surname &&
                        c.patronymic == patronymic &&
                        c.dateDirth == datebirth).First().id;
            }
            return idStudent;
        }

        /// <summary>
        /// Поиск сертификата по фамилии имени отчеству отправляемого одной строкой
        /// порядок важен, они делятся пробелом.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fio"></param>
        /// <returns></returns>
        public int FindIdStudentByFio(DataBaseContext context, string fio)
        {
            SpecFunction specFunction = new SpecFunction();
            string[] fioArray = specFunction.CutFromStringElements(fio, ' ');
            Students students = new Students();
            int idStudent = 0; ;

            if (fioArray.Length == 0)
            {
                return idStudent;
            }
            if (fioArray.Length == 1)
            {
                if (students.IsStudent(context, fioArray[0]))
                {
                    string surname = fioArray[0];
                    idStudent = context.Students
                        .Where(c => c.surname == surname).First().id;
                }
            }
            else
            {
                if (fioArray.Length == 2)
                {
                    if (students.IsStudent(context, fioArray[0], fioArray[1]))
                    {
                        string surname = fioArray[0];
                        string name = fioArray[1];
                        idStudent = context.Students
                            .Where(c => c.surname == surname &&
                                        c.name == name).First().id;
                    }
                }
                else
                {
                    if (fioArray.Length == 3)
                    {
                        if (students.IsStudent(context, fioArray[0], fioArray[1], fioArray[2]))
                        {
                            string surname = fioArray[0];
                            string name = fioArray[1];
                            string patronymic = fioArray[2];
                            idStudent = context.Students
                                .Where(c => c.surname == surname &&
                                            c.name == name &&
                                            c.patronymic == patronymic).First().id;
                        }
                    }
                }
            }

            return idStudent;
        }

        /// <summary>
        /// Поиск сертификата по фамилии имени отчеству отправляемого одной строкой
        /// порядок важен, они делятся пробелом.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fio"></param>
        /// <param name="dateBirth"></param>
        /// <returns></returns>
        public int FindIdStudentByFioAndDateBirth(DataBaseContext context, string fio, string dateBirth)
        {
            SpecFunction specFunction = new SpecFunction();
            string[] fioArray = specFunction.CutFromStringElements(fio, ' ');
            Students students = new Students();
            int idStudent = 0;

            if (fioArray.Length == 0)
            {
                return idStudent;
            }

            if (fioArray.Length == 3 && dateBirth.Length == 10)
            {
                string surname = fioArray[0];
                string name = fioArray[1];
                string patronymic = fioArray[2];
                if (students.IsStudent(context, fioArray[0], fioArray[1], fioArray[2], dateBirth))
                {
                    idStudent = context.Students
                        .Where(c => c.surname == surname &&
                                    c.name == name &&
                                    c.patronymic == patronymic &&
                                    c.dateDirth == dateBirth).First().id;
                }
            }

            return idStudent;
        }

        //////////////////////////////////////TypeDocument
        public int ConvertNameToId(string type)
        {
            int idType = -1;
            if (type.Equals("Свидетельство"))
            {
                idType = 0;
            }
            if (type.Equals("Удостоверения (Лицензия)"))
            {
                idType = 1;
            }
            if (type.Equals("Удостоверение (Реквизит)"))
            {
                idType = 2;
            }
            if (type.Equals("Сертификат ОГ"))
            {
                idType = 3;
            }
            return idType;
        }
