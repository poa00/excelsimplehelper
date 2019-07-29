using Model.Data;
using Model.Data.PatternMVVM;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Model;
using Model.File;
using Model.Message;
using System;
using System.Collections.Generic;

namespace Model.Write.Word.Document
{
    public class ManagerDocument
    {
        private int IdDocument;
        private Programs Program;
        private ProgramDGs ProgramDG;
        private FileExcel DateFromFile;
        private DocumentEvidenceAndUdostovereniyeModel EvidenceAndUdostovereniye;
        private CertificateDangerousGoodsModel CertificateDangerousGoods;
        private string PathTemplate;
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

        /// <summary>
        /// Создает ведомость, если пользователь захочет
        /// </summary>
        /// <param name="group">группа</param>
        private void isStatement(StudentRecord[] studentRecord, string group)
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
        /// <param name="group">группа</param>
        private void isStatement(StudentRecord[] studentRecord, string number, string group)
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
        private void createCertificate(string[] bookmarkWord)
        {
            DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile.GetRecords(), EvidenceAndUdostovereniye.DateStartEducation, EvidenceAndUdostovereniye.DateEndEducation,
                                                                        EvidenceAndUdostovereniye.DateIssueDocument, EvidenceAndUdostovereniye.Group, Program);
            dataSpec.Correction();

            isStatement(dataSpec.GetRecords(), EvidenceAndUdostovereniye.Group);
            
            if (IdDocument == 0)
            {
                PathTemplate = Properties.Settings.Default.PathFileWordEvidenceTemplate;
            }
            else
            {
                PathTemplate = Properties.Settings.Default.PathFileWordUdostovereniyeTemplate;
            }

            Document_ Evidence = new Document_(dataSpec.GetRecords(), PathTemplate, EvidenceAndUdostovereniye.Group);
            Evidence.AddBookmarksWord(bookmarkWord);
            Evidence.CreateDocument();
        }

        /// <summary>
        /// Создает сертификат ОГ
        /// </summary>
        /// <param name="bookmarkWord"></param>
        private void createCertificateDG(string[] bookmarkWord)
        {
            CertificateDangerousGoodsSpec dataSpec = new CertificateDangerousGoodsSpec(DateFromFile.GetRecords(), CertificateDangerousGoods.DateIssue, CertificateDangerousGoods.Number, ProgramDG);
            dataSpec.Correction();

            isStatement(dataSpec.GetRecords(), CertificateDangerousGoods.Number, CertificateDangerousGoods.Group);
            if (dataSpec.IsCertificate12Category == true)
            {
                PathTemplate = Properties.Settings.Default.PathFileWordCertificate12DGTemplate;
            }
            else
            {
                PathTemplate = Properties.Settings.Default.PathFileWordCertificateDGTemplate;
            }


            Document_ CertificatDG = new Document_(dataSpec.GetRecords(), PathTemplate, CertificateDangerousGoods.Number);
            CertificatDG.AddBookmarksWord(bookmarkWord);
            CertificatDG.CreateDocument();
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
                DateFromFile = new FileExcel(Properties.Settings.Default.PathFileExcelDataStudents, 1);
                DateFromFile.ReadFile();
                if (IdDocument == 0)
                {
                    string[] bookmarkWord = new string[19] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "ПовышенияКвалификации", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                    createCertificate(bookmarkWord);
                }
                if (IdDocument == 1 || IdDocument == 2)
                {
                    string[] bookmarkWord = new string[18] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                    createCertificate(bookmarkWord);
                }
                if (IdDocument == 3)
                {
                    string[] bookmarkWord = new string[7] { "КогдаКемУтверждена", "ДатаВыдачи", "Имя", "Программа", "Номер", "Отчество", "Фамилия" };
                    createCertificateDG(bookmarkWord);
                }
            }
        }
    }
}
