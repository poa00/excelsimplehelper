using System.Collections.Generic;

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
            Не_правильно_заполнены_данные_дата_в_файле_excel ,
            Повышение_квалификации_не_заполнено ,
            Кем_и_когда_одобрена_не_заполнены ,
            Не_все_поля_на_экране_заполнены ,
            Название_программы_не_заполнено ,
            Не_хватает_данных_в_файле_excel ,
            Проблема_с_путем_к_файлу_или_папке ,
            Модель_не_заполнена ,
            Нет_закладки_в_word ,
            Уроки_не_заполнены ,
            Часы_не_заполнены,
            Тип_не_выбран
        }

        private static string[] listErrors;

        static MessageBug()
        {
            Message = new List<string>(4);
            listErrors = new string[13];
            listErrors[0] = "Не правильно заполнены данные оценка в файле excel";
            listErrors[1] = "Не правильно заполнены данные дата в файле excel";
            listErrors[2] = "Повышение квалификации не заполнено";
            listErrors[3] = "Кем и когда одобрена не заполнены";
            listErrors[4] = "Не все поля на экране заполнены или заполнены корректно";
            listErrors[5] = "Название программы не заполнено";
            listErrors[6] = "Не хватает данных в файле excel";
            listErrors[7] = "Проблема с путем к файлу или папке";
            listErrors[8] = "Модель не заполнена";
            listErrors[9] = "Нет закладки в word";
            listErrors[10] = "Уроки не заполнены";
            listErrors[11] = "Часы не заполнены";
            listErrors[12] = "Тип не выбран";
        }

        /// <summary>
        /// Добавляет новые сообщения
        /// </summary>
        /// <param name="newMessage">Сообщение</param>
        public static void AddMessage(message newMessage, string additionalMessage)
        {
            Message.Add(listErrors[(int)newMessage] + " " + additionalMessage);
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
