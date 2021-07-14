using System;

namespace SimpleChat.Database.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public Guid UserId { get; set; }
        public UserModel User { get; set; }

        public Guid ChatId { get; set; }
        public ChatModel Chat { get; set; }
    }
}