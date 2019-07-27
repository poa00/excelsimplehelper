using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Message
{
    /// <summary>
    /// Сообщения об ошибках во всем приложение
    /// </summary>
    public static class MessageBug
    {
        public static List<string> Message;

        public enum message
        {
            Не_правильно_заполнены_данные_оценка_в_файле_excel ,
            Не_правильно_заполнены_данные_дата_в_файле_excel,
            Повышение_квалификации_не_заполнено ,
            Кем_и_когда_одобрена_не_заполнены ,
            Не_все_поля_на_экране_заполнены ,
            Название_программы_не_заполнено ,
            Не_хватает_данных_в_файле_excel,
            Проблема_с_путем_к_файлу ,
            Модель_не_заполнена ,
            Нет_закладки_в_word ,
            Уроки_не_заполнены ,
            Часы_не_заполнены,
            Тип_не_выбран
        }

        static MessageBug()
        {
            Message = new List<string>(4);
        }

        /// <summary>
        /// Добавляет новые сообщения
        /// </summary>
        /// <param name="newMessage">Сообщение</param>
        public static void AddMessage(string newMessage)
        {
            Message.Add(newMessage);
        }

        /// <summary>
        /// Вовращает все сообщения об ошибках
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMessages()
        {
            return Message;
        }

        /// <summary>
        /// Очищает лист с сообщениями
        /// </summary>
        public static void ClearMessages()
        {
            Message.Clear();
        }
    }
}
