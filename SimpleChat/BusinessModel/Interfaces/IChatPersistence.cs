using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleChat.BusinessModel.Dto;
using SimpleChat.Database.Models;

namespace SimpleChat.BusinessModel.Interfaces
{
    public interface IChatPersistence
    {
        Task<IEnumerable<MessageModel>> GetAllMessagesAsync();
        Task<IEnumerable<ChatModel>> GetAllChatsAsync();
    }
}