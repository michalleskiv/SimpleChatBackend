using System;

namespace SimpleChat.BusinessModel.Dto
{
    public class MessageDto
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
    }
}