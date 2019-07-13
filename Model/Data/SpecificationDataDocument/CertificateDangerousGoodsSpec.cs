using Model.DataBase.Model;
using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.Data.SpecificationDataDocument
{
    public class CertificateDangerousGoodsSpec : SpecFunction
    {
        private Record[] CertificateDangerousGoodsRecords;
        private string Number;
        private string DateIssue;
        private ProgramDGs Programm;
        public bool IsCertificate12Category;

        public CertificateDangerousGoodsSpec(Record[] records, string dateIssue, string number, ProgramDGs programm)
        {
            CertificateDangerousGoodsRecords = records;
            DateIssue = dateIssue;
            Number = number;
            Programm = programm;
            IsCertificate12Category = false;
        }

        /// <summary>
        /// Сертификат 12 категории
        /// </summary>
        /// <returns></returns>
        private bool isCertificate12Category()
        {
            int[] teacher12Categories = Regex.Matches(Programm.name, "\\d+").Cast<Match>().Select(x => int.Parse(x.Value)).ToArray();
            if (teacher12Categories[0] == 12)
            {
                return true;
            }
            return false;
        }

        public Record[] GetRecords()
        {
            return CertificateDangerousGoodsRecords;
        }

        public void Correction()
        {
            IsCertificate12Category = isCertificate12Category();
            for (int i = 0; i < CertificateDangerousGoodsRecords.Length; i++)
            {
                CertificateDangerousGoodsRecords[i] = CorrectFIO(CertificateDangerousGoodsRecords[i]);
                CertificateDangerousGoodsRecords[i] = CorrectData(CertificateDangerousGoodsRecords[i]);
                CertificateDangerousGoodsRecords[i] = CorrectNumberDocument(CertificateDangerousGoodsRecords[i], i);
                CertificateDangerousGoodsRecords[i] = CorrectProgram(CertificateDangerousGoodsRecords[i]);
            }
        }

        public void CorrectionLoad()
        {
            for (int i = 0; i < CertificateDangerousGoodsRecords.Length; i++)
            {
                CertificateDangerousGoodsRecords[i] = CorrectData(CertificateDangerousGoodsRecords[i]);
                CertificateDangerousGoodsRecords[i] = CorrectProgram(CertificateDangerousGoodsRecords[i]);
            }
        }

        public Record CorrectData(Record dataStudent)
        {
            dataStudent.AddPropertyRecord("ДатаВыдачи", DateIssue);
            return dataStudent;
        }

        public Record CorrectNumberDocument(Record dataStudent, int id)
        {
            int.TryParse(string.Join("", Number.Where(c => char.IsDigit(c))), out int value);
            if ((value + id) > 10 && (value + id) < 100)
            {
                dataStudent.AddPropertyRecord("Номер", "0" + (value + id).ToString());
                return dataStudent;
            }
            if ((value + id) < 10)
            {
                dataStudent.AddPropertyRecord("Номер", "00" + (value + id).ToString());
                return dataStudent;
            }
            dataStudent.AddPropertyRecord("Номер", (value + id).ToString());
            return dataStudent;
        }

        

        public Record CorrectProgram(Record dataStudent)
        {
            dataStudent.AddPropertyRecord("НазваниеПрограммы", Programm.name);
            dataStudent.AddPropertyRecord("КогдаКемУтверждена", Programm.dateNumberApproved);
            dataStudent.AddPropertyRecord("Тип", Programm.TypeDocument.Name);
            return dataStudent;
        }
    }
}
