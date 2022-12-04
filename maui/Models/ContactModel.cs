using System;
using System.Collections.Generic;

namespace maui.Models
{
    public class ContactModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public int MessageFrom { get; set; }
        public string MessageText { get; set; }
    }
}
