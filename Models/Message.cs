using System;
namespace GroupChatApp.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string AddedBy { get; set; }
        public string ChatMessage { get; set; }
        public int GroupId { get; set; }
    }
}