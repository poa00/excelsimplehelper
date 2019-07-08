using global::Model.Data;
using global::Model.Data.SpecificationDataDocument;
using global::Model.DataBase.Context;
using global::Model.Write.Word.Document;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model.DataBase.Model
{
    [Table("Certificate")]
    public partial class Certificate
    {
        public int id { get; set; }

        [Required]
        [StringLength(18)]
        public string startEducation { get; set; }

        [Required]
        [StringLength(18)]
        public string endEducation { get; set; }

        [Required]
        [StringLength(18)]
        public string issueDate { get; set; }

        [Required]
        [StringLength(10)]
        public string party { get; set; }

        [Required]
        [StringLength(7)]
        public string mark { get; set; }

        public int idStudent { get; set; }

        public int idProgramm { get; set; }

        public virtual Programs Programs { get; set; }

        public virtual Students Students { get; set; }

        /// <summary>
        /// �������� �� ������� ����������� �� ��� ������
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
        /// ������������ ���������� ����� ��� ������� �������� �����������
        /// � ��� ������ ���� �������
        /// </summary>
        /// <param name="certification"></param>
        /// <param name="bookmarkWord"></param>
        private void loadCertificate(Certificate certification, string[] bookmarkWord)
        {
            Record[] DateFromFile = new Record[1];
            DateFromFile[0] = new Record();
            DateFromFile[0].AddPropertyRecord("�������", certification.Students.surname);
            DateFromFile[0].AddPropertyRecord("���", certification.Students.name);
            DateFromFile[0].AddPropertyRecord("��������", certification.Students.patronymic);
            DateFromFile[0].AddPropertyRecord("������������", certification.Students.dateDirth);
            DateFromFile[0].AddPropertyRecord("������", certification.mark);
            DateFromFile[0].AddPropertyRecord("�����", certification.party);
            DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile, certification.startEducation, certification.endEducation,
                                                                certification.issueDate, certification.party, certification.Programs);
            dataSpec.CorrectionLoad();
            Document_ Evidence = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordEvidenceTemplate, certification.party);
            Evidence.AddBookmarksWord(bookmarkWord);
            Evidence.CreateDocument();
        }

        /// <summary>
        /// �������� ����������� �� ���� ������
        /// </summary>
        /// <param name="certification"></param>
        public void LoadCertificate(Certificate certification)
        {
            if (certification.Programs.TypeDocument.Id == 0)
            {
                string[] bookmarkWord = new string[19] { "�������", "���", "��������", "������������", "�����", "������", "���������", "�����", "���������������������", "����", "��", "��", "��", "��", "��", "��", "��", "��", "��" };
                loadCertificate(certification, bookmarkWord);
            }
            if (certification.Programs.TypeDocument.Id == 1 || certification.Programs.TypeDocument.Id == 2)
            {
                string[] bookmarkWord = new string[18] { "�������", "���", "��������", "������������", "�����", "������", "���������", "�����", "����", "��", "��", "��", "��", "��", "��", "��", "��", "��" };
                loadCertificate(certification, bookmarkWord);
            }
        }

        /// <summary>
        /// ��������� ���������� � ���� ������,
        /// ����� ��������� �������� ���� ��� ��� � ����
        /// </summary>
        /// <param name="DataForDocuments">������ ��� ����������</param>
        public void SaveCertificate(Record DataForDocuments)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                Certificate certifications = new Certificate();
                certifications.endEducation = DataForDocuments.GetOneStudent()["��"] + "." + DataForDocuments.GetOneStudent()["��"] + "." + DataForDocuments.GetOneStudent()["��"];
                certifications.startEducation = DataForDocuments.GetOneStudent()["��"] + "." + DataForDocuments.GetOneStudent()["��"] + "." + DataForDocuments.GetOneStudent()["��"];
                certifications.issueDate = DataForDocuments.GetOneStudent()["��"] + "." + DataForDocuments.GetOneStudent()["��"] + "." + DataForDocuments.GetOneStudent()["��"];
                certifications.party = DataForDocuments.GetOneStudent()["�����"];
                certifications.mark = DataForDocuments.GetOneStudent()["������"];

                string name = DataForDocuments.GetOneStudent()["���"].Trim();
                string surname = DataForDocuments.GetOneStudent()["�������".Trim()];
                string patronymic = DataForDocuments.GetOneStudent()["��������"].Trim();
                string dateBirth = DataForDocuments.GetOneStudent()["������������"].Trim();

                Students student = new Students();
                certifications.idStudent = student.SaveStudent(context, name, surname, patronymic, dateBirth);

                string nameProgramm = DataForDocuments.GetOneStudent()["���������"];
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
        /// ����� ����������� �� ������
        /// </summary>
        /// <param name="context">������ � ���� ������</param>
        /// <param name="group">�����</param>
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
