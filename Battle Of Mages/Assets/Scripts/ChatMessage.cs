using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ChatMessage
    {
        private string username;
        private string message;

        public ChatMessage(string username, string message)
        {
            this.username = username;
            this.message = message;
        }
    }
}
