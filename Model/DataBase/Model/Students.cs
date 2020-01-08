namespace Model.DataBase.Model
{
    using global::Model.Data.SpecificationDataDocument;
    using global::Model.DataBase.Context;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class Students
    {
        private SpecFunction SpecFunction_;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Students()
        {
            Certificate = new HashSet<Certificate>();
            CertificateDGs = new HashSet<CertificateDGs>();

            SpecFunction_ = new SpecFunction();
        }

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        public string surname { get; set; }

        [Required]
        [StringLength(20)]
        public string patronymic { get; set; }

        [Required]
        [StringLength(10)]
        public string dateDirth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Certificate> Certificate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertificateDGs> CertificateDGs { get; set; }

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
            string[] fioArray = SpecFunction_.CutFromStringElements(fio, ' ');
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
            string[] fioArray = SpecFunction_.CutFromStringElements(fio, ' ');
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

        
    }
}
