using System;
using System.Collections.Generic;

namespace SimpleChat.Database.Models
{
    public class ChatModel
    {
        public Guid Id { get; set; }

        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<MessageModel> Messages { get; set; }
    }
}