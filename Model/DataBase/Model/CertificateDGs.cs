using global::Model.Data;
using global::Model.Data.SpecificationDataDocument;
using global::Model.DataBase.Context;
using global::Model.Write.Word.Document;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Model.DataBase.Model
{
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

        /// <summary>
        /// �������� �� ������� ����������� �� ��� ������
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
            string[] bookmarkWord = new string[7] { "������������������", "����������", "���", "���������", "�����", "��������", "�������" };
            StudentRecord[] DateFromFile = new StudentRecord[1];
            DateFromFile[0] = new StudentRecord();
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
        public void SaveSertification(StudentRecord DataForDocuments)
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


                string nameProgramm = DataForDocuments.GetOneStudent()["���������"].Trim();
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
        /// ����� ����������� �� ������
        /// </summary>
        /// <param name="context">������ � ���� ������</param>
        /// <param name="group">�����</param>
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
    }
}
