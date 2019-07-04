using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.File;

namespace UnitTest.Excel
{
    [TestClass]
    public class UnitTestFileExcel
    {
        [TestMethod]
        public void TestMethod1()
        {
            FileExcel reader = new FileExcel(@"C:\Users\NikitaK\Documents\Создание ведомостей\Группы\634.xlsx", 1);
            string ExpectedBookmarkFio_Min = "ФИО";
            string ExpectedValueFio_Min = "Бакиров Даурен Зиннурович";

            string ExpectedBookmarkDataBirth_Min = "ДатаРождения";
            string ExpectedValueDataBirth_Min = "15.04.1984";

            string ExpectedBookmarkMark_Min = "Оценка";
            string ExpectedValueMark_Min = "4";
               
            string ExpectedBookmarkFio = "ФИО";
            string ExpectedValueFio = "Глушко Семен Эдуардович";

            string ExpectedBookmarkDataBirth = "ДатаРождения";
            string ExpectedValueDataBirth = "06.11.1995";

            string ExpectedBookmarkMark = "Оценка";
            string ExpectedValueMark = "4";

            string ExpectedBookmarkFio_Max = "ФИО";
            string ExpectedValueFio_Max = "Яковец Евгений Сергеевич";

            string ExpectedBookmarkDataBirth_Max = "ДатаРождения";
            string ExpectedValueDataBirth_Max = "29.01.1987";

            string ExpectedBookmarkMark_Max = "Оценка";
            string ExpectedValueMark_Max = "5";

            foreach (KeyValuePair<string, string> keyValue in reader.GetRecords()[0].GetOneStudent())
            {
                if (keyValue.Key == ExpectedBookmarkFio_Min)
                {
                    Assert.AreEqual(ExpectedBookmarkFio_Min, keyValue.Key);
                    Assert.AreEqual(ExpectedValueFio_Min, keyValue.Value);
                }
                if (keyValue.Key == ExpectedBookmarkDataBirth_Min)
                {
                    Assert.AreEqual(ExpectedBookmarkDataBirth_Min, keyValue.Key);
                    Assert.AreEqual(ExpectedValueDataBirth_Min, keyValue.Value);
                }
                if (keyValue.Key == ExpectedBookmarkMark_Min)
                {
                    Assert.AreEqual(ExpectedBookmarkMark_Min, keyValue.Key);
                    Assert.AreEqual(ExpectedValueMark_Min, keyValue.Value);
                }
            }

            foreach (KeyValuePair<string, string> keyValue in reader.GetRecords()[11].GetOneStudent())
            {
                if (keyValue.Key == ExpectedBookmarkFio_Max)
                {
                    Assert.AreEqual(ExpectedBookmarkFio_Max, keyValue.Key);
                    Assert.AreEqual(ExpectedValueFio_Max, keyValue.Value);
                }
                if (keyValue.Key == ExpectedBookmarkDataBirth_Max)
                {
                    Assert.AreEqual(ExpectedBookmarkDataBirth_Max, keyValue.Key);
                    Assert.AreEqual(ExpectedValueDataBirth_Max, keyValue.Value);
                }
                if (keyValue.Key == ExpectedBookmarkMark_Max)
                {
                    Assert.AreEqual(ExpectedBookmarkMark_Max, keyValue.Key);
                    Assert.AreEqual(ExpectedValueMark_Max, keyValue.Value);
                }
            }

            foreach (KeyValuePair<string, string> keyValue in reader.GetRecords()[5].GetOneStudent())
            {
                if (keyValue.Key == ExpectedBookmarkFio)
                {
                    Assert.AreEqual(ExpectedBookmarkFio, keyValue.Key);
                    Assert.AreEqual(ExpectedValueFio, keyValue.Value);
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
}