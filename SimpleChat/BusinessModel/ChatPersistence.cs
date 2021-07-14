using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleChat.BusinessModel.Dto;
using SimpleChat.BusinessModel.Interfaces;
using SimpleChat.Database;
using SimpleChat.Database.Models;

namespace SimpleChat.BusinessModel
{
    public class ChatPersistence : IChatPersistence
    {
        private readonly ChatContext _chatContext;
        
        public ChatPersistence(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }
        
        public async Task<IEnumerable<MessageModel>> GetAllMessagesAsync() => await _chatContext.Messages.ToListAsync();

        public async Task<IEnumerable<ChatModel>> GetAllChatsAsync() => await _chatContext.Chats.ToListAsync();
    }
}