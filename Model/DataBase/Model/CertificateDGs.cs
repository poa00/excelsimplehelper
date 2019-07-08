namespace Model.DataBase.Model
{
    using global::Model.Data;
    using global::Model.Data.SpecificationDataDocument;
    using global::Model.DataBase.Context;
    using global::Model.Write.Word.Document;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class CertificateDGs
    {
        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string issueDate { get; set; }

        [Required]
        [StringLength(10)]
        public string party { get; set; }

        public int idProgramDG { get; set; }

        public int idStudent { get; set; }

        public virtual ProgramDGs ProgramDGs { get; set; }

        public virtual Students Students { get; set; }

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
    }
}
