using System.Collections.Generic;

namespace Model.Data
{
    class Month
    {
        private Dictionary<string, string> month;

        public Month()
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

        public string GetMonth(string key)
        {
            return month[key];
        }
    }
}
