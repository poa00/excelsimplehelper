using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Data;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.SpecificationData
{
    [TestClass]
    public class UnitCertificateDangerousGoodsSpec
    {
        StudentRecord[] Records;
        ProgramDGs Program;

        public UnitCertificateDangerousGoodsSpec()
        {
            Program = new ProgramDGs();
            Program.dateNumberApproved = "sad";
            Program.name = "tests";
            Program.TypeDocument = new TypeDocument();
            Program.TypeDocument.Name = "te";

            Records = new StudentRecord[12];
            StudentRecord record0 = new StudentRecord();
            record0.AddPropertyRecord("ФИО", "Бакиров Даурен Зиннурович");
            record0.AddPropertyRecord("Оценка", "4");
            record0.AddPropertyRecord("ДатаРождения", "15.04.1984 00:00:00");
            Records[0] = record0;

            StudentRecord record1 = new StudentRecord();
            record1.AddPropertyRecord("ФИО", "Баткевич Валерия Александровна");
            record1.AddPropertyRecord("Оценка", "5");
            record1.AddPropertyRecord("ДатаРождения", "04.02.1993 00:00:00");
            Records[1] = record1;

            StudentRecord record2 = new StudentRecord();
            record2.AddPropertyRecord("ФИО", "Бояркина Наталья Николаевна");
            record2.AddPropertyRecord("Оценка", "4");
            record2.AddPropertyRecord("ДатаРождения", "03.03.2000 00:00:00");
            Records[2] = record2;

            StudentRecord record3 = new StudentRecord();
            record3.AddPropertyRecord("ФИО", "Гах Дмитрий Сергеевич");
            record3.AddPropertyRecord("Оценка", "3");
            record3.AddPropertyRecord("ДатаРождения", "01.08.1995 00:00:00");
            Records[3] = record3;

            StudentRecord record4 = new StudentRecord();
            record4.AddPropertyRecord("ФИО", "Герман Алена Владимировна");
            record4.AddPropertyRecord("Оценка", "4");
            record4.AddPropertyRecord("ДатаРождения", "27.02.1996 00:00:00");
            Records[4] = record4;

            StudentRecord record5 = new StudentRecord();
            record5.AddPropertyRecord("ФИО", "Глушко Семен Эдуардович");
            record5.AddPropertyRecord("Оценка", "4");
            record5.AddPropertyRecord("ДатаРождения", "06.11.1995 00:00:00");
            Records[5] = record5;

            StudentRecord record6 = new StudentRecord();
            record6.AddPropertyRecord("ФИО", "Гольцман Ольга Николаевна");
            record6.AddPropertyRecord("Оценка", "4");
            record6.AddPropertyRecord("ДатаРождения", "30.09.1983 00:00:00");
            Records[6] = record6;

            StudentRecord record7 = new StudentRecord();
            record7.AddPropertyRecord("ФИО", "Грохов Алексей Михайлович");
            record7.AddPropertyRecord("Оценка", "3");
            record7.AddPropertyRecord("ДатаРождения", "29.06.1972 00:00:00");
            Records[7] = record7;

            StudentRecord record8 = new StudentRecord();
            record8.AddPropertyRecord("ФИО", "Ишмаков Сергей Фуатович");
            record8.AddPropertyRecord("Оценка", "4");
            record8.AddPropertyRecord("ДатаРождения", "20.07.1983 00:00:00");
            Records[8] = record8;

            StudentRecord record9 = new StudentRecord();
            record9.AddPropertyRecord("ФИО", "Портнова Ирина Юрьевна");
            record9.AddPropertyRecord("Оценка", "5");
            record9.AddPropertyRecord("ДатаРождения", "28.05.1976 00:00:00");
            Records[9] = record9;

            StudentRecord record10 = new StudentRecord();
            record10.AddPropertyRecord("ФИО", "Филиппов Олег Юрьевич");
            record10.AddPropertyRecord("Оценка", "3");
            record10.AddPropertyRecord("ДатаРождения", "02.06.1985 00:00:00");
            Records[10] = record10;

            StudentRecord record11 = new StudentRecord();
            record11.AddPropertyRecord("ФИО", "Яковец Евгений Сергеевич");
            record11.AddPropertyRecord("Оценка", "5");
            record11.AddPropertyRecord("ДатаРождения", "29.01.1987 00:00:00");
            Records[11] = record11;
        }

        [TestMethod]
        public void TestMethodCorrection()
        {
            CertificateDangerousGoods certificationSpec = new CertificateDangerousGoods(Records, "01.01.2018", "2", Program);
            certificationSpec.Correction();
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Отчество"], "Зиннурович");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Фамилия"], "Бакиров");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Имя"], "Даурен");
            Assert.AreEqual(certificationSpec.GetRecords()[0].GetOneStudent()["Номер"], "002");

            Assert.AreEqual(certificationSpec.GetRecords()[10].GetOneStudent()["Номер"], "012");
        }
    }
}
