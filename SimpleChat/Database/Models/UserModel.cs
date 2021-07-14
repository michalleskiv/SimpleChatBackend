using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SimpleChat.Database.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        
        [JsonIgnore]
        public string PasswordHash { get; set; }
        
        [JsonIgnore]
        public string Token { get; set; }

        public IEnumerable<ChatModel> Chats { get; set; }
        public IEnumerable<MessageModel> Messages { get; set; }
    }
}