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
            string[] bookmarkWord = new string[7] { "������������������", "����������", "���", "�����������������", "�����", "��������", "�������" };
            Record[] DateFromFile = new Record[1];
            DateFromFile[0] = new Record();
            DateFromFile[0].AddPropertyRecord("�������", certification.Students.surname);
            DateFromFile[0].AddPropertyRecord("���", certification.Students.name);
            DateFromFile[0].AddPropertyRecord("��������", certification.Students.patronymic);
            DateFromFile[0].AddPropertyRecord("������������", certification.Students.dateDirth);
            DateFromFile[0].AddPropertyRecord("�����", certification.party);
            CertificateDangerousGoodsSpec dataSpec = new CertificateDangerousGoodsSpec(DateFromFile, certification.issueDate, certification.party, certification.ProgramDGs);
            dataSpec.CorrectionLoad();
            Document_ document = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordCertificateDGTemplate, certification.party);
            document.AddBookmarksWord(bookmarkWord);
            document.CreateDocument();
        }

        /// <summary>
        /// ��������� ���������� � ���� ������,
        /// ����� ��������� �������� ���� ��� ��� � ����
        /// </summary>
        /// <param name="DataForDocuments">������ ��� ����������</param>
        public void SaveSertification(Record DataForDocuments)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                CertificateDGs certifications = new CertificateDGs();
                certifications.issueDate = DataForDocuments.GetOneStudent()["����������"];
                certifications.party = DataForDocuments.GetOneStudent()["�����"];
                string name = DataForDocuments.GetOneStudent()["���"].Trim();
                string surname = DataForDocuments.GetOneStudent()["�������".Trim()];
                string patronymic = DataForDocuments.GetOneStudent()["��������"].Trim();
                string dateBirth = DataForDocuments.GetOneStudent()["������������"].Trim();

                Students student = new Students();
                certifications.idStudent = student.SaveStudent(context, name, surname, patronymic, dateBirth);


                string nameProgramm = DataForDocuments.GetOneStudent()["�����������������"].Trim();
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
