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
        private Record[] StatementRecords;
        private string Group;
        private Programs Programm;

        public StatementSpec(Record[] records, string group, Programs programm)
        {
            StatementRecords = records;
            Group = group;
            Programm = programm;
        }

        public Record[] GetRecords()
        {
            return StatementRecords;
        }

        public void Correction()
        {
            for (int i = 0; i < StatementRecords.Length; i++)
            {
                StatementRecords[i] = CorrectMark(StatementRecords[i]);
                StatementRecords[i] = CorrectIndexInDocument(StatementRecords[i], Group, i);
                StatementRecords[i] = CorrectProgram(StatementRecords[i]);
            }
        }

        private Record CorrectProgram(Record dataStudent)
        {
            dataStudent.AddPropertyRecord("НаименованиеПрограммыУтверждения", Programm.name);

            return dataStudent;
        }

        /// <summary>
        /// Добавляет ноль в числа до 10 (Пример: 01 )
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Record CorrectIndexInDocument(Record dataStudent, string group, int index)
        {
            if ((index + 1) < 10)
            {
                dataStudent.AddPropertyRecord("группа", group + "-0" + (index + 1));
            }
            else
            {
                dataStudent.AddPropertyRecord("группа", group + "-" + (index + 1));
            }       
            return dataStudent;
        }

        private Record CorrectMark(Record dataStudent)
        {
            dataStudent.RemoveRecord("Оценка");
            return dataStudent;
        }
    }
}
