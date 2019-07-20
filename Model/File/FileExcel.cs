using System;
using Model.Data;
using OfficeOpenXml;
using System.IO;

namespace Model.File
{
    /// <summary>
    /// Файл эксель
    /// </summary>
    public class FileExcel
    {
        /// <summary>
        /// Лист с записями студентов
        /// </summary>
        private StudentRecord[] studentRecords;
        /// <summary>
        /// Количество заполненых строк в файле
        /// </summary>
        private int filledString;
        public FileInfo existingFile;
        public ExcelPackage package;
        public ExcelWorksheet worksheet;
        const int LINE_NUMBER_FROM_WHICH_FILE_IS_FILED = 2;

        public FileExcel(string path, int Sheet)
        {
            existingFile = new FileInfo(path);
            package = new ExcelPackage(existingFile);
            worksheet = package.Workbook.Worksheets[Sheet];
            GetLineAndColumn();
        }

        public StudentRecord[] GetRecords()
        {
            return studentRecords;
        }

        /// <summary>
        /// Получить количество строк, столбцов в файле
        /// </summary>
        /// <returns></returns>
        private void GetLineAndColumn()
        {
            // 1 строка в файле названия колонок, со 2 начинаются данные
            int fileLine = LINE_NUMBER_FROM_WHICH_FILE_IS_FILED;
            while (worksheet.Cells[fileLine, 1].Value != null)
            {
                fileLine++;
            }
            filledString = fileLine - LINE_NUMBER_FROM_WHICH_FILE_IS_FILED;
            studentRecords = new StudentRecord[filledString];
        }

        /// <summary>
        /// Чтение файла excel в лист
        /// </summary>
        /// <returns></returns>
        public void ReadFile()
        {
            using (package)
            {
                int fileLine = LINE_NUMBER_FROM_WHICH_FILE_IS_FILED;// строка в файле excel
                GetLineAndColumn();
                while (worksheet.Cells[fileLine, 1].Value != null)
                {

                    int fileСolumn = 1;// столбец      
                    StudentRecord record = new StudentRecord();
                    while (worksheet.Cells[1, fileСolumn].Value != null)
                    {
                        if (worksheet.Cells[fileLine, fileСolumn].Value != null)
                        {
                            record.AddPropertyRecord(Convert.ToString(worksheet.Cells[1, fileСolumn].Value), Convert.ToString(worksheet.Cells[fileLine, fileСolumn].Value));
                        }
                        else
                        {
                            record.AddPropertyRecord(Convert.ToString(worksheet.Cells[1, fileСolumn].Value), " ");
                        }
                        fileСolumn++;
                    }
                    studentRecords[fileLine - LINE_NUMBER_FROM_WHICH_FILE_IS_FILED] = record;
                    fileLine++;
                }
            }
        }
    }
}