using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Data;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Context;
using Model.DataBase.Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace UnitTest.SpecificationData
{
    [TestClass]
    public class UnitUdostovereniyeSpec
    {
        Record[] Records;
        Programs Program;
        DataBaseContext DataBaseContext;

        public UnitUdostovereniyeSpec()
        {
            Records = new Record[12];
            Record record0 = new Record();
            record0.AddPropertyRecord("ФИО", "Бакиров Даурен Зиннурович");
            record0.AddPropertyRecord("Оценка", "4");
            record0.AddPropertyRecord("ДатаРождения", "15.04.1984 00:00:00");
            Records[0] = record0;

            Record record1 = new Record();
            record1.AddPropertyRecord("ФИО", "Баткевич Валерия Александровна");
            record1.AddPropertyRecord("Оценка", "5");
            record1.AddPropertyRecord("ДатаРождения", "04.02.1993 00:00:00");
            Records[1] = record1;

            Record record2 = new Record();
            record2.AddPropertyRecord("ФИО", "Бояркина Наталья Николаевна");
            record2.AddPropertyRecord("Оценка", "4");
            record2.AddPropertyRecord("ДатаРождения", "03.03.2000 00:00:00");
            Records[2] = record2;

            Record record3 = new Record();
            record3.AddPropertyRecord("ФИО", "Гах Дмитрий Сергеевич");
            record3.AddPropertyRecord("Оценка", "3");
            record3.AddPropertyRecord("ДатаРождения", "01.08.1995 00:00:00");
            Records[3] = record3;

            Record record4 = new Record();
            record4.AddPropertyRecord("ФИО", "Герман Алена Владимировна");
            record4.AddPropertyRecord("Оценка", "4");
            record4.AddPropertyRecord("ДатаРождения", "27.02.1996 00:00:00");
            Records[4] = record4;

            Record record5 = new Record();
            record5.AddPropertyRecord("ФИО", "Глушко Семен Эдуардович");
            record5.AddPropertyRecord("Оценка", "4");
            record5.AddPropertyRecord("ДатаРождения", "06.11.1995 00:00:00");
            Records[5] = record5;

            Record record6 = new Record();
            record6.AddPropertyRecord("ФИО", "Гольцман Ольга Николаевна");
            record6.AddPropertyRecord("Оценка", "4");
            record6.AddPropertyRecord("ДатаРождения", "30.09.1983 00:00:00");
            Records[6] = record6;

            Record record7 = new Record();
            record7.AddPropertyRecord("ФИО", "Грохов Алексей Михайлович");
            record7.AddPropertyRecord("Оценка", "3");
            record7.AddPropertyRecord("ДатаРождения", "29.06.1972 00:00:00");
            Records[7] = record7;

            Record record8 = new Record();
            record8.AddPropertyRecord("ФИО", "Ишмаков Сергей Фуатович");
            record8.AddPropertyRecord("Оценка", "4");
            record8.AddPropertyRecord("ДатаРождения", "20.07.1983 00:00:00");
            Records[8] = record8;

            Record record9 = new Record();
            record9.AddPropertyRecord("ФИО", "Портнова Ирина Юрьевна");
            record9.AddPropertyRecord("Оценка", "5");
            record9.AddPropertyRecord("ДатаРождения", "28.05.1976 00:00:00");
            Records[9] = record9;

            Record record10 = new Record();
            record10.AddPropertyRecord("ФИО", "Филиппов Олег Юрьевич");
            record10.AddPropertyRecord("Оценка", "3");
            record10.AddPropertyRecord("ДатаРождения", "02.06.1985 00:00:00");
            Records[10] = record10;

            Record record11 = new Record();
            record11.AddPropertyRecord("ФИО", "Яковец Евгений Сергеевич");
            record11.AddPropertyRecord("Оценка", "5");
            record11.AddPropertyRecord("ДатаРождения", "29.01.1987 00:00:00");
            Records[11] = record11;
        }

        
        
        [TestMethod]
        public void TestMethodData()
        {
            DocumentEvidenceAndUdostovereniyeSpec certificationSpec = new DocumentEvidenceAndUdostovereniyeSpec(Records, "01.01.2018", "02.01.2019", 
                                                                        "03.02.2020", "666", Program);
            certificationSpec.Correction();
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Отчество"], "Зиннурович");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Фамилия"], "Бакиров");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Оценка"], "четыре");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Номер"], "666-01");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Имя"], "Даурен");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["НД"], "01"); 
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["НМ"], "января");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["НГ"], "18");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["КД"], "02");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["КМ"], "января");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["КГ"], "19");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["ПД"], "03");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["ПМ"], "февраля");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["ПГ"], "20");

            Assert.AreEqual(certificationSpec.GetRecords()[9].GetOneStudent()["Номер"], "666-10");
            Assert.AreEqual(certificationSpec.GetRecords()[8].GetOneStudent()["Номер"], "666-09");

            Assert.AreEqual(certificationSpec.GetRecords()[11].GetOneStudent()["Оценка"], "пять");
        }
    }
}
