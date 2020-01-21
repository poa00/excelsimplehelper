using Model.DataBase.Model;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.Data.SpecificationDataDocument
{
    /// <summary>
    /// Данные для сертификата ОГ
    /// </summary>
    public class CertificateDangerousGoods
    {
        private StudentRecord[] CertificateDangerousGoodsRecords;
        private string Number;
        private string DateIssue;
        private ProgramDGs Programm;
        public bool IsCertificate12Category;
        private SpecFunction SpecFunction_;

        public CertificateDangerousGoods(StudentRecord[] records, string dateIssue, string number, ProgramDGs programm)
        {
            CertificateDangerousGoodsRecords = records;
            DateIssue = dateIssue;
            Number = number;
            Programm = programm;
            IsCertificate12Category = false;

            SpecFunction_ = new SpecFunction();
        }

        /// <summary>
        /// Проверка является ли сертификат 12 категории
        /// </summary>
        /// <returns></returns>
        private bool isCertificate12Category()
        {
            int[] teacher12Categories = Regex.Matches(Programm.name, "\\d+").Cast<Match>().Select(x => int.Parse(x.Value)).ToArray();
            if (teacher12Categories.Length <= 0)
            {
                return false;
            }
            if (teacher12Categories[0] == 12)
            {
                return true;
            }
            
            return false;
        }

        public StudentRecord[] GetRecords()
        {
            return CertificateDangerousGoodsRecords;
        }

        /// <summary>
        /// Корректирует данные для сертификата ОГ
        /// </summary>
        public void Correction()
        {
            IsCertificate12Category = isCertificate12Category();
            for (byte i = 0; i < CertificateDangerousGoodsRecords.Length; i++)
            {
                CertificateDangerousGoodsRecords[i] = SpecFunction_.CorrectFIO(CertificateDangerousGoodsRecords[i], i);
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

        /// <summary>
        /// Добавляет дату выдачи
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <returns></returns>
        public StudentRecord CorrectData(StudentRecord dataStudent)
        {
            dataStudent.AddPropertyRecord("ДатаВыдачи", DateIssue);
            return dataStudent;
        }

        /// <summary>
        /// Добавляет нули к числу
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentRecord CorrectNumberDocument(StudentRecord dataStudent, byte id)
        {
            int.TryParse(string.Join("", Number.Where(c => char.IsDigit(c))), out int value);
            if ((value + id) >=  10 && (value + id) < 100)
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

        /// <summary>
        /// Добавляет название группы
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <returns></returns>
        private StudentRecord CorrectGroup(StudentRecord dataStudent)
        {
            dataStudent.AddPropertyRecord("Группа", Number.Trim());

            return dataStudent;
        }

        /// <summary>
        /// Добавляет программу и данные о ней
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <returns></returns>
        public StudentRecord CorrectProgram(StudentRecord dataStudent)
        {
            dataStudent.AddPropertyRecord("Программа", Programm.name);
            dataStudent.AddPropertyRecord("КогдаКемУтверждена", Programm.dateNumberApproved);
            dataStudent.AddPropertyRecord("Тип", Programm.TypeDocument.Name);
            return dataStudent;
        }
    }
}
