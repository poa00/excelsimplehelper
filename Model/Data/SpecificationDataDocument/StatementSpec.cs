using Model.DataBase.Model;
using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.SpecificationDataDocument
{
    public class StatementSpec : SpecFunction
    {
        private StudentRecord[] StatementRecords;
        private string Group;
        private int numberInt;
        private string ProgrammName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="records">Данные из файла</param>
        /// <param name="group">Номер группы</param>
        /// <param name="programmName">Название программы</param>
        public StatementSpec(StudentRecord[] records, string group, string programmName)
         {
            StatementRecords = records;
            Group = group;
            numberInt = -1;
            ProgrammName = programmName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="records">Данные из файла</param>
        /// <param name="group">Номер группы</param>
        /// <param name="certificateNumber">Номер для сертификата ОГ</param>
        /// <param name="programmName">Название программы</param>
        public StatementSpec(StudentRecord[] records, string group, string certificateNumber, string programmName)
        {
            StatementRecords = records;
            Group = group;

            numberInt = Int32.Parse(certificateNumber);
            ProgrammName = programmName;
        }

        public StudentRecord[] GetRecords()
        {
            return StatementRecords;
        }

        /// <summary>
        /// Корректировка данных под сертификат
        /// </summary>
        public void Correction()
        {
            for (int i = 0; i < StatementRecords.Length; i++)
            {
                StatementRecords[i] = CorrectMark(StatementRecords[i]);
                StatementRecords[i] = CorrectIndexInDocument(StatementRecords[i], i);
                StatementRecords[i] = CorrectProgram(StatementRecords[i], ProgrammName);
            }
        }

        /// <summary>
        /// Добавляет программу в данные студентов
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <param name="programm"></param>
        /// <returns></returns>
        private StudentRecord CorrectProgram(StudentRecord dataStudent, string programm)
        {
            dataStudent.AddPropertyRecord("НаименованиеПрограммыУтверждения", programm);

            return dataStudent;
        }

        /// <summary>
        /// Добавляет ноль в числа до 10 (Пример: 01 )
        /// </summary>
        /// <param name="records">Данные из файла</param>
        /// <param name="index"></param>
        /// <returns></returns>
        private StudentRecord CorrectIndexInDocument(StudentRecord dataStudent, int index)
        {
            if (numberInt == -1)
            {
                return correctCertificate012(dataStudent, index);
            }
            else
            {
                return correctCertificate3(dataStudent, index);
            }
        }

        /// <summary>
        /// Корректирует  номера для Свидетельства, Удостоверение(лицензия), Удостоверения(Реквизит)
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private StudentRecord correctCertificate012(StudentRecord dataStudent, int index)
        {
            if ((index + 1) < 10)
            {
                dataStudent.AddPropertyRecord("группа", Group + "-0" + (index + 1));
            }
            else
            {
                dataStudent.AddPropertyRecord("группа", Group + "-" + (index + 1));
            }
            return dataStudent;
        }

        /// <summary>
        /// Корректирует  номера для Сертификат ОГ
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private StudentRecord correctCertificate3(StudentRecord dataStudent, int index)
        {
            if (index == 0)
            {
                dataStudent.AddPropertyRecord("группа", numberInt.ToString());
            }
            else
            {
                numberInt++;
                dataStudent.AddPropertyRecord("группа", numberInt.ToString());
            }
            return dataStudent;
        }

        /// <summary>
        /// Удаляет оценку из ведомости
        /// </summary>
        /// <param name="records">Данные из файла</param>
        /// <param name="dataStudent"></param>
        /// <returns></returns>
        private StudentRecord CorrectMark(StudentRecord dataStudent)
        {
            dataStudent.RemoveRecord("Оценка");
            return dataStudent;
        }
    }
}
