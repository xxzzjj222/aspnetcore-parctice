using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Services
    {
        public interface IAccount { }

        public interface IMessageService
        {
            public string Text { get; set; }
        }
        public interface ITool { }

        public class Account : IAccount { }
        public class MessageService : IMessageService
        {
            public string Text { get; set; }

            public MessageService()
            {
                Text = "Hello Message";
                Console.WriteLine(Text);
            }
        }
        public class Tool : ITool { }
    }
}
