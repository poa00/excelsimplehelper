using Model.Data;
using Model.Data.PatternMVVM;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Model;
using Model.Message;

namespace Model.Write.Word.Document
{
    public class ManagerDocument
    {
        private int IdDocument;
        private Programs Program;
        private ProgramDGs ProgramDG;
        private FileExcel_.FileExcel DateFromFile;
        private DocumentEvidenceAndUdostovereniyeModel EvidenceAndUdostovereniye;
        private CertificateDangerousGoodsModel CertificateDangerousGoods;
        private string PathTemplateDocument;
        public ManagerDocument()
        {
        }

        public ManagerDocument(Programs program, DocumentEvidenceAndUdostovereniyeModel evidenceAndUdostovereniye)
        {
            IdDocument = program.TypeDocument.Id;
            Program = program;
            EvidenceAndUdostovereniye = evidenceAndUdostovereniye;
        }
        public ManagerDocument(ProgramDGs program, CertificateDangerousGoodsModel certificateDangerousGoods)
        {
            IdDocument = program.TypeDocument.Id;
            ProgramDG = program;
            CertificateDangerousGoods = certificateDangerousGoods;
        }

        public ManagerDocument(ProgramDGs program, CertificateDangerousGoodsModel certificateDangerousGoods, int type)
        {
            IdDocument = type;
            ProgramDG = program;
            CertificateDangerousGoods = certificateDangerousGoods;
        }
        public ManagerDocument(Programs program, DocumentEvidenceAndUdostovereniyeModel evidenceAndUdostovereniye, int type)
        {
            IdDocument = type;
            Program = program;
            EvidenceAndUdostovereniye = evidenceAndUdostovereniye;
        }

        /// <summary>
        /// Создает ведомость, если пользователь захочет
        /// </summary>
        private void CreateStatement(StudentRecord[] studentRecord, string group)
        {
            if (EvidenceAndUdostovereniye != null && EvidenceAndUdostovereniye.IsSelectedStatement == true)
            {
                Statement statement = new Statement(studentRecord, Program.name, group);
                statement.DocumentCreate();
            }
        }
        /// <summary>
        /// Создает ведомость, если пользователь захочет
        /// </summary>
        private void CreateStatement(StudentRecord[] studentRecord, string group, string number)
        {
            if (CertificateDangerousGoods != null && CertificateDangerousGoods.IsSelectedStatement == true)
            {
                Statement statement = new Statement(studentRecord, ProgramDG.name, group, number);
                statement.DocumentCreate();
            }
        }

        /// <summary>
        /// Создает сертификат
        /// </summary>
        /// <param name="bookmarkWord"></param>
        private void CreateCertificate()
        {
            DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile.GetRecords(), EvidenceAndUdostovereniye.DateStartEducation, EvidenceAndUdostovereniye.DateEndEducation,
                                                                        EvidenceAndUdostovereniye.DateIssueDocument, EvidenceAndUdostovereniye.Group, Program);
            dataSpec.Correction();
            if (IdDocument == 0)
            {
                PathTemplateDocument = Properties.Settings.Default.PathFileWordEvidenceTemplate;
            }
            if(IdDocument == 1 || IdDocument == 2)
            {
                PathTemplateDocument = Properties.Settings.Default.PathFileWordUdostovereniyeTemplate;
            }

            Document_ Evidence = new Document_(dataSpec.GetRecords(), PathTemplateDocument, EvidenceAndUdostovereniye.Group);
            Evidence.CreateDocument(DateFromFile.GetRecords()[0].GetOneStudent()["Тип"]);

            CreateStatement(DateFromFile.GetRecords(), EvidenceAndUdostovereniye.Group);
        }

        /// <summary>
        /// Создает сертификат ОГ
        /// </summary>
        /// <param name="bookmarkWord"></param>
        private void CreateCertificateDG()
        {
            CertificateDangerousGoodsSpec dataSpec = new CertificateDangerousGoodsSpec(DateFromFile.GetRecords(), CertificateDangerousGoods.DateIssue, CertificateDangerousGoods.Number, ProgramDG);
            dataSpec.Correction();
            if (dataSpec.IsCertificate12Category == true)
            {
                PathTemplateDocument = Properties.Settings.Default.PathFileWordCertificate12DGTemplate;
            }
            else
            {
                PathTemplateDocument = Properties.Settings.Default.PathFileWordCertificateDGTemplate;
            }

            Document_ CertificatDG = new Document_(dataSpec.GetRecords(), PathTemplateDocument, CertificateDangerousGoods.Number);
            CertificatDG.CreateDocument(DateFromFile.GetRecords()[0].GetOneStudent()["Тип"]);

            CreateStatement(DateFromFile.GetRecords(), CertificateDangerousGoods.Group, CertificateDangerousGoods.Number);
        }

        public void DocumentCreate()
        {
            if(Properties.Settings.Default.PathFileExcelDataStudents == "Выбери свой путь" ||
                    Properties.Settings.Default.PathFileWordCertificate12DGTemplate == "Выбери свой путь" ||
                Properties.Settings.Default.PathFileWordCertificateDGTemplate == "Выбери свой путь" ||
                    Properties.Settings.Default.PathFileWordEvidenceTemplate == "Выбери свой путь" ||
                Properties.Settings.Default.PathFileWordStatementTemplate == "Выбери свой путь" ||
                    Properties.Settings.Default.PathFileWordUdostovereniyeTemplate == "Выбери свой путь" ||
                Properties.Settings.Default.PathFolderResult == "Выбери свой путь" ||
                    Properties.Settings.Default.PathResulInputForParallelFolder == "Выбери свой путь")
                
            {
                MessageBug.AddMessage(MessageBug.message.Проблема_с_путем_к_файлу_или_папке, "посмотрите файл настройки");
            }
            else
            {
                DateFromFile = new FileExcel_.FileExcel(Properties.Settings.Default.PathFileExcelDataStudents, 1);
                DateFromFile.ReadFile();
                if (IdDocument == 0 || IdDocument == 1 || IdDocument == 2)
                {
                    CreateCertificate();
                }
                if (IdDocument == 3)
                {
                    CreateCertificateDG();
                }
            }
        }
    }
}
