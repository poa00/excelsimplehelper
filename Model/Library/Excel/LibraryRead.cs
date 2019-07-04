using OfficeOpenXml;
using System.IO;

namespace Model.Library.Excel
{
    /// <summary>
    /// Инкапсуляция открытия файла используя библиотеку
    /// </summary>
    public class LibraryRead
    {
        public FileInfo existingFile;
        public ExcelPackage package;
        public ExcelWorksheet worksheet;

        public LibraryRead(string path, int Sheet)
        {
            existingFile = new FileInfo(path);
            package = new ExcelPackage(existingFile);
            worksheet = package.Workbook.Worksheets[Sheet];
        }
    }
}
