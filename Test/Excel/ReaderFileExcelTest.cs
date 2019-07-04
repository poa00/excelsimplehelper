using NUnit.Framework;
using System.Collections.Generic;
using ViewModel.Read.ReadFile.Excel;

namespace Test.Excel
{
    [TestFixture]
    public class ReaderFileExcelTest
    {
        [Test]
        public void TestReaderFileExcel()
        {
            ReaderFileExcel reader = new ReaderFileExcel("D:\\program_uli\\Шаблон.xlsx", 1);
            string ExpectedBookmarkFIO = "ФИО";
            string ExpectedValueFIO = "Бакиров Даурен Зиннурович";

            string ExpectedBookmarkDataBirth = "ДатаРождения";
            string ExpectedValueDataBirth = "15.04.1984";

            string ExpectedBookmarkMark = "Оценка";
            string ExpectedValueMark = "4";


            foreach (KeyValuePair<string, string> keyValue in reader.records[1].GetOneStudent())
            {
                if (keyValue.Key == ExpectedBookmarkFIO)
                {
                    Assert.AreEqual(ExpectedBookmarkFIO, keyValue.Key);
                    Assert.AreEqual(ExpectedValueFIO, keyValue.Value);
                }
                if (keyValue.Key == ExpectedBookmarkDataBirth)
                {
                    Assert.AreEqual(ExpectedBookmarkDataBirth, keyValue.Key);
                    Assert.AreEqual(ExpectedValueDataBirth, keyValue.Value);
                }
                if (keyValue.Key == ExpectedBookmarkMark)
                {
                    Assert.AreEqual(ExpectedBookmarkMark, keyValue.Key);
                    Assert.AreEqual(ExpectedValueMark, keyValue.Value);
                }
            }
    }
}
