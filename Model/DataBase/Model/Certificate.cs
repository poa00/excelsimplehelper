using global::Model.Data;
using global::Model.Data.SpecificationDataDocument;
using global::Model.DataBase.Context;
using global::Model.Write.Word.Document;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;

namespace Model.DataBase.Model
{
    [Table("Certificate")]
    public partial class Certificate
    {
        public int id { get; set; }

        [Required]
        public string startEducation { get; set; }

        [Required]
        public string endEducation { get; set; }

        [Required]
        public string issueDate { get; set; }

        [Required]
        public string party { get; set; }

        [Required]
        public string mark { get; set; }

        public int idStudent { get; set; }

        public int idProgramm { get; set; }

        public virtual Programs Programs { get; set; }

        public virtual Students Students { get; set; }

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
        private void loadCertificate(Certificate certification)
        {
            StudentRecord[] DateFromFile = new StudentRecord[1];
            DateFromFile[0] = new StudentRecord();
            DateFromFile[0].AddPropertyRecord("Фамилия", certification.Students.surname);
            DateFromFile[0].AddPropertyRecord("Имя", certification.Students.name);
            DateFromFile[0].AddPropertyRecord("Отчество", certification.Students.patronymic);
            DateFromFile[0].AddPropertyRecord("ДатаРождения", certification.Students.dateDirth);
            DateFromFile[0].AddPropertyRecord("Оценка", certification.mark);
            DateFromFile[0].AddPropertyRecord("Номер", certification.party);
            EvidenceAndUdostovereniye dataSpec = new EvidenceAndUdostovereniye(DateFromFile, certification.startEducation, certification.endEducation,
                                                                certification.issueDate, certification.party, certification.Programs);
            dataSpec.CorrectionLoad();
            Document_ Evidence = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.PathFileWordEvidenceTemplate, certification.party);
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
                loadCertificate(certification);
            }
            if (certification.Programs.TypeDocument.Id == 1 || certification.Programs.TypeDocument.Id == 2)
            {
                loadCertificate(certification);
            }
        }

        /// <summary>
        /// Сохраняем сертификат в базу данных,
        /// также сохраняет студента если его нет в базе
        /// </summary>
        /// <param name="DataForDocuments">данные для сохранения</param>
        public void SaveCertificate(StudentRecord DataForDocuments)
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
    }
}
