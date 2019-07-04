using Model.Data;
using Model.Data.PatternMVVM;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Context;
using Model.DataBase.Model;
using Model.File;
using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<string> DocumentCreate()
        {
            DateFromFile = new FileExcel(Properties.Settings.Default.TextPathFileExcelDataStudentsUdostovereniye, 1);
            DateFromFile.ReadFile();
            if (MessageBug.GetMessages().Count > 0)
            {
                return MessageBug.GetMessages();
            }
            else
            {
                if (IdDocument == 0)
                {
                    string[] bookmarkWord = new string[19] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "ПовышенияКвалификации", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                    DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile.GetRecords(), EvidenceAndUdostovereniye.DateStartEducation, EvidenceAndUdostovereniye.DateEndEducation,
                                                                        EvidenceAndUdostovereniye.DateIssueDocument, EvidenceAndUdostovereniye.Group, Program);
                    dataSpec.Correction();

                    if (EvidenceAndUdostovereniye.IsSelectedStatement)
                    {
                        StatementModel statementModel = new StatementModel();
                        statementModel.Group = EvidenceAndUdostovereniye.Group;
                        Statement statement = new Statement(Program, statementModel);
                        statement.DocumentCreate();
                    }

                    Document_ Evidence = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordEvidenceTemplate);
                    Evidence.AddBookmarksWord(bookmarkWord);
                    Evidence.CreateDocument();
                }
                if (IdDocument == 1 || IdDocument == 2)
                {
                    string[] bookmarkWord = new string[18] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                    DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile.GetRecords(), EvidenceAndUdostovereniye.DateStartEducation, EvidenceAndUdostovereniye.DateEndEducation,
                                                                        EvidenceAndUdostovereniye.DateIssueDocument, EvidenceAndUdostovereniye.Group, Program);
                    dataSpec.Correction();

                    if (EvidenceAndUdostovereniye.IsSelectedStatement)
                    {
                        StatementModel statementModel = new StatementModel();
                        statementModel.Group = EvidenceAndUdostovereniye.Group;
                        Statement statement = new Statement(Program, statementModel);
                        statement.DocumentCreate();
                    }

                    Document_ UdostoverenieL = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordUdostovereniyeTemplate);
                    UdostoverenieL.AddBookmarksWord(bookmarkWord);
                    UdostoverenieL.CreateDocument();
                }
                if (IdDocument == 3)
                {
                    string[] bookmarkWord = new string[7] { "КогдаКемУтверждена", "ДатаВыдачи", "Имя", "НазваниеПрограммы", "Номер", "Отчество", "Фамилия" };
                    CertificateDangerousGoodsSpec dataSpec = new CertificateDangerousGoodsSpec(DateFromFile.GetRecords(), CertificateDangerousGoods.DateIssue, CertificateDangerousGoods.Number, ProgramDG);
                    dataSpec.Correction();

                    if (EvidenceAndUdostovereniye.IsSelectedStatement)
                    {
                        StatementModel statementModel = new StatementModel();
                        statementModel.Group = EvidenceAndUdostovereniye.Group;
                        Statement statement = new Statement(Program, statementModel);
                        statement.DocumentCreate();
                    }

                    Document_ CertificatDG = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordCertificateDGTemplate);
                    CertificatDG.AddBookmarksWord(bookmarkWord);
                    CertificatDG.CreateDocument();
                }
                return MessageBug.GetMessages();
            }
        }

        public void DocumentLoad(Certifications certification)
        {
            if (certification.Programs.TypeDocument.Id == 0)
            {
                string[] bookmarkWord = new string[19] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "ПовышенияКвалификации", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                Record[] DateFromFile = new Record[1];
                DateFromFile[0] = new Record();
                DateFromFile[0].AddPropertyRecord("Фамилия", certification.Students.surname);
                DateFromFile[0].AddPropertyRecord("Имя", certification.Students.name);
                DateFromFile[0].AddPropertyRecord("Отчество", certification.Students.patronymic);
                DateFromFile[0].AddPropertyRecord("Оценка", certification.mark);
                DateFromFile[0].AddPropertyRecord("ДатаРождения", certification.Students.dateDirth);
                DateFromFile[0].AddPropertyRecord("Номер", certification.party);
                DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile, certification.startEducation, certification.endEducation,
                                                                    certification.issueDate, certification.party.ToString(), certification.Programs);
                dataSpec.CorrectionLoad();
                Document_ Evidence = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordEvidenceTemplate);
                Evidence.AddBookmarksWord(bookmarkWord);
                Evidence.CreateDocument();
            }
            if (certification.Programs.TypeDocument.Id == 1 || certification.Programs.TypeDocument.Id == 2)
            {
                string[] bookmarkWord = new string[18] { "Фамилия", "Имя", "Отчество", "ДатаРождения", "Номер", "Оценка", "Программа", "Уроки", "Часы", "НД", "НМ", "НГ", "КД", "КМ", "КГ", "ПД", "ПМ", "ПГ" };
                Record[] DateFromFile = new Record[1];
                DateFromFile[0].AddPropertyRecord("Имя", certification.Students.name);
                DateFromFile[0].AddPropertyRecord("Фамилия", certification.Students.surname);
                DateFromFile[0].AddPropertyRecord("Отчество", certification.Students.patronymic);
                DateFromFile[0].AddPropertyRecord("ДатаРождения", certification.Students.dateDirth);
                DateFromFile[0].AddPropertyRecord("Оценка", certification.Students.dateDirth);
                DocumentEvidenceAndUdostovereniyeSpec dataSpec = new DocumentEvidenceAndUdostovereniyeSpec(DateFromFile, certification.startEducation, certification.endEducation,
                                                                    certification.issueDate, certification.party.ToString(), certification.Programs);
                dataSpec.Correction();
                Document_ UdostoverenieL = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordUdostovereniyeTemplate);
                UdostoverenieL.AddBookmarksWord(bookmarkWord);
                UdostoverenieL.CreateDocument();
            }
        }

        public void DocumentLoad(CertificateDGs certification)
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
            Document_ document = new Document_(dataSpec.GetRecords(), Properties.Settings.Default.TextPathFileWordCertificateDGTemplate);
            document.AddBookmarksWord(bookmarkWord);
            document.CreateDocument();
        }

        public void DocumentSaveCertificate(Record DataForDocuments)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                Certifications certifications = new Certifications();
                certifications.endEducation = DataForDocuments.GetOneStudent()["КД"] + "." + DataForDocuments.GetOneStudent()["КМ"] + "." + DataForDocuments.GetOneStudent()["КГ"];
                certifications.startEducation = DataForDocuments.GetOneStudent()["НД"] + "." + DataForDocuments.GetOneStudent()["НМ"] + "." + DataForDocuments.GetOneStudent()["НГ"];
                certifications.issueDate = DataForDocuments.GetOneStudent()["ПД"] + "." + DataForDocuments.GetOneStudent()["ПМ"] + "." + DataForDocuments.GetOneStudent()["ПГ"];
                certifications.party = DataForDocuments.GetOneStudent()["Номер"];
                certifications.mark = DataForDocuments.GetOneStudent()["Оценка"];

                string name = DataForDocuments.GetOneStudent()["Имя"].Trim();
                string surname = DataForDocuments.GetOneStudent()["Фамилия".Trim()];
                string patronymic = DataForDocuments.GetOneStudent()["Отчество"].Trim();
                string dateDirth = DataForDocuments.GetOneStudent()["ДатаРождения"].Trim();
                var student = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == dateDirth);
                int idStudent;
                if (student.Count() > 0)
                {
                    idStudent = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == dateDirth).First().id;
                }
                else
                {

                    Students s = new Students()
                    {
                        name = name,
                        surname = surname,
                        patronymic = patronymic,
                        dateDirth = dateDirth
                    };
                    context.Students.Add(s);
                    context.SaveChanges();
                    idStudent = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == dateDirth).First().id;
                }

                certifications.idStudent = idStudent;

                string nameProgramm = DataForDocuments.GetOneStudent()["Программа"].Trim();
                var programs1 = context.Programs
                    .Where(c => c.name == nameProgramm);
                certifications.idProgramm = programs1.First().id;

                var certificat = context.Certifications.Where(c => c.party == certifications.party);
                if (certificat.Count() > 0)
                {

                }
                else
                {
                    context.Certifications.Add(certifications);
                }

                context.SaveChanges();
            }
        }

        public void DocumentSaveCertificateDG(Record DataForDocuments)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                CertificateDGs certifications = new CertificateDGs();
                certifications.issueDate = DataForDocuments.GetOneStudent()["ДатаВыдачи"];
                certifications.party = DataForDocuments.GetOneStudent()["Номер"];
                string name = DataForDocuments.GetOneStudent()["Имя"].Trim();
                string surname = DataForDocuments.GetOneStudent()["Фамилия".Trim()];
                string patronymic = DataForDocuments.GetOneStudent()["Отчество"].Trim();
                string dateDirth = DataForDocuments.GetOneStudent()["ДатаРождения"].Trim();
                var student = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == dateDirth);
                int idStudent;
                if (student.Count() > 0)
                {
                    idStudent = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == dateDirth).First().id;
                }
                else
                {

                    Students s = new Students()
                    {
                        name = name,
                        surname = surname,
                        patronymic = patronymic,
                        dateDirth = dateDirth
                    };
                    context.Students.Add(s);
                    context.SaveChanges();
                    idStudent = context.Students
                    .Where(c => c.name == name &&
                            c.surname == surname &&
                            c.patronymic == patronymic &&
                            c.dateDirth == dateDirth).First().id;
                }

                certifications.idStudent = idStudent;

                string nameProgramm = DataForDocuments.GetOneStudent()["НазваниеПрограммы"].Trim();
                var programs1 = context.ProgramDGs
                    .Where(c => c.name == nameProgramm);
                certifications.idProgramDG = programs1.First().id;

                var certificat = context.CertificateDGs.Where(c => c.party == certifications.party);
                if (certificat.Count() > 0)
                {

                }
                else
                {
                    context.CertificateDGs.Add(certifications);
                }
                context.SaveChanges();
            }    
        }
    }
}
