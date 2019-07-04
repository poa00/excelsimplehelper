using System;
using Model.Data;
using Model.Library.Excel;
using Model.Message;

namespace Model.File
{
    /// <summary>
    /// Файл эксель в ввиде массива 
    /// </summary>
    public class FileExcel : LibraryRead
    {
        /// <summary>
        /// Лист с записями студентов
        /// </summary>
        private Record[] records;
        private int Line;
        public FileExcel(string path, int Sheet) : base(path, Sheet)
        {
            GetLineAndColumn();
        }

        public Record[] GetRecords()
        {
            return records;
        }

        /// <summary>
        /// Получить количество строк, столбцов в файле
        /// </summary>
        /// <returns></returns>
        private void GetLineAndColumn()
        {
            int line = 2;
            while (worksheet.Cells[line, 1].Value != null)
            {
                line++;
            }
            Line = line - 2;
            records = new Record[Line];
        }

        /// <summary>
        /// Чтение файла excel в лист
        /// </summary>
        /// <returns></returns>
        public void ReadFile()
        {
            using (package)
            {
                int line = 2;// строка в файле excel
                GetLineAndColumn();
                while (worksheet.Cells[line, 1].Value != null)
                {

                    int column = 1;// столбец      
                    Record record = new Record();
                    while (worksheet.Cells[1, column].Value != null)
                    {
                        if (worksheet.Cells[line, column].Value != null)
                        {
                            record.AddPropertyRecord(Convert.ToString(worksheet.Cells[1, column].Value), Convert.ToString(worksheet.Cells[line, column].Value));
                        }
                        else
                        {
                            record.AddPropertyRecord(Convert.ToString(worksheet.Cells[1, column].Value), " ");
                        }
                        column++;
                    }
                    records[line - 2] = record;
                    line++;
                }
            }
        }
    }
}