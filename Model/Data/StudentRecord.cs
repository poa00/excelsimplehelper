using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Data
{
    /// <summary>
    /// Запись об одном студенте
    /// </summary>
    public class StudentRecord : IStudentRecord
    {
        private Dictionary<string, string> OneStudent;
        public Dictionary<string, string> GetOneStudent() => OneStudent;

        public StudentRecord()
        {
            OneStudent = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Добавить новую запись
        /// Если ее нет вставить пробел
        /// </summary>
        /// <param name="bookmark">Название записи</param>
        /// <param name="value">Запись</param>
        public void AddPropertyRecord(string bookmark, string value)
        {
            if (bookmark == null || value == null)
                return;
            OneStudent.Add(bookmark, value);
        }

        /// <summary>
        /// Добавить новую запись
        /// Если ее нет вставить пробел
        /// </summary>
        /// <param name="bookmark"></param>
        /// <param name="value"></param>
        /// <param name="indexLine">номер строки в файле</param>
        public void AddPropertyRecord(string bookmark, string value, int indexLine)
        {
            if (bookmark == null || value == null)
                return;
            OneStudent.Add(bookmark.Trim(), Check(bookmark, value, indexLine));
        }

        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="bookmark">Назваание записи</param>
        public void RemoveRecord(string bookmark)
        {
            if (bookmark == null)
                return;
            OneStudent.Remove(bookmark);
        }

        /// <summary>
        /// Обединение записей
        /// </summary>
        /// <param name="record">Массив записей</param>
        public void ConcatRecord(StudentRecord record)
        {
            if (record == null)
            {
                throw new Exception("Запись равна null");
            }

            OneStudent = OneStudent.Concat(record.OneStudent.Where(kvp => !OneStudent.ContainsKey(kvp.Key)))
                                                .OrderBy(c => c.Value)
                                                .ToDictionary(c => c.Key, c => c.Value);           
        }
        /// <summary>
        /// Проверяет данные
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">значение</param>
        /// <returns></returns>
        public string Check(string key, string value, int indexLine)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                value = " ";
                return value;
            }

            if (key.Contains("Дата") || key.Contains("дата"))
            {
                return CheckDate(value.Trim(), indexLine);
            }
            if (key.Contains("Оценка") || key.Contains("оценка"))
            {
                return CheckMark(value.Trim(), indexLine);
            }
            if (key.Contains("Программа"))
            {
                return value;
            }

            return value.Trim();
        }

        
        /// <summary>
        /// Поверяет наличие данных (даты)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string CheckDate(string date, int indexLine)
        {
            if (date.Length > 10)
            {
                return date.Substring(0, 10);
            }
            if (date.Length < 10)
            {
                MessageBug.AddMessage(MessageBug.message.Не_правильно_заполнены_данные_дата_в_файле_excel, "в строке " + indexLine);
                date = "01.01.0001";
                return date;
            }
            return date;
        }

        /// <summary>
        /// Поверяет наличие данных (Оценки)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string CheckMark(string date, int indexLine)
        {
            if (date != " ")
            {
                if (date != "5" && date != "4" && date != "3")
                {
                    MessageBug.AddMessage(MessageBug.message.Не_правильно_заполнены_данные_оценка_в_файле_excel, " в строке " + indexLine);
                    return date;
                }
            }
            return date;
        }
    }
}
