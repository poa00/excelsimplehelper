using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.SpecificationDataDocument
{
    public class SpecFunction
    {
        private Dictionary<string, string> month;

        public SpecFunction()
        {
            month = new Dictionary<string, string>
            {
                { "01", "января" },
                { "02", "февраля" },
                { "03", "марта" },
                { "04", "апреля" },
                { "05", "мая" },
                { "06", "июня" },
                { "07", "июля" },
                { "08", "августа" },
                { "09", "сентября" },
                { "10", "октября" },
                { "11", "ноября" },
                { "12", "декабря" }
            };
        }

        private string GetMonth(string key)
        {
            return month[key];
        }
        /// <summary>
        /// Вырезает из строки значение по определенному признаку 12.25.45841 станет 12 25 45841
        /// </summary>
        /// <param name="initialString">изначальная строка</param>
        /// <param name="elementCut">элемент по которрому произойдет разделения</param>
        public string[] CutFromStringElements(string initialString, char elementCut)
        {
            string[] array_full_name = initialString.Split(new char[] { elementCut }, StringSplitOptions.RemoveEmptyEntries);

            return array_full_name;
        }
        
        /// <summary>
        /// Берет числа из даты
        /// </summary>
        /// <param name="id">id даты из массива дат</param>
        /// <param name="count">какое число взять</param>
        /// <returns>число из даты</returns>
        public string GetNumberData(string date, int count)
        {
            string Number = " ";

            if (count == 1)
            {
                Number = CutFromStringElements(date, '.')[0]; //date.Substring(0, 2);// Пример: берет 02 из 02.12.1999
            }
            if (count == 2)
            {
                Number = CutFromStringElements(date, '.')[1];//date.Substring(3, 2);// Пример: берет 12 из 02.12.1999
            }
            if (count == 3)
            {
                string dateYear = CutFromStringElements(date, '.')[2];
                if (dateYear.Length == 2)
                {
                    Number = dateYear;
                }
                else
                {
                    Number = dateYear.Substring(2, 2);//date.Substring(8, 2);// Пример: берет 1299 из 02.12.1999
                }
            }
            return Number;
        }

        /// <summary>
        /// Заменяет число на месяц: 01 станет январь
        /// </summary>
        /// <param name="id">id даты из массива дат</param>
        /// <returns></returns>
        public string ConvertingNumberPerMonth(string month)
        {
            if (month.Length > 2)
            {
                return month;
            }

            return GetMonth(month);
        }

        /// <summary>
        /// Достает из строки с ФИО, данные разделенные пробелом
        /// </summary>
        /// <param name="student"></param>
        /// <param name="InitialString"></param>
        /// <returns></returns>
        protected StudentRecord CorrectFIO(StudentRecord dataStudent)
        {
            string[] fio = CutFromStringElements(dataStudent.GetOneStudent()["ФИО"], ' ');
            if (fio.Length < 3)
            {
                MessageBug.AddMessage("ФИО не полное");
            }
            if (fio.Length == 2)
            {
                dataStudent.AddPropertyRecord("Фамилия", fio[0]);
                dataStudent.AddPropertyRecord("Имя", fio[1]);
                dataStudent.AddPropertyRecord("Отчество", " ");
                dataStudent.RemoveRecord("ФИО");
                return dataStudent;
            }
            if (fio.Length == 1)
            {
                dataStudent.AddPropertyRecord("Фамилия", fio[0]);
                dataStudent.AddPropertyRecord("Имя", " ");
                dataStudent.AddPropertyRecord("Отчество", " ");
                dataStudent.RemoveRecord("ФИО");
                return dataStudent;
            }
            if (fio.Length == 0)
            {
                dataStudent.AddPropertyRecord("Фамилия", " ");
                dataStudent.AddPropertyRecord("Имя", " ");
                dataStudent.AddPropertyRecord("Отчество", " ");
                dataStudent.RemoveRecord("ФИО");
                return dataStudent;
            }
            dataStudent.AddPropertyRecord("Фамилия", fio[0]);
            dataStudent.AddPropertyRecord("Имя", fio[1]);
            dataStudent.AddPropertyRecord("Отчество", fio[2]);
            dataStudent.RemoveRecord("ФИО");
            return dataStudent;
        }
    }
}
