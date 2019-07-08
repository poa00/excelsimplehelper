using Model.Data;
using Model.Data.PatternMVVM;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Context;
using Model.DataBase.Model;
using Model.File;
using Model.Message;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace Model.Write.Word.Document
{
    public class Document_
    {
        private string[] BookmarksWord;
        private string[] FileName;
        private FileExcel DateFromFile;
        private string PathResult;
        private string PathTemplateWord;
        private Record[] DataForDocuments;
        private string TypeDocument;
        private string Group;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program">Программа(Модель)</param>
        /// <param name="evidenceAndUdostovereniyeSpec">Данные которые будут добавляться в файл</param>
        /// <param name="pathTemplateWord">Путь к шаблону ворда</param>
        public Document_(Record[] evidenceAndUdostovereniyeSpec, string pathTemplateWord, string group)
        {
            PathTemplateWord = pathTemplateWord;
            DateFromFile = new FileExcel(Properties.Settings.Default.TextPathFileExcelDataStudentsUdostovereniye, 1);
            DataForDocuments = evidenceAndUdostovereniyeSpec;
            FileName = CreateVoidCertification();
            TypeDocument = DataForDocuments[0].GetOneStudent()["Тип"];
            Group = group;
        }
        
        public void AddBookmarksWord(string[] bookmarksWord)
        {
            BookmarksWord = bookmarksWord;
        }

        int countDocuments;
        public string[] CreateVoidCertification()
        {
            countDocuments = DataForDocuments.Length;
            return CreateVoidDocumentWord(countDocuments);
        }

        /// <summary>
        /// Создает документы для функции распаралеливания
        /// </summary>
        /// <param name="CountDocument">Количество документов</param>
        /// <returns></returns>
        public string[] CreateVoidDocumentWord(int CountDocument)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Properties.Settings.Default.TextPathResulInputForParallelFolder);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
            string[] files = new string[CountDocument];
            for (int i = 0; i < CountDocument; i++)
            {
                files[i] = Properties.Settings.Default.TextPathResulInputForParallelFolder + "\\" + i + ".docx";
                System.IO.File.Copy(PathTemplateWord, files[i]);
            }
            return files;
        }

        public void CreateDocument()
        {
            CreatingFolderForDocuments();
            if (DataForDocuments.Length > 10)
            {
                CreateDocParallel();
            }
            else
            {
                CreateDoc();
            }
        }

        public void CreatingFolderForDocuments()
        {
            PathResult = Properties.Settings.Default.TextPathFolderResult + "\\" + TypeDocument + "_" + DataForDocuments[0].GetOneStudent()["Номер"];
            Directory.CreateDirectory(PathResult);
        }

        /// <summary>
        /// Заменяет по закладкам текст
        /// </summary>
        /// <param name="bookmark">закладка</param>
        /// <param name="doc"></param>
        /// <param name="insertValue">строка для замены закладки(вместо закладки ставиться текст)</param>
        /// <param name="idBookmark">номер закладки</param>
        /// <param name="messageError">сообщение об ошибке</param>
        private void InsertBookmarkCertification(Bookmark bookmark, DocX doc, string insertValue, int idBookmark)
        {
            //Проверка на наличие закладки в word документе
            if (doc.Bookmarks[BookmarksWord[idBookmark]] != null)
            {
                bookmark = doc.Bookmarks[BookmarksWord[idBookmark]];
                bookmark.SetText(insertValue);
            }
            else
            {
                MessageBug.AddMessage("Что-то не так проверьте: Закладку " + BookmarksWord[idBookmark] + " (Она в Word)" + "\n");
            }
        }

        /// <summary>
        /// Создание документа(Word)
        /// </summary>
        private void CreateDoc()
        {
            FileName = CreateVoidCertification();

            for (int j = 0; j < DataForDocuments.Length; j++)
            {
                using (var document = DocX.Load(FileName[0]))
                {
                    Bookmark bookmark = null;
                    for (int idBookmarkWord = 0; idBookmarkWord < BookmarksWord.Length; idBookmarkWord++)
                    {
                        foreach (KeyValuePair<string, string> keyValue in DataForDocuments[j].GetOneStudent())
                        {
                            if (keyValue.Key.Equals(BookmarksWord[idBookmarkWord]))
                            {
                                InsertBookmarkCertification(bookmark, document, DataForDocuments[j].GetOneStudent()[BookmarksWord[idBookmarkWord]], idBookmarkWord);
                                break;
                            }
                        }
                    }
                    SaveDocument(DataForDocuments[j]);
                    document.SaveAs(PathResult + "\\" + DataForDocuments[j].GetOneStudent()["Фамилия"] + "_" + DataForDocuments[j].GetOneStudent()["Имя"] + "_" + DataForDocuments[j].GetOneStudent()["Отчество"] + "_" + DataForDocuments[j].GetOneStudent()["Номер"] + ".doc");
                }
            }
        }

        /// <summary>
        /// Создание документа(Word) параллельно
        /// </summary>
        public void CreateDocParallel(string fileName)
        {
            using (var document = DocX.Load(fileName))
            {
                int j = Convert.ToInt32(Regex.Replace(fileName, @"[^\d]+", ""));

                Bookmark bookmark = null;
                for (int idBookmarkWord = 0; idBookmarkWord < BookmarksWord.Length; idBookmarkWord++)
                {
                    foreach (KeyValuePair<string, string> keyValue in DataForDocuments[j].GetOneStudent())
                    {
                        if (keyValue.Key == BookmarksWord[idBookmarkWord])
                        {
                            InsertBookmarkCertification(bookmark, document, DataForDocuments[j].GetOneStudent()[BookmarksWord[idBookmarkWord]], idBookmarkWord);
                        }
                    }
                }
                SaveDocument(DataForDocuments[j]);
                document.SaveAs(PathResult + "\\" + DataForDocuments[j].GetOneStudent()["Фамилия"] + "_" + DataForDocuments[j].GetOneStudent()["Имя"] + "_" + DataForDocuments[j].GetOneStudent()["Отчество"] + "_" + DataForDocuments[j].GetOneStudent()["Номер"] + ".doc");
            }
        }

        private void SaveDocument(Record DataForDocuments)
        {
            if (DataForDocuments.GetOneStudent()["Тип"] == "Сертификат ОГ")
            {
                CertificateDGs certifications = new CertificateDGs();
                certifications.SaveSertification(DataForDocuments);
            }
            else
            {
                Certificate certifications = new Certificate();
                certifications.SaveCertificate(DataForDocuments);
            }    
        }

        /// <summary>
        /// Для паралельности 
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        private long Parallel_ReplaceText(DirectoryInfo di)
        {
            // Create a new Stopwatch, we will use this to time execution.
            Stopwatch sw = new Stopwatch();

            sw.Start(); // Start the stop watch.

            // Loop through each document in this specified direction.
            System.Threading.Tasks.Parallel.ForEach
            (
                di.GetFiles(),
                currentFile =>
                {
                    CreateDocParallel(currentFile.FullName);
                }
            );

            sw.Stop(); // Stop the stop watch.

            // Return the time taken in miliseconds.
            return sw.ElapsedMilliseconds;
        }

        public void CreateDocParallel()
        {
            DirectoryInfo di = new DirectoryInfo(Properties.Settings.Default.TextPathResulInputForParallelFolder);
            Parallel_ReplaceText(di);
        }
    }
}
