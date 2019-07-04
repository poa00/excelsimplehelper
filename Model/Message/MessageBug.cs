using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Message
{
    public static class MessageBug
    {
        private static List<string> Message;

        static MessageBug()
        {
            Message = new List<string>(1);
        }

        public static void AddMessage(string newMessage)
        {
            Message.Add(newMessage);
        }

        public static List<string> GetMessages()
        {
            return Message;
        }
    }
}
