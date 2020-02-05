using System.IO;
using Model.Data;
using Model.DataBase.Model;
using Model.FileExcel_;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Model.Data.SpecificationDataDocument;

namespace Model.Write.Word.Document
{
    internal class Document_
    {
        private string[] BookmarksWord;
        private FileExcel DateFromFile;
        private string PathResult;
        private string PathTemplateWord;
        private StudentRecord[] DataForDocuments;
        private string TypeDocument;
        private string Group;
        private PropertiesDocument propertiesDocument;
        private SpecFunction SpecFunction_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program">Программа(Модель)</param>
        /// <param name="evidenceAndUdostovereniyeSpec">Данные которые будут добавляться в файл</param>
        /// <param name="pathTemplateWord">Путь к шаблону ворда</param>
        public Document_(StudentRecord[] recordSpec, string pathTemplateWord, string group)
        {
            PathTemplateWord = pathTemplateWord;
            DateFromFile = new FileExcel(Properties.Settings.Default.PathFileExcelDataStudents, 1);
            propertiesDocument = new PropertiesDocument();

            DataForDocuments = recordSpec;
            TypeDocument = DataForDocuments[0].GetOneStudent()["Тип"];
            Group = group;

            SpecFunction_ = new SpecFunction();
        }
        
        public void AddBookmarksWord(string[] bookmarksWord)
        {
            BookmarksWord = bookmarksWord;
        }

        public void CreateDocument()
        {
            CreatingFolderForDocuments();
            CreateDoc();
        }

        public void CreatingFolderForDocuments()
        {
            PathResult = Properties.Settings.Default.PathFolderResult + "\\" + TypeDocument + "_" + Group;
            Directory.CreateDirectory(PathResult);
        }
        
        /// <summary>
        /// Создание документа(Word)
        /// </summary>
        private void CreateDoc()
        {
            byte[] textByteArray = File.ReadAllBytes(PathTemplateWord);

            for (int j = 0; j < DataForDocuments.Length; j++)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(textByteArray, 0, textByteArray.Length);
                    using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                    {
                        var bookMarks = FindBookmarks(doc.MainDocumentPart.Document);// Ищем все закладки в документе
                        foreach (var current in bookMarks)
                        {
                            var run = new Run();
                            if (current.Key == "_GoBack")
                            {
                                continue;
                            }

                            if (current.Key == "ПовышенияКвалификации" &&(TypeDocument == "Удостоверения (Лицензия)" || TypeDocument == "Удостоверение (Реквизит)"))
                            {
                                continue;
                            }

                            if (current.Key == "Уроки")
                            {
                                string[] lesson = SpecFunction_.CutFromStringElements(DataForDocuments[j].GetOneStudent()[current.Key], '\r');
                                for (int i = lesson.Length - 1; i > -1; i--)
                                {
                                    var text = new Text(lesson[i]);
                                    run = propertiesDocument.GetProperties(TypeDocument, current.Key, text);
                                    current.Value.InsertAfterSelf(run);

                                    var run2 = new Run(new Break());
                                    current.Value.InsertAfterSelf(run2);
                                }
                                continue;
                            }

                            var textElement = new Text(DataForDocuments[j].GetOneStudent()[current.Key]);
                            run = propertiesDocument.GetProperties(TypeDocument, current.Key, textElement);
                            current.Value.InsertAfterSelf(run);
                        }
                    }
                    File.WriteAllBytes(PathResult +
                                "\\" + DataForDocuments[j].GetOneStudent()["Фамилия"] +
                                "_" + DataForDocuments[j].GetOneStudent()["Имя"] +
                                "_" + DataForDocuments[j].GetOneStudent()["Отчество"] +
                                "_" + DataForDocuments[j].GetOneStudent()["Номер"] + ".docx",
                                stream.ToArray());
                        SaveDocument(DataForDocuments[j]);
                }
            }
        }

        // Получаем все закладки в документе
        // bStartWithNoEnds - словарь, который будет содержать только начало закладок,
        // чтобы потом по ним находить соответствующие им концы закладок
        // documentPart - элемент Word-документа
        // outs - конечный результат
        private static Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> outs = null, Dictionary<string, string> bStartWithNoEnds = null)
        {
            if (outs == null) { outs = new Dictionary<string, BookmarkEnd>(); }
            if (bStartWithNoEnds == null) { bStartWithNoEnds = new Dictionary<string, string>(); }

            // Проходимся по всем элементам на странице Word-документа
            foreach (var docElement in documentPart.Elements())
            {
                // BookmarkStart определяет начало закладки в рамках документа
                // маркер начала связан с маркером конца закладки
                if (docElement is BookmarkStart)
                {
                    var bookmarkStart = docElement as BookmarkStart;
                    // Записываем id и имя закладки
                    bStartWithNoEnds.Add(bookmarkStart.Id, bookmarkStart.Name);
                }

                // BookmarkEnd определяет конец закладки в рамках документа
                if (docElement is BookmarkEnd)
                {
                    var bookmarkEnd = docElement as BookmarkEnd;
                    foreach (var startName in bStartWithNoEnds)
                    {
                        // startName.Key как раз и содержит id закладки
                        // здесь проверяем, что есть связь между началом и концом закладки
                        if (bookmarkEnd.Id == startName.Key)
                            // В конечный массив добавляем то, что нам и нужно получить
                            outs.Add(startName.Value, bookmarkEnd);
                    }
                }
                // Рекурсивно вызываем данный метод, чтобы пройтись по всем элементам
                // word-документа
                FindBookmarks(docElement, outs, bStartWithNoEnds);
            }

            return outs;
        }

        private void SaveDocument(StudentRecord DataForDocuments)
        {
            if (DataForDocuments.GetOneStudent()["Тип"] == "Сертификат ОГ")
            {
                CertificateDGs certifications = new CertificateDGs();
                certifications.SaveSertification(DataForDocuments);
            }
            if (DataForDocuments.GetOneStudent()["Тип"] == "Удостоверение (Реквизит)" || DataForDocuments.GetOneStudent()["Тип"] == "Удостоверения (Лицензия)" || DataForDocuments.GetOneStudent()["Тип"] == "Свидетельство")
            {
                Certificate certifications = new Certificate();
                certifications.SaveCertificate(DataForDocuments);
            }    
        }
    }
}
